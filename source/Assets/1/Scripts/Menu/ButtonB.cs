using UnityEngine;
using System.Collections;

public class ButtonB : Button {
	
	public GameObject Cam;
    private float XPosition = -8;

    public override void Action()
	{
		((Moving)Cam.GetComponent(typeof(Moving))).Destination = XPosition;
		base.Action ();
	}
}
