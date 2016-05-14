using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	public CharacterMotor Motor;
    public float MaxForwardSpeed = 5;
    public float MaxRunSpeed = 8;
    public float MinRunSpeed = 4;

    public bool IsFail = false;
    public bool IsRun = false;
	
	public AudioClip WalkSound;
	public AudioClip Fail;
	public AudioSource SourceForSteps;
	public AudioSource Source;

    public float TotalEnergy = 60;
    public float _currentEnergy;
    public float OneRechargeEnergy = 10;
    public float OneLess;

    public float WalkDistance = 0.5f;
    public float RunDistance = 0.3f;
    public float CurrentDistance;
    public float _time;

    private bool IsForwardPress = false;
    private bool IsBackPress = false;
    private bool IsLeftPress = false;
    private bool IsRightPress = false;
	
	public RotateCam Cam;
	
	void Start () {
		CurrentDistance = WalkDistance;
		_currentEnergy = TotalEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		_currentEnergy += (OneRechargeEnergy) * Time.deltaTime;
		OneLess = Motor.movement.maxForwardSpeed * 2f;
		if(IsRun)
		{
			_time -= Time.deltaTime;
			_currentEnergy -= Time.deltaTime * OneLess;
			if(_time < 0)
			{
				SourceForSteps.PlayOneShot(WalkSound);
				_time = CurrentDistance;
			}
		}
		
		if(_currentEnergy > TotalEnergy)
			_currentEnergy = TotalEnergy;
		
		if(_currentEnergy < 0 && !IsFail)
		{
			Motor.movement.maxForwardSpeed = MinRunSpeed;
			IsFail = true;
			CurrentDistance = WalkDistance;
			Source.PlayOneShot(Fail);
		}
		
		if(_currentEnergy > TotalEnergy/4 && IsFail)
		{
			IsFail = false;
			Motor.movement.maxForwardSpeed = MaxForwardSpeed;
		}
			
		if(Input.GetButtonDown("Run") && !IsFail)
		{
			Motor.movement.maxForwardSpeed = MaxRunSpeed;
			CurrentDistance = RunDistance;
		}
		if(Input.GetButtonUp("Run") && !IsFail)
		{
			Motor.movement.maxForwardSpeed = MaxForwardSpeed;
			CurrentDistance = WalkDistance;
		}
		UpdateButtoms();
		if(!IsRun)
			if(IsForwardPress || IsBackPress || IsLeftPress || IsRightPress)
			{
				IsRun = true;
				_time = CurrentDistance;
			}
		if(!IsForwardPress && !IsBackPress && !IsLeftPress && !IsRightPress)
		{
			IsRun = false;
		}
		if(CurrentDistance == RunDistance && IsRun)
			Cam.IsRun = true;
		else Cam.IsRun = false;
	}
	
	void UpdateButtoms()
	{
		if(Input.GetButtonDown("Forward"))
			IsForwardPress = true;
		if(Input.GetButtonDown("Back"))
			IsBackPress = true;
		if(Input.GetButtonDown("Left"))
			IsLeftPress = true;
		if(Input.GetButtonDown("Right"))
			IsRightPress = true;
		
		if(Input.GetButtonUp("Forward"))
			IsForwardPress = false;
		if(Input.GetButtonUp("Back"))
			IsBackPress = false;
		if(Input.GetButtonUp("Left"))
			IsLeftPress = false;
		if(Input.GetButtonUp("Right"))
			IsRightPress = false;
	}
}
