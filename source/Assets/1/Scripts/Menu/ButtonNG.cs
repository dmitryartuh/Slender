using UnityEngine;
using System.Collections;

public class ButtonNG : Button {

    public override void Action()
	{
		Application.LoadLevel(1);
		base.Action ();
	}
}
