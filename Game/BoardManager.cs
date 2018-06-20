using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BoardManager : MonoBehaviour 
{
	public static BoardManager Instance { set; get; }
	private bool [,] allowedMoves{ set; get; }
	public Chessman[,] Chessmans { set;get;}
	private Chessman selectedChessman;


	private const float TILE_SIZE = 1.0f;//taille des cases
	private const float TILE_OFFSET = 0.5f;//espacement
	//Axes
	private int selectionX=-1;
	private int selectionY=-1;

	public List<GameObject> chessmanPrefabs;
	private List<GameObject> activeChessman = new List<GameObject>();

	private Quaternion orientation = Quaternion.Euler(0,180,0);

	public bool isWhiteTurn = true;

	public GameObject bouclier_joueur;
	public GameObject bouclier_adversaire;
	public Canvas Encart_fight;
	public Canvas message_tour_humains;
	public Canvas message_tour_enfers;

	private void Start(){
		Destroy (bouclier_joueur,3);
		Destroy (bouclier_adversaire,3);
		Instance = this;
		SpawnAllChessmans ();
		Encart_fight = Encart_fight.GetComponent<Canvas> ();
		message_tour_enfers = message_tour_enfers.GetComponent<Canvas> ();
		message_tour_humains = message_tour_humains.GetComponent<Canvas> ();
		bouclier_joueur= bouclier_joueur.GetComponent<GameObject> ();
		bouclier_adversaire= bouclier_adversaire.GetComponent<GameObject> ();
		Encart_fight.enabled = true;
		message_tour_enfers.enabled = false;
		message_tour_humains.enabled = false;


	
		StartCoroutine ("disablethe_fight");
	}



	IEnumerator disablethe_fight(){
		yield return new WaitForSeconds(3);
		Encart_fight.enabled = false;

	}

	//methode d'initialisation qui appeles la creation du plateau
	private void Update(){
		UpdateSelection();
		DrawChessboard ();


		if (Input.GetMouseButtonDown (0)) 
		{
			if (selectionX >=0 && selectionY >=0) 
			{
				//Selectionner la piece
				if (selectedChessman == null) 
				{
					SelectedChessman(selectionX,selectionY);
				} else {
				 // Bouger la piece
					MoveChessman(selectionX,selectionY);
				
				
				}
			}
		}
	}



	private void SelectedChessman(int x,int y)
	{
		if (Chessmans [x, y] == null)
			return;
		if (Chessmans [x, y].isWhite != isWhiteTurn)
			return;

		//Pour q'une piece bloquer par un adversaire ne soit plus selectionnable
		bool hasAtleastOneMove = false;
		allowedMoves = Chessmans [x, y].PossibleMove ();
		for (int i = 0; i < 8; i++)
			for (int j = 0; j < 8; j++)
				if (allowedMoves [i, j])
					hasAtleastOneMove = true;

		selectedChessman = Chessmans [x, y];
		BoardHighlights.Instance.HighlightAllowedMoves (allowedMoves);
	}



	private void MoveChessman(int x,int y)
	{
		if (allowedMoves [x, y]) 
		{
			
			Chessman c = Chessmans [x, y];
			if (c != null && c.isWhite != isWhiteTurn)
			{
				
				// SI c'est le ROI
				if (c.GetType () == typeof(King)) 
				{
					// FIN de la Partie
					EndGame();
					return;
				}

				//Capturer une piece
				activeChessman.Remove(c.gameObject);
				Destroy (c.gameObject);


			}


			Chessmans [selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.position = GetTileCenter (x, y);
			//
			selectedChessman.SetPosition (x, y);
			Chessmans [x, y] = selectedChessman;
			//Changer le tour apres un deplacement
			isWhiteTurn = !isWhiteTurn;
			message_tour_humains.enabled = true;
			StartCoroutine ("endMoveHumains");

		}
		BoardHighlights.Instance.Hidehighlights ();
		selectedChessman = null; 
		message_tour_enfers.enabled = true;
		StartCoroutine ("endMoveEnfers");
	}
	IEnumerator endMoveHumains(){
		yield return new WaitForSeconds(1);
		message_tour_humains.enabled = false;

	}
	IEnumerator endMoveEnfers(){
		yield return new WaitForSeconds(1);
		message_tour_enfers.enabled = false;

	}


	private void UpdateSelection(){
		if (!Camera.main)
			return;
		//Les Collisions Box
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) 
		{
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		}else{
			selectionX=-1;
			selectionY=-1;
		}
	}
	//methode pour dessiner le plateau
	private void DrawChessboard(){
		//dessiner la line gauche de longueur 8
		//et la hauteur de 8
		Vector3 widthLine = Vector3.right*8;
		Vector3 heigthLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start,start + widthLine);
			for (int j = 0; j <= 8; j++) {
				start = Vector3.right *j;
				Debug.DrawLine (start,start + heigthLine);
			}

		}
		//Dessiner la selection
		if (selectionX >= 0 && selectionY >= 0) {
			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * (selectionX+1),
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX );
		}

	}



	private void SpawnChessman(int index,  int x, int y)
	{
		GameObject go = Instantiate (chessmanPrefabs [index], GetTileCenter(x,y), Quaternion.identity)as GameObject;
		go.transform.SetParent (transform);
		Chessmans [x, y] = go.GetComponent<Chessman> ();
		Chessmans [x, y].SetPosition (x, y);
		activeChessman.Add (go);
	}
	private void SpawnAllChessmans (){
		activeChessman = new List<GameObject> ();
		Chessmans = new Chessman[8, 8];

		//Spawn Equipe Blanche
		//Roi
		SpawnChessman (0,3,0);
		//Reine
		SpawnChessman (1,4,0);
		//Tours
		SpawnChessman (2,0,0);
		SpawnChessman (2,7,0);
		//Fous
		SpawnChessman (3,2,0);
		SpawnChessman (3,5,0);
		//Cavaliers
		SpawnChessman (4,1,0);
		SpawnChessman (4,6,0);
		//Pions
		for (int i = 0; i < 8; i++) {
		SpawnChessman (5,i,1);
		}


		//Spawn Equipe Noire
		//Roi
		SpawnChessman (6,4,7);
		//Reine
		SpawnChessman (7,3,7);
		//Tours
		SpawnChessman (8,0,7);
		SpawnChessman (8,7,7);
		//Fous
		SpawnChessman (9,2,7);
		SpawnChessman (9,5,7);
		//Cavaliers
		SpawnChessman (10,1,7);
		SpawnChessman (10,6,7);
		//Pions
		for (int i = 0; i < 8; i++) {
	SpawnChessman (11,i,6);
		}
	}


	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		origin.y += 0.65f;
		return origin;
	}

	private void EndGame()
	{
		if (isWhiteTurn)
			Debug.Log ("L'Equipe MEDIEVAL L'EMPORTE");
		else
			Debug.Log ("L'Equipe DES ENFERS L'EMPORTE");
		foreach (GameObject go in activeChessman)
			Destroy (go);

		isWhiteTurn = true;
		BoardHighlights.Instance.Hidehighlights ();
		SpawnAllChessmans ();
			
	}

}
