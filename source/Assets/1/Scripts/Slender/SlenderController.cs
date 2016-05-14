using UnityEngine;
using System.Collections.Generic;

public enum SlenderStatus {Stay, Wait, Follow, Recharge};

public class SlenderController : MonoBehaviour {

    public int MaxSizeStack = 10;

    public float MinDist = 20;
    public float MaxDist = 50;
    public float MinNearDist = 10;
    public float MaxNearDist = 20;

    public Transform WaitPosition;
	public SlenderPlaceScript CurrentPlace;

    public float TotalRechargeTime;
    public float CurrentRechargeTime = 0;

    public float TotalWaitTime;
    public float CurrentWaitTime = 0;

    public float TotalStayTime;
    public float CurrentStayTime = 0;

    public float TotalFollowTime;
    public float CurrentFollowTime = 0;

    public float Speed;

    public bool IsReady = false;
    public bool IsOn = false;
    public bool IsVis = false;
    public bool WasInCam = false;

    public int Level = -1;
    public LevelSettings CurrentSettings;

    public Dictionary<int, LevelSettings> dic;
	
	public AudioClip Sound1;
	public AudioClip Sound3;
	public AudioClip Sound5;
	public AudioClip Sound7;
	
	public AudioClip Appear0;
	public AudioClip Appear1;
	public AudioClip Appear2;
	
	public AudioSource Source;
	public AudioSource SourceThis;

    private List<SlenderPlaceScript> AllPlaces;
	
	public Transform Cam;

    private SlenderStatus State;
	
	public NavMeshAgent Agent;

    private int MaxHealth = 20;
    private int MinHealth = 10;
    private int CurrentHealth;
	
	private RaycastHit hit;
		
	void Start () {
		Source = (AudioSource)GameObject.FindGameObjectWithTag("TotalSource").GetComponent(typeof(AudioSource));
		dic = new Dictionary<int, LevelSettings>();
		dic.Add(-1, new LevelSettings(1, 3, 90, 90, 0, 0, null, null, 3, 5, 10, 10, 1f, null));
		dic.Add(0, new LevelSettings(14, 20, 10, 15, 0, 0, Sound1, null, 0, 0, 0, 0, 0, null));//дальний везде
		dic.Add(1, new LevelSettings(12, 17, 10, 15, 0, 0, Sound1, null, 0, 0, 0, 0, 0, null));//дальний впереди
		dic.Add(2, new LevelSettings(10, 15, 10, 15, 0, 2, Sound3, null, 0, 0, 0, 0, 0, Appear0));//ближний везде
		dic.Add(3, new LevelSettings(6, 11, 10, 15, 0, 0, Sound3, null, 0, 0, 0, 0, 0, Appear0));//ближний впереди
		dic.Add(4, new LevelSettings(5, 10, 2, 5, 1, 3, Sound5, null, 1, 2, 8, 12, 1f, Appear1));//дальний везде и бежит
		dic.Add(5, new LevelSettings(4, 8, 1, 4, 1, 3, Sound5, null, 2, 3, 10, 17, 1f, Appear1));//дальний впереди и бежит
		dic.Add(6, new LevelSettings(2, 7, 1, 1, 0, 0, Sound7, null, 3, 4, 10, 16, 1f, Appear2));//ближний везде и бежит 
		dic.Add(7, new LevelSettings(1, 4, 1, 1, 0, 0, Sound7, null, 4, 5, 10, 15, 1f, Appear2));//ближний впереди и бежит 
		AllPlaces = new List<SlenderPlaceScript>();
		NextLevel();
		TotalRechargeTime = Random.Range(CurrentSettings.MinRechargeTime, CurrentSettings.MaxRechargeTime);
		CurrentRechargeTime = 0;
		IsReady = false;
		IsOn = false;
		Source.loop = true;
		foreach(var item in GameObject.FindGameObjectsWithTag("Place"))
		{
			SlenderPlaceScript temp = (SlenderPlaceScript)item.GetComponent(typeof(SlenderPlaceScript));
			if(temp.IsInForest == true)
				AllPlaces.Add(temp);
		}
	} 

