using UnityEngine;
using System.Collections;

public class Battery : MonoBehaviour {

	public bool IsInTrigger = false;
	
	public Vector3 pos;
	
	public bool IsUnder = false;
	
	void Start()
	{
		pos = transform.position;	
		if(IsUnder)
			transform.position = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			IsInTrigger = true;
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
			IsInTrigger = false;	
	}
	
	public void Show()
	{
		transform.position = pos;	
		IsUnder = false;
	}
	
	public void DisShow()
	{
		transform.Translate(0,-10,0);
		IsUnder = true;
		IsInTrigger = false;
	}
}
