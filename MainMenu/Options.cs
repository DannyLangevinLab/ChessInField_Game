using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour {

	public Canvas OptionsCanvas;
	public Button OptionsText;


	// Use this for initialization
	void Start () {
		OptionsText = OptionsText.GetComponent<Button> ();

	}
		

	public void Retour ()
	{
		Application.LoadLevel (0);
	}

}


