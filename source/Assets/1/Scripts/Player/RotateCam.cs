using UnityEngine;
using System.Collections;

public class RotateCam : MonoBehaviour {

	public Camera Camer;

    public float Velosity = 10;
    public float MaxRot = 5;
    public float _currentVelosity = 0;
    public Vector3 Axe = new Vector3(0, 1, 0);

    public float time = 2;
    public float _currentTime = 0;

    public bool IsRun = false;

    private void SetIsRun(bool x)
	{
		IsRun = x;
		_currentTime = 0;
		_currentVelosity = 0;
	}
	
	void Update () {
		if(IsRun)
		{
			_currentTime += Time.deltaTime;
			_currentVelosity += Velosity * Time.deltaTime;
			
			if(Mathf.Abs(_currentVelosity) < Mathf.Abs(MaxRot))
				Camer.transform.RotateAroundLocal(Axe, Velosity * Time.deltaTime);
			
			if(_currentTime >= time)
			{
				_currentTime = 0;
				Velosity *= -1;
				_currentVelosity = 0;
			}
		}
		
	}
}
