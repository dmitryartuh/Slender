using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {
	
	public Light FlashLight;
    public bool IsOn = true;
	
	public AudioClip OnOff;
	public AudioClip FlashSound;
	public AudioSource Source;

    public float MaxEnergy = 60;
	public float CurrentBataryEnergy = 60;
    public float EnergyForSecond = 0.5f;

    public float _minTimeFlash = 5;
    public float _maxTimeFlash = 10;
    public float _previousFlash;
    public float _flashTime = 0.15f;
    public float _currentFlashTime = 0;
	
	public BatteryController BatteryComtroller;
	
	void Start () {
		CurrentBataryEnergy = MaxEnergy;
		_previousFlash = (CurrentBataryEnergy / EnergyForSecond) / 4;
	}
	
	void NewBatary()
	{
		CurrentBataryEnergy = MaxEnergy;
		_previousFlash = (CurrentBataryEnergy / EnergyForSecond) / 4;
		_minTimeFlash = 5;
		_maxTimeFlash = 10;
	}

	void UpdatePar ()
	{
		_minTimeFlash = 0.5f + 4 * (CurrentBataryEnergy / MaxEnergy);
		_maxTimeFlash = 1.5f + 7 * (CurrentBataryEnergy / MaxEnergy);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Battery"))
		{
			if(BatteryComtroller.GetBattery())
				CurrentBataryEnergy = MaxEnergy;	
		}
		UpdatePar ();
		if(CurrentBataryEnergy < 0)
		{
			IsOn = false;
			CurrentBataryEnergy = 0; 
		}
		_currentFlashTime -= Time.deltaTime;
		if(_currentFlashTime < 0)
			_currentFlashTime = 0;
		if(Input.GetButtonDown("Torch"))
		{
			if(IsOn)
			{
				IsOn = false;	
				FlashLight.intensity = 0;
				Source.PlayOneShot(OnOff);
			}
			else
			{
				IsOn = true;
				FlashLight.intensity = 2 * CurrentBataryEnergy / MaxEnergy;
				_previousFlash = (CurrentBataryEnergy / EnergyForSecond) / 4;
				Source.PlayOneShot(OnOff);
			}
		}
		
		if(IsOn)
		{
			if(_currentFlashTime > 0)
			{
				FlashLight.intensity = 0;
			}
			else
			{
				FlashLight.intensity = 2 * CurrentBataryEnergy / MaxEnergy;
				CurrentBataryEnergy -= Time.deltaTime * EnergyForSecond;
				_previousFlash -= Time.deltaTime;
				if(_previousFlash <= 0)
				{
					FlashLight.intensity = 0;
					_currentFlashTime = _flashTime;
					_previousFlash = Random.Range(_minTimeFlash, _maxTimeFlash);
					Source.PlayOneShot(FlashSound);
				}
			}
		}
	}
}
