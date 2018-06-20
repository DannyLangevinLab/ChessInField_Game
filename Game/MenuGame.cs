using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuGame : MonoBehaviour {

	public Canvas Menu_pause;
	public Button btn_pause;
	public Button btn_reprendre;
	public Button btn_options;
	public Button btn_abandonner;



	// Use this for initialization
	void Start () {
		Menu_pause = Menu_pause.GetComponent<Canvas> ();
		btn_pause= btn_pause.GetComponent<Button> ();
		btn_reprendre= btn_reprendre.GetComponent<Button> ();
		btn_options= btn_options.GetComponent<Button> ();
		btn_abandonner= btn_abandonner.GetComponent<Button> ();
		Menu_pause.enabled = false;


	}
	public void PausePress(){
		Menu_pause.enabled = true; 

	}

	public void ReprendrePress()
	{
		Menu_pause.enabled = false; 

	}

	public void OptionsPress()
	{
		Application.LoadLevel (2);
	}


	public void AbandonnerPress()
	{
		Application.LoadLevel (0);
	}

}
