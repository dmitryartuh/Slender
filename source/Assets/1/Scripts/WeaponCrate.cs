using UnityEngine;
using System.Collections;

public class WeaponCrate : MonoBehaviour {

    public bool IsInTrigger = false;

    public int Holders = 1;

    private Vector3 pos;
	
	void Start()
	{
		pos = transform.position;
		transform.position = new Vector3(pos.x, pos.y - 10, pos.z);
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
	}
}
