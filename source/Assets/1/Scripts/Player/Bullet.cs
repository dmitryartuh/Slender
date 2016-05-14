using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private float Velocity = 1;
	public GameObject WallSledPrefab;
	public GameObject BodySledPrefad;
	private RaycastHit hit;
    private float LiveTime = 5;
    private float _currentTime = 0;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		_currentTime += Time.deltaTime;
		if(_currentTime >= LiveTime)
			GameObject.Destroy(this.gameObject);
		transform.Translate(0, 0, Velocity);
		if(Physics.Raycast(transform.position, transform.forward, out hit, 10f))
		{
			if(hit.transform.tag == "Wall")
			{
				GameObject.Destroy(Instantiate(WallSledPrefab, hit.point, Quaternion.identity), 0.5f);
				GameObject.Destroy(this.gameObject);
			}
			if(hit.transform.tag == "Slender")
			{
				GameObject.Destroy(Instantiate(BodySledPrefad, hit.point, Quaternion.identity), 0.5f);
				GameObject.Destroy(this.gameObject);
				((SlenderController)GameObject.FindGameObjectWithTag("Slender").GetComponent(typeof(SlenderController))).SetDamage(1);
			}
		}
	}
}
