using UnityEngine;
using System.Collections;

public class SetCollider : MonoBehaviour {

	public CharacterController Controller;

    private ColliderSettings Settings;
	
	public GameObject Ak;
	
	void Start()
	{
		Settings = new ColliderSettings();	
	}
	
	void Update()
	{
		if(Ak.activeSelf)
		{
			Controller.radius = Settings.RadiusAk;
			Controller.height = Settings.HeightAk;
			Controller.center = Settings.CenterAk;
		}
		else
		{
			Controller.radius = Settings.Radius;
			Controller.height = Settings.Height;
			Controller.center = Settings.Center;
		}
	}
}
