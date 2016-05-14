using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	private bool IsMouseOn = false;

    protected void OnMouseEnter()
	{
		IsMouseOn = true;
	}

    protected void OnMouseExit()
	{
		IsMouseOn = false;	
	}

    protected void OnMouseUp()
	{
		Action();	
	}

    public virtual void Action()
	{
		Debug.Log (gameObject.name);
	}
}