	void Recharge ()
	{
		MovTo (WaitPosition);
		IsOn = false;
		IsReady = false;
		Agent.enabled = false;
		State = SlenderStatus.Recharge;
		WasInCam = false;
	}
	
	void UpdateTime()
	{
		if(!IsReady && !IsOn)
		{
			CurrentRechargeTime += Time.deltaTime;
			if(CurrentRechargeTime >= TotalRechargeTime)
			{
				IsReady = true;
				CurrentRechargeTime = 0;
			}
		}
		if(IsOn)
		{
			switch(State)
			{
			case(SlenderStatus.Stay):
				CurrentStayTime += Time.deltaTime;
				if(CurrentStayTime >= TotalStayTime)
				{
					State = SlenderStatus.Wait;
					CurrentWaitTime = 0;
				}
				break;
			case(SlenderStatus.Wait):
				CurrentWaitTime += Time.deltaTime;
				if(CurrentWaitTime >= TotalWaitTime)
				{
					State = SlenderStatus.Follow;
					CurrentFollowTime = 0;
					Agent.enabled = true;
					Agent.destination = Cam.transform.position;
					Agent.speed = Random.Range(CurrentSettings.MinSpeed, CurrentSettings.MaxSpeed);
				}
				break;
			case(SlenderStatus.Follow):
				CurrentFollowTime += Time.deltaTime;
				if(CurrentFollowTime >= TotalFollowTime)
				{
					if(!IsInCam())
					{
						if(Agent.remainingDistance > MaxDist / 4)
							Recharge ();
						return;
					}
					else WasInCam = true;
				}
				if(Agent.remainingDistance > MaxDist)
				{
					Recharge();
					return;
				}
				break;
			}
		}
	}
	
	public bool IsInCam()
	{
		return ((SlenderAttack)gameObject.GetComponent(typeof(SlenderAttack))).IsPlayerVis;
	}

