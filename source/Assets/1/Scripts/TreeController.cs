using UnityEngine;
using System.Collections;

public class TreeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var list = GameObject.FindGameObjectsWithTag("tree");
		foreach(var item in list)
		{	
			item.renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
