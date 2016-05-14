using UnityEngine;
using System.Collections;

public class ButtonE : Button {

    public override void Action()
	{
		base.Action ();
		Application.Quit();
	}
}
