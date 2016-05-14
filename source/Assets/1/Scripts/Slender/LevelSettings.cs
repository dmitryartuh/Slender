using UnityEngine;
using System.Collections;

public class LevelSettings 
{
	public float MinRechargeTime;
	public float MaxRechargeTime;
	
	public float MinWaitTime;
	public float MaxWaitTime;
	
	public float MinStayTime;
	public float MaxStayTime;
	
	public AudioClip Sound;
	public AudioClip SlenderNoise;
	public AudioClip SlenderStartNoise;
	
	public float MinSpeed;
	public float MaxSpeed;
	
	public float MinFollowTime;
	public float MaxFollowTime;	
	
	public float Distance;
	
	public static float AttackDistance = 1f;
	
	public LevelSettings(float minRechargeTime, float maxRechargeTime, float minWaitTime, float maxWaitTime, 
		float minStayTime, float maxStayTime, AudioClip sound, AudioClip slenderNoise,
		float minSpeed, float maxSpeed, float minFollowTime, float maxFollowTime, float distance,
		AudioClip slenderStartNoise)
	{
		this.MinStayTime = minStayTime;
		this.MaxStayTime = maxStayTime;
		this.MinWaitTime = minWaitTime;
		this.MaxWaitTime = maxWaitTime;
		this.Sound = sound;
		this.SlenderNoise = slenderNoise;
		this.MinRechargeTime = minRechargeTime;
		this.MaxRechargeTime = maxRechargeTime;
		this.MinSpeed = minSpeed;
		this.MaxSpeed = maxSpeed;
		this.MinFollowTime = minFollowTime;
		this.MaxFollowTime = maxFollowTime;
		this.Distance = distance;
		this.SlenderStartNoise = slenderStartNoise;
	}
}
