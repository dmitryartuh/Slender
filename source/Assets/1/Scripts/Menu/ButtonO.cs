using UnityEngine;
using System.Collections;

public class ButtonO : Button {

	public GameObject Cam;
	private float XPosition = 16;

    public override void Action()
	{
		((Moving)Cam.GetComponent(typeof(Moving))).Destination = XPosition;
		base.Action ();
	}
}
