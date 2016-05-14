using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class SlenderPlaceScript : MonoBehaviour {

	public List<Transform> List = new List<Transform>();
	public List<Transform> NearList;	
	public SlenderController controller;
	 
	public bool IsCurrent = false;	
		
	public bool IsInForest = false;
	
	public bool CanUseInFirest = false;
	
	void Start () {
		controller = (SlenderController)GameObject.FindGameObjectWithTag("Slender").GetComponent("SlenderController");
	}
	
	// Update is called once per frame
	void Update () {
		//if(IsCurrent)
		//	UpdatePos();
	}
	
	void OnTriggerEnter(Collider myCollider)
	{
		if(myCollider.gameObject.tag == "Player")
		{ 
			IsCurrent = true;
			controller.SetPlace((SlenderPlaceScript)this);
		}
	}
	
	void OnTriggerExit(Collider myCollider)
	{
		if(myCollider.gameObject.tag == "Player")
		{
			IsCurrent = false;
		}
	}
	
	void UpdatePos()
	{
		foreach(var item in List.Where(q => q != null))
		{
			Debug.DrawLine(item.position, transform.position, Color.blue);
		}
		foreach(var item in NearList.Where(q => q != null))
		{
			Debug.DrawLine(item.position, transform.position, Color.red);	
		}	
	}
}
