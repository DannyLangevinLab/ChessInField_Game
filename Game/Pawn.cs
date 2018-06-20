using UnityEngine;
using System.Collections;

public class Pawn : Chessman 
{
	public override bool [,] PossibleMove()
	{
		bool[,] r = new bool[8, 8];
		Chessman c, c2;

		//Mouvements pions MEDIEVAUX
		if (isWhite) 
		{
			//Diagonale Gauche
			if (CurrentX !=0 && CurrentY != 7) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX -1, CurrentY + 1];
				//Si il n'y a pas de piece et qu'elle est blanche
				if (c != null && !c.isWhite)
					r [CurrentX - 1, CurrentY + 1] = true; 
			}

			//Diagonale Droite

			if (CurrentX != 7 && CurrentY != 7) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY + 1];
				if (c != null && !c.isWhite)
					r [CurrentX + 1, CurrentY + 1] = true; 
			}

			//Mileu

			if (CurrentY != 7) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				if (c == null)
					r [CurrentX, CurrentY + 1] = true;
			}

			//pour le premier coup
			if (CurrentY == 1) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 2];
				if (c == null & c2 == null)
					r [CurrentX, CurrentY + 2] = true;
			}

			//pour le premier coup
			if (CurrentY == 1) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY + 2];
				if (c == null & c2 == null)
					r [CurrentX, CurrentY + 2] = true;
			}
		}
		else{
			                         // EQUIPE DES ENFERS

			//Diagonale Gauche
			if (CurrentX !=0 && CurrentY != 0) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX -1, CurrentY -1];
				//Si il n'y a pas de piece et qu'elle est blanche
				if (c != null && c.isWhite)
					r [CurrentX - 1, CurrentY -1] = true; 
			}

			//Diagonale Droite

			if (CurrentX != 7 && CurrentY != 0) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX + 1, CurrentY -1];
				if (c != null && c.isWhite)
					r [CurrentX + 1, CurrentY -1] = true; 
			}

			//Mileu

			if (CurrentY != 0) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY -1];
				if (c == null)
					r [CurrentX, CurrentY  -1] = true;
			}

			//pour le premier coup
			if (CurrentY ==6) 
			{
				c = BoardManager.Instance.Chessmans [CurrentX, CurrentY -1];
				c2 = BoardManager.Instance.Chessmans [CurrentX, CurrentY -2];
				if (c == null & c2 == null)
					r [CurrentX, CurrentY -2] = true;
			}


		
	}
		return r;
}
}