using UnityEngine;
using System.Collections;

public class SlenderAttack : MonoBehaviour {
	
	public Camera CamPlayer;
	
	public GameObject Player;
	public HealthBar PlayerHealth;

    private float MinDist = 1f;
    public float MaxDist = 45f;

    public bool IsPlayerVis = false;
	
	private RaycastHit hit;
	
	public GameObject StartRay;
	
	void Start () {
		PlayerHealth = (HealthBar)Player.GetComponent(typeof(HealthBar));
	}
	
	void Update () {
		transform.transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, 0), 
					Quaternion.LookRotation(new Vector3(CamPlayer.transform.position.x, 0, CamPlayer.transform.position.z) - transform.position), 
			180);
		IsPlayerVis = IsPlayerInCam();
		//Debug.Log(IsPlayerVis);
		if(IsPlayerInCam())
			PlayerHealth.SetDamage(CalDamage());
		else PlayerHealth.SetDamage(0);
	}
	
	public bool IsPlayerInCam()
	{
		Quaternion last = StartRay.transform.rotation;
		for(int i = 0 ; i < 5 ; i++)
		{
			StartRay.transform.RotateAround(transform.up, 0.005f);
			Physics.Raycast(StartRay.transform.position, StartRay.transform.forward, out hit, 
				Vector3.Distance(transform.position, Player.transform.position) + 1);	
				if (hit.transform != null)
				{
					Debug.DrawRay(StartRay.transform.position, 
					StartRay.transform.forward * (Vector3.Distance(transform.position, Player.transform.position)+1), Color.red);
					if(hit.transform.tag == "Player")
					{
						StartRay.transform.rotation = last;
						return true;	
					}
				}
		}
		StartRay.transform.rotation = last;
		last = StartRay.transform.rotation;
		for(int i = 0 ; i < 5 ; i++)
		{
			StartRay.transform.RotateAround(transform.up, -0.005f);
			Physics.Raycast(StartRay.transform.position, StartRay.transform.forward, out hit, 
				Vector3.Distance(transform.position, Player.transform.position) + 1);	
				if (hit.transform != null)
				{
					Debug.DrawRay(StartRay.transform.position, 
					StartRay.transform.forward * (Vector3.Distance(transform.position, Player.transform.position)+1), Color.red);
					if(hit.transform.tag == "Player")
					{
						StartRay.transform.rotation = last;
						return true;	
					}
				}
		}
		StartRay.transform.rotation = last;
		return false;
	}
			
	float CalDamage()
	{
		var res = (MaxDist - Vector3.Distance(transform.position, Player.transform.position)) / MaxDist;
		if(res <= 0)
			return 1;
		if(Vector3.Distance(transform.position, Player.transform.position) < 1.6f * MinDist)
			return 501;
		else return res * 100 * Time.deltaTime;
	}
	
	void OnCollisionEnter(Collision x)
	{
		Debug.Log(x.collider.gameObject.tag);
		if(x.collider.gameObject.tag == "Player")
			PlayerHealth.SetDamage(501);	
	}
}