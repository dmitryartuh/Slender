using UnityEngine;
using System.Collections;

public class Ficha1 : MonoBehaviour {
	
	private float TotalTime = 20;
    private float CurrentTime = 0;

    private bool IsHere = false;
    private bool IsOpen = false;

    private float TotalCloseTime = 100;
    private float CurrentCloseTime = 0;
	
	public GameObject Door;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Player").transform.position.y < -10)
			Application.LoadLevel(2);
		
		if(IsHere)
			CurrentTime += Time.deltaTime;
		if(CurrentTime >= TotalTime)
		{
			Door.collider.isTrigger = true;
			IsOpen = true;
		}
		if(IsOpen)
			CurrentCloseTime += Time.deltaTime;
		if(CurrentCloseTime >= TotalCloseTime)
		{
			Door.collider.isTrigger = false;
			GameObject.Destroy(this);
		}
	}
	
	void OnTriggerEnter()
	{
		IsHere = true;
	}
	
	void OnTriggerExit()
	{
		IsHere = false;
	}
}
