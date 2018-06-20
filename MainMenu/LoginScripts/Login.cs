using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using System.Text;
using System;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour {

	//Declaration des variables
	public static string loginUsername = "";
	public static string loginPassword = "";
	public string CurrentMenu ="Login";
	public Canvas StartMenu;
	//addresse du script creer un compte
	public string url = "http://localhost/CreateAccountT.php"; 
	//addresse du script connection
	public string url2 = "http://localhost/LoginAccount.php";
	//Contient le lien de la page Php
	private string LoginUrl="";
	private string Confirmmail_address = "";
    private string Confirmpass = "";
	//Variables pour créer email et mot de passe
	private string Cmail_address = "";
	private string Cpassword="";
	private string username = "";
	private string age = "";

	// Pour tester la GUI
	public float X;
	public float Y;
	public float Width;
	public float Height;

	//lancee au commencement
	public void Start () {
		//Je recupere le Canvas et je le fais disparaitre
		StartMenu = StartMenu.GetComponent<Canvas> ();
		StartMenu.enabled = true;
	}// Fin Start


	//Fonction GUI
	void OnGUI(){
			//Si le menu actif est Login alors on affiche le menu Login 
			// Sinon on affiche le menu créer un compte en appelant la fonction CreateAccountGui
			if (CurrentMenu == "Login") {
				//Appeler le GUI du Login
				LoginGUI ();
			} else if (CurrentMenu == "CreateAccount") {
				//Appeler le GUI pour créer un compte
				CreateAccountGUI ();
		}
	}//Fin GUI
		
	//Cette methode est pour connecter les comptes
	//et creer le menu Login
	void LoginGUI(){
		GUI.Box (new Rect (300,120,(Screen.width/4)+200,(Screen.height/4)+250), "Connection");
		// Creer les boutons connection et Creer un compte
		//Ouvre le menu Creer un compte
		if(GUI.Button(new Rect(370,360,120,25),"Créer un compte")){
			CurrentMenu="CreateAccount";
			
		}
		//Creation champ Email
		GUI.Label(new Rect(420,200,220,25),"Username");
		loginUsername=GUI.TextField(new Rect(420,225,220,25),loginUsername);

		//Creation champ Mot de passe
		GUI.Label(new Rect(420,255,220,25),"Votre Mot de Passe:");
		loginPassword=GUI.TextField(new Rect(420,280,220,25),loginPassword);
		//Connecter l'utilisateur
		if(GUI.Button(new Rect(580,360,120,25),"Se Connecter")){
			if ( loginUsername + loginPassword != null){
				//Messager qui envoit au php
				WWWForm FormLogin = new WWWForm ();
				//Ce qui est envoyés au script php
				FormLogin.AddField ("loginUsername",loginUsername);
				FormLogin.AddField ("loginPassword",loginPassword);
				//Creation d'un nouvel objet formulaire avec les infos
				WWW log = new WWW (url2, FormLogin);
				//appeler la fonction pour envoyer le formulaire
				StartCoroutine(LoginAccount(log));
			}else{
			}
		}//Fin boucle creer un compte
	}

	//Envoit le formulaire dans la base de données
	IEnumerator LoginAccount( WWW log){
		//Attends le retour de php vers Unity
		yield return log;
		if(log != null){
			Debug.Log ("WWW OK !"+loginUsername+loginPassword+log.text);
			Destroy(this);
		}else{
			Debug.Log ("WWW error");
		}
	}


	//Cette methode est le GUI pour créer un compte
	void CreateAccountGUI(){
		GUI.Box (new Rect (300,120,(Screen.width/4)+200,(Screen.height/4)+250), "Création d'un Compte");
		//Creation champ Email, Mot de passe et leur confirmation
		GUI.Label(new Rect(420,150,220,25)," Choisissez votre Pseudo");
		username=GUI.TextField(new Rect(420,170,220,25),username);

		GUI.Label(new Rect(420,200,220,25),"Votre Email:");
		Cmail_address=GUI.TextField(new Rect(420,225,220,25),Cmail_address);

		GUI.Label(new Rect(420,255,220,25),"Votre Mot de Passe:");
		Cpassword=GUI.TextField(new Rect(420,400,220,25),Cpassword);

		GUI.Label(new Rect(420,310,220,25)," Confimez Votre Email:");
		Confirmmail_address=GUI.TextField(new Rect(420,340,220,25),Confirmmail_address);

		GUI.Label(new Rect(420,370,220,25)," Confirmez Votre Mot de Passe:");
		Confirmpass=GUI.TextField(new Rect(420,280,220,25),Confirmpass);

		GUI.Label(new Rect(420,425,220,25)," Indiquez nous votre âge");
		age=GUI.TextField(new Rect(420,450,220,25),age);

		// Creer un compte
		//Ouvre le menu pour créer un compte
		if(GUI.Button(new Rect(370,480,120,25),"Créer un compte")){
			if (Confirmpass == Cpassword && Confirmmail_address == Cmail_address){

				//Messager qui envoit au php
				WWWForm Form = new WWWForm ();
				//Ce qui est envoyés au script php
				Form.AddField ("username", username);
				Form.AddField ("Cpassword", Cpassword);
				Form.AddField ("Cmail_address",Cmail_address);
				Form.AddField ("age", age);
				WWW www = new WWW (url, Form);
				StartCoroutine(CreateAccount(www));
			}else{
				//StartCoroutine ();
			}
		}//Fin boucle creer un compte
		//Connecter l'utilisateur
		if(GUI.Button(new Rect(580,480,120,25),"Retour")){
			CurrentMenu="Login";

		}

		
	}
		
	//Envoit les données dans la base
	IEnumerator CreateAccount( WWW www){

		//Attends le retour de php vers Unity
		yield return www;
		if(www != null){
			Debug.Log ("WWW OK !"+username+Cpassword+Cmail_address+age+www.text);
			CurrentMenu="Login";
		}else{
			Debug.Log ("WWW error");

		}
	}
	



}//fin CreateAccount





