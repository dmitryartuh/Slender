using UnityEngine;
using System.Collections;

public class CrateController : MonoBehaviour {

	public Transform Cam;
	public RaycastHit Hit;
	public bool IsOpen = false;
	public GameObject Box;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Action") && !IsOpen)
		{
			Physics.Raycast(Cam.transform.position, Cam.transform.forward, out Hit, 5f);
			if(Hit.transform != null)
			{
				Debug.Log(Hit.transform.tag);
				if(Hit.transform.tag == "Tampa")
				{
					Box.animation.Play();
					IsOpen = true;
				}
			}
		}
	}
}
