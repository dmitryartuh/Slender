using UnityEngine;
using System.Collections;

public class SlenderRotate : MonoBehaviour {

	public Transform Target;
	
	void Start () {
		Target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Target);
	}
}
