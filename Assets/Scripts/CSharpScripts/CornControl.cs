using UnityEngine;
using System.Collections;

public class CornControl : MonoBehaviour {
	Vector3 pos;
	int flag;
	int timer;

	// Use this for initialization
	void Start () {
		pos = renderer.transform.position;
		flag = 1;
		timer = 0;
	}

	
	void reLoad()
	{
		renderer.transform.position = pos;
	}
	
	// Update is called once per frame
	void Update () {
		if(flag == 1)
		{
			timer++;
			if(timer > 100)
			{
				timer = 0;
				flag = 0;
				reLoad ();
			}
		}
	}
}
