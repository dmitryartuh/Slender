using UnityEngine;
using System.Collections;

public class ButtonBToMenu : Button {

    public override void Action()
	{
		Application.LoadLevel(0);
	}
}
