using UnityEngine;
using System.Collections;

public class TakeAk : MonoBehaviour {

	public Transform Cam;
    private RaycastHit Hit;
    private bool IsTaked = false;
	public GameObject Ak;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Action") && !IsTaked)
		{
			Physics.Raycast(Cam.transform.position, Cam.transform.forward, out Hit, 5f);
			if(Hit.transform != null)
			{
				Debug.Log(Hit.transform.tag);
				if(Hit.transform.tag == "Ak")
				{
					IsTaked = true;
					Ak.SetActive(true);
					Destroy(this.gameObject);
					foreach(var item in GameObject.FindGameObjectsWithTag("WeaponCrate"))
					{
						((WeaponCrate)item.GetComponent(typeof(WeaponCrate))).Show();	
					}
				}
			}
		}
	}
}