	void MovTo (Transform place)
	{
		Agent.enabled = false;
		transform.position = new Vector3(place.position.x, 0, place.transform.position.z);
		Agent.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		IsVis = IsInCam();
		if(IsReady && !IsOn)
		{
			TotalStayTime = Random.Range(CurrentSettings.MinStayTime, CurrentSettings.MaxStayTime);
			TotalWaitTime = Random.Range (CurrentSettings.MinWaitTime, CurrentSettings.MaxWaitTime);
			Agent.speed = Random.Range(CurrentSettings.MinSpeed, CurrentSettings.MaxSpeed);
			Agent.stoppingDistance = CurrentSettings.Distance;
			TotalFollowTime = Random.Range (CurrentSettings.MinFollowTime, CurrentSettings.MaxFollowTime);
			CurrentFollowTime = 0;
			CurrentStayTime = 0;
			CurrentWaitTime = 0;
			
			var list = GetPossiblePlaces();
			var place  = FindPlace(list);
			if(place == null)
				return;
			//foreach(var item in list)
			//{
			//	Debug.DrawLine(CurrentPlace.transform.position, item.position, Color.white);
			//}
			//Debug.DrawLine(CurrentPlace.transform.position, place.position, Color.green);
			MovTo (place);
			IsReady = false;
			IsOn = true;
			State = SlenderStatus.Stay;
			SourceThis.clip = CurrentSettings.SlenderStartNoise;
			SourceThis.loop = false;
			SourceThis.Play ();
			CurrentHealth = Random.Range(MinHealth, MaxHealth);
		}
		UpdateTime();
		if(IsOn)
		{
			
			if(CurrentHealth == 0)
			{
				Recharge();
				return;
			}
			if(IsInCam())
				WasInCam = true;
			switch(State)
			{
			case(SlenderStatus.Stay):
			case(SlenderStatus.Wait):
				transform.transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, 0), 
					Quaternion.LookRotation(new Vector3(Cam.transform.position.x, 0, Cam.transform.position.z) - transform.position), 
					Time.deltaTime * 180);
				break;
			case(SlenderStatus.Follow):
				if(TotalFollowTime == 0)
					transform.transform.rotation = Quaternion.Slerp(new Quaternion(0, transform.rotation.y, 0, 0), 
					Quaternion.LookRotation(new Vector3(Cam.transform.position.x, 0, Cam.transform.position.z) - transform.position), 
					Time.deltaTime * 180);
				else Agent.SetDestination(Cam.transform.position);
				break;
			}
		}
	}
	
	void SetTimes()
	{
		TotalRechargeTime = Random.Range(CurrentSettings.MinRechargeTime, CurrentSettings.MaxRechargeTime);
		CurrentRechargeTime = 0;
		
	}
	
	List<Transform> GetPossiblePlaces()
	{
		var res = new List<Transform>();
		var tempStack = new Stack<SlenderPlaceScript>();
		switch(Level)
		{
		case (0):
		case (1):
		case (2):
		{
            if (CurrentPlace.List != null)
            {
                for (int i = 0; i < CurrentPlace.List.Count; i++)
                    res.Add(CurrentPlace.List[i]);
            }
            if (CurrentPlace.CanUseInFirest)
            {
                foreach (var item in AllPlaces)
                {
                    var distance = Vector3.Distance(CurrentPlace.transform.position, item.transform.position);
                    if ((distance > MinDist) && (distance < MaxDist))
                        res.Add(item.transform);
                }
            }
            if (res.Count == 0)
            {
                if (CurrentPlace.NearList != null)
                {
                    for (int i = 0; i < CurrentPlace.NearList.Count; i++)
                        res.Add(CurrentPlace.NearList[i]);
                }
            }
			break;	
		}
		case (5):
		case (6):
		{
            if (CurrentPlace.List != null)
            {
                for (int i = 0; i < CurrentPlace.List.Count; i++)
                    res.Add(CurrentPlace.List[i]);
            }
            if (CurrentPlace.NearList != null)
            {
                for (int i = 0; i < CurrentPlace.NearList.Count; i++)
                    res.Add(CurrentPlace.NearList[i]);
            }
            if (CurrentPlace.CanUseInFirest)
            {
                foreach (var item in AllPlaces)
                {
                    var distance = Vector3.Distance(CurrentPlace.transform.position, item.transform.position);
                    if ((distance > MinDist) && (distance < MaxDist))
                        res.Add(item.transform);
                }
            }
			break;	
		}
		case (3):
		case (4):
		case (7):
		case (8):
		{
            if (CurrentPlace.NearList != null)
            {
                for (int i = 0; i < CurrentPlace.NearList.Count; i++)
                    res.Add(CurrentPlace.NearList[i]);
            }
            if (CurrentPlace.CanUseInFirest)
            {
                foreach (var item in AllPlaces)
                {
                    var distance = Vector3.Distance(CurrentPlace.transform.position, item.transform.position);
                    if ((distance > MinNearDist) && (distance < MaxNearDist))
                        res.Add(item.transform);
                }
            }
			break;	
		}
		}
		return res;
	}

	Transform FindPlace(List<Transform> list)
	{
		switch(Level)
		{
			case(0):
			case(1):
			case(3):
			case(5):
			case(7):
			{
				return list[Random.Range(0, list.Count - 1)];
			}
			case(2):
			case(4):
			case(6):
			case(8):
			{
				var res = list[Random.Range(0, list.Count - 1)];
				var angle = Vector3.Angle(Cam.transform.forward, res.transform.position - Cam.transform.position);
				while(angle>70 && list.Count > 0)
				{
					list.Remove(res);
					res = list[Random.Range(0, list.Count)];
					angle = Vector3.Angle(Cam.transform.forward, res.transform.position - Cam.transform.position);
				}
				return res;
			}
		}
		return null;
	}
	
	public void NextLevel()
	{
		Level++;
		if(Level<9)
		{
			dic.TryGetValue(Level, out CurrentSettings);	
			Source.Stop();
			Source.clip = CurrentSettings.Sound;
			Source.Play();
		}
		else
		{
			//win!!!!!!!!!!	
		}
	}
	
	public void SetPlace(SlenderPlaceScript place)
	{
        if (place != null)
            CurrentPlace = place;
	}
	
	public void SetDamage(int delta)
	{
		CurrentHealth -= delta;
	}
}