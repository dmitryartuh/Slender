using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour {

	public GameObject Player;
	public GameObject Slender;
	
	public AudioSource Source;
	public AudioClip StartClip;

    public float TotalTime;
    public float CurrentTime = 0;

    public float TotalWaitTime = 0.5f;
    public float CurrentWaitTime = 0;

    public bool IsPlayed = false;
	
	void Start () {
		Screen.fullScreen =true;
		Screen.showCursor = false;
		foreach(var item in GameObject.FindGameObjectsWithTag("Sun"))
		{
			((Light)item.GetComponent(typeof(Light))).intensity = 0;	
		}
		((CharacterMotor)Player.GetComponent(typeof(CharacterMotor))).canControl = false;
		((MoveController)Player.GetComponent(typeof(MoveController))).enabled = false;
		((SlenderController)Slender.GetComponent(typeof(SlenderController))).enabled = false;
		TotalTime = StartClip.length / 2;
		Source.clip = StartClip;
		Source.loop = false;
		animation.Play();
	}
	
	
	void Update () {
		CurrentWaitTime += Time.deltaTime;
		if(CurrentWaitTime < TotalWaitTime)
			return;
		else if(!Source.isPlaying && !IsPlayed)
		{
			IsPlayed = true;
			Source.Play();
		}
		CurrentTime += Time.deltaTime;
		if(CurrentTime >= TotalTime)
		{
			((CharacterMotor)Player.GetComponent(typeof(CharacterMotor))).canControl = true;
			((MoveController)Player.GetComponent(typeof(MoveController))).enabled = true;
			((SlenderController)Slender.GetComponent(typeof(SlenderController))).enabled = true;
			this.enabled = false;
		}
	}
}
