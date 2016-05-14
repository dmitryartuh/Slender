using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {
	
	public Camera Camer;

    public float Max = 10;
    public float Min = 60;
	
	public AudioSource Source;
	public AudioClip Clip;
	
	void Start(){	
	}
	
	void Update () {
		if(Input.GetButtonDown("ZoomUp"))
		{
			Camer.fieldOfView-=10;
			if(Camer.fieldOfView < Max)
				Camer.fieldOfView = Max;
			Source.clip = Clip;
			Source.loop = false;
			Source.volume = 0.3f;
			Source.Play();
		}
		else if(Input.GetButtonDown("ZoomDown"))
		{
			Camer.fieldOfView+=10;
			if(Camer.fieldOfView > Min)
				Camer.fieldOfView = Min;
			Source.clip = Clip;
			Source.loop = false;
			Source.volume = 0.3f;
			Source.Play();
		}
	}
}
