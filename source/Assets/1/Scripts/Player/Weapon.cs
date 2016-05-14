using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public AudioClip ShotSound;
	public AudioClip FreeShotSound;
	public AudioClip GilzSound = null;
	public AudioClip RechargeSound;
	public AudioSource _audioSorce;
	
	public GameObject BulletPrefab;
	public GameObject FirePrefab;
	public GameObject StartPosition;
	public GameObject StartPotation;

    public bool IsAvtometic = false;

    public float BulletRechargeTime;
    public float _bulletDeltaTime;
    public float HolderRechargeTime;
    public float _holderDeltaTime;
    public bool _isRecharge;

    public float LiveTime = 5;

    public int TotalBullet;
    public int BulletInHolder;
    public int _currentBulletInHolder = 0;
	
	public AnimationClip RechargeClip;
    public float AnimationSpeedRecharge;
	
	public AnimationClip TriggerOnClip;
    public float AnimationSpeedTriggerOn;
    public bool IsTriggerOnPlayed = false;
	
	public AnimationClip TriggerOffClip;
    public float AnimationSpeedTriggerOff;
	
	
	// Use this for initialization
	void Start () {
		_bulletDeltaTime = 0;
		_holderDeltaTime = 0;
		_isRecharge = false;
		IsAvtometic = false;
		HolderRechargeTime = RechargeClip.length + 1f;
		animation[RechargeClip.name].speed = AnimationSpeedRecharge;
		animation[TriggerOnClip.name].speed = AnimationSpeedTriggerOn;
		animation[TriggerOffClip.name].speed = AnimationSpeedTriggerOff;
	}
	
	void OnGUI()
	{
		GUI.TextField(new Rect(10,70,50,20), _currentBulletInHolder + "/" + TotalBullet);	
	}
	
	private void UpdateRecharge()
	{
		if(_isRecharge)
		{
			_holderDeltaTime+=Time.deltaTime;
			if(_holderDeltaTime >= HolderRechargeTime)
			{
				_isRecharge = false;
				if(TotalBullet >= BulletInHolder)
				{
					TotalBullet -= BulletInHolder;
					_currentBulletInHolder = BulletInHolder;
				}
				else 
				{
					_currentBulletInHolder = TotalBullet;
					TotalBullet = 0;
				}
			}
			return;
		}
		if((Input.GetButtonDown("Recharge")) && (TotalBullet > 0) && (_currentBulletInHolder < BulletInHolder))
		{
			_isRecharge = true;
			animation.Play("Recharge");
			_audioSorce.PlayOneShot(RechargeSound);
			_holderDeltaTime = 0;
			TotalBullet += _currentBulletInHolder;
			_currentBulletInHolder = 0;
		}
	}
	
	// Update is called once per frame
	void Update () {
		_bulletDeltaTime += Time.deltaTime; 
		_holderDeltaTime += Time.deltaTime; 
		UpdateHolders();
		UpdateRecharge();
		if(Input.GetButtonUp("Fire1") && !_isRecharge)
		{
			IsAvtometic = false;
			animation.Play(TriggerOffClip.name);
			IsTriggerOnPlayed = false;
		}
		if((Input.GetButtonDown("Fire1") || (IsAvtometic))&& !_isRecharge)
		{
			IsAvtometic = true;
			if(!IsTriggerOnPlayed)
			{
				animation.Play(TriggerOnClip.name);
				IsTriggerOnPlayed = true;
			}
			if(_bulletDeltaTime >= BulletRechargeTime && _currentBulletInHolder > 0)
			{
				_bulletDeltaTime = 0;
				_currentBulletInHolder--;
				Instantiate(BulletPrefab, 
					StartPosition.transform.position, StartPotation.transform.rotation);
				GameObject.Destroy(Instantiate(FirePrefab, 
					StartPosition.transform.position, StartPotation.transform.rotation), 0.08f); 
				_audioSorce.PlayOneShot(ShotSound); 
				if(GilzSound != null)
					_audioSorce.PlayOneShot(GilzSound); 
				return;
			}
			else
			{
				if(_currentBulletInHolder <= 0)
				{
					_audioSorce.PlayOneShot(FreeShotSound);
					IsAvtometic = false;
					//animation.Play(TriggerOffClip.name);
					//IsTriggerOnPlayed = false;
				}
			}
		}
	}
	
	void UpdateHolders()
	{
		if(Input.GetButtonDown("Action"))
		{
			foreach(var item in GameObject.FindGameObjectsWithTag("WeaponCrate"))
			{
				var temp = ((WeaponCrate)item.GetComponent(typeof(WeaponCrate)));
				if(temp.IsInTrigger)	
				{
					TotalBullet += BulletInHolder * temp.Holders;
					GameObject.Destroy(item);
					break;
				}
			}	
		}	
	}
}
