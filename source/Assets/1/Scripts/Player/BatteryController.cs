using UnityEngine;
using System.Collections;

public class BatteryController : MonoBehaviour {
	
	public int Count = 0;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Action"))
		{
			foreach(var item in GameObject.FindGameObjectsWithTag("Battery"))
			{
				if(((Battery)item.GetComponent(typeof(Battery))).IsInTrigger)	
				{
					Count++;
					((Battery)item.GetComponent(typeof(Battery))).DisShow();
					int temp = Random.Range(0,6);
					foreach(var item2 in GameObject.FindGameObjectsWithTag("Battery"))
					{
						if(((Battery)item2.GetComponent(typeof(Battery))).IsUnder)
						{
							if(temp > 0)
								temp--;
							else 
							{
								((Battery)item2.GetComponent(typeof(Battery))).Show();
								break;
							}
						}
					}
					break;
				}
			}	
		}
	}
	
	public bool GetBattery()
	{
		if(Count > 0)
		{
			Count--;
			return true;
		}
		else return false;
	}
	
	void OnGUI()
	{
		GUI.TextField(new Rect(10,100,70,20), Count + " Battery");	
	}
}
