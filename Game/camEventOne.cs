using UnityEngine;
using System.Collections;

public class camEventOne : MonoBehaviour {

	public Camera[] cams;

	public void CamMainMove(){
		cams [0].enabled = true;
		cams [1].enabled = false;
		cams [2].enabled = false;

	}

	public void CamLateralsMove(){
		cams [0].enabled = false;
		cams [1].enabled = true;
		cams [2].enabled = false;

	}

	public void CamDosMove(){
		cams [0].enabled = false;
		cams [1].enabled = false;
		cams [2].enabled = true;

	}
}
