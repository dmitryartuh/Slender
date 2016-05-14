using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Exit"))
		{
			Screen.showCursor = true;
			Application.LoadLevel(0);	
		}
	}
}
