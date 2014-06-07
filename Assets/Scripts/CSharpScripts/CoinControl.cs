using UnityEngine;
using System.Collections;

public class CoinControl : MonoBehaviour {
	Vector3 pos;
	int flag;
	int flag1;
	int timer;
	

	// Use this for initialization
	void Start () {
		flag1 = 0;
		pos = renderer.transform.position;
		flag = 0;
		timer = 0; 
	}
	
	void getItem()
	{
		renderer.transform.position = new Vector3(-150,-100,-300);
//		flag = 1;
	}
	
	void reLoad()
	{
//		renderer.transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		if(flag == 1)
		{
			timer++;
			if(timer == 300)
			{
				timer = 0;
				flag = 0;
				reLoad ();
			}
		}
	}
}
