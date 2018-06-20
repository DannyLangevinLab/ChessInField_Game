using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class menuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
	public Button OptionsText;



	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText= startText.GetComponent<Button> ();
		exitText= exitText.GetComponent<Button> ();
		OptionsText= exitText.GetComponent<Button> ();
		quitMenu.enabled = false;

	
	}

	public void ExitPress()
	{
		quitMenu.enabled = true; 
		startText.enabled = false;
		exitText.enabled = false;
		OptionsText.enabled = false;
	

	}

	public void NoPress()
	{
		quitMenu.enabled = false; 
		startText.enabled = true;
		exitText.enabled = true;
		OptionsText.enabled = true;
	
	}

	public void StartLevel()
	{
		Application.LoadLevel (1);
	}
	public void StartOptions()
	{
		Application.LoadLevel (2);
	}
	public void Retour ()
	{
		Application.LoadLevel (0);
	}
	public void ExitGame ()
	{
		Application.Quit ();
	}
}
