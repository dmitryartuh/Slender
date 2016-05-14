using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour {
	
	public CharacterMotor Motor;

    public float WaitTime = 1f;
    public float Currenttime = 0;
	
	public bool IsDead = false;
	public bool IsWin = false;
	
	void Update()
	{
		if(IsDead)
		{
			Currenttime += Time.deltaTime;
			if(Currenttime >= WaitTime)
			{
				Screen.showCursor = true;
				if(IsWin)
					Application.LoadLevel(3);
				else Application.LoadLevel(2);
			}
		}
	}
	
	public void End(bool IsVin)
	{
		IsDead = true;
		Motor.canControl = false;
		IsWin = IsVin;
	}
}
