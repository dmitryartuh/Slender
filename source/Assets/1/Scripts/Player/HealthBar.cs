using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public float TotalHealth = 400f;
    public float CurrentHealth;
	
	public NoiseEffect Effect;
	
	public bool IsDead = false;

    public float MaxGrainInt = 4;

    public float MaxScratchInt = 3.5f;

    public float TotalStayTime = 120f;
    public float CurrentStayTime = 0;
    public Vector3 LastTransform;
	
	public AudioSource Source;
	public AudioClip Clip;

    public Rect position;
	public Texture2D Tex;
	public Finish finish;
	
	void Normolize(float x)
	{
		Effect.grainIntensityMin = x * MaxGrainInt;
		Effect.scratchIntensityMin = x * MaxScratchInt;
		Effect.grainIntensityMax =  0.5f * x * MaxGrainInt;
		Effect.scratchIntensityMax = 0.5f * x * MaxScratchInt;
		Source.volume = x;
	}	
	
	void Start () 
    {
		Source.clip = Clip;
		Source.loop = true;
		Source.volume = 0;
		Source.Play();
		Effect = (NoiseEffect)gameObject.GetComponentInChildren(typeof(NoiseEffect));
		Normolize(0);
		CurrentHealth = TotalHealth;
		LastTransform = transform.position;
		position = new Rect(10, 10, 0, 15);
	}
	
	void Update()
	{
		if(transform.position == LastTransform)
		{
			CurrentStayTime += Time.deltaTime;
			if(CurrentStayTime >= TotalStayTime)
			{
				Debug.Log ("Died with stayTime");
				finish.End(false);
			}
		}
		else 
		{
			CurrentStayTime = 0;
			LastTransform = transform.position;
		}
		if(CurrentHealth > TotalHealth)
			CurrentHealth = TotalHealth;
		if(CurrentHealth == 0)
		{
			Debug.Log ("Died with health");
				finish.End(false);
		}
		Normolize(1 - CurrentHealth / TotalHealth);
		
		//Debug.Log(CurrentHealth + "  " + CurrentDeadTime);
	}
	
	public void SetDamage(float x)
	{
		if(x == 0)
		{
			CurrentHealth += 100 * Time.deltaTime;
			return;
		}
		CurrentHealth -= x;
		if(CurrentHealth <= 0)
			CurrentHealth = 0;
	}	
	
	void OnGUI()
	{
		position.width = (Screen.width / 2) * (CurrentHealth / (float)TotalHealth);
		GUI.DrawTexture(position, Tex, ScaleMode.StretchToFill); 
	}
}
