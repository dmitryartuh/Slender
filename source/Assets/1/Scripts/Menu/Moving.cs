using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float Destination;

    private float Speed = 1f;
	
	void Start()
	{
		Destination = transform.position.x;	
	}
	
	void Update () {
		if(Destination != transform.position.x)
		{
			float delta = Destination - transform.position.x;
			if(delta > Speed && delta > 0)
				delta = Speed;
			if(delta < Speed && delta < 0)
				delta = -Speed;
			transform.Translate(delta, 0 , 0);	
		}
	}
}
