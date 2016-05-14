using UnityEngine;
using System.Collections.Generic;

public class PagesController : MonoBehaviour {

	public List<Texture> Textures;

    private GameObject[] list;
    private SlenderController Slender;

    public int Level = 1;

    private RaycastHit hit;
	
	public GameObject Direction;
	
	public AudioSource Source;
	
	public AudioClip TakeClip;
	
	public Finish finish;
	
	void Start () {
		list = GameObject.FindGameObjectsWithTag("Page");
		Slender = (SlenderController)GameObject.FindGameObjectWithTag("Slender").GetComponent(typeof(SlenderController));
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Action"))
		{
			Physics.Raycast(Direction.transform.position, transform.forward, out hit, 5f);
			if (hit.transform != null)
			{
				Debug.Log (hit.transform.tag);
				if(hit.transform.tag == "Page")
				{
					Source.clip = TakeClip;
					Source.Play();
					Source.loop = false;
					GameObject.Destroy(hit.transform.gameObject);
					NextLevel();	
				}
			}
		}
	}
	
	void OnGUI()
	{
		GUI.TextField(new Rect(10,40,70,25), Level - 1 + " Pages");	
	}
	
	public void NextLevel()
	{
		Level++;
		if(Level == 9)
		{
			finish.End(true);
			return;
		}
		foreach(var item in list)
		{
			if(item != null)
				item.renderer.material.SetTexture("_MainTex", Textures[Level]);
		}
		Slender.NextLevel();
	}
}
