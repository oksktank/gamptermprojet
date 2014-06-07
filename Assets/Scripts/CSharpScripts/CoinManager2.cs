using UnityEngine;
using System.Collections;

public class CoinManager2 : MonoBehaviour {
	Vector3 pos;
	int flag;
	int flag1;
	int timer;
//	GameObject[] coinN = new GameObject[6];
	string text;
	int i;
	GameObject car1, car2;
	GameObject ran;

	// Use this for initialization
	void Start () {
//		for(i=0;i<6;i++)
	//	{
//			text = "coins" + (i+1);
	//		coinN[i] = GameObject.Find (text);
//		}
		flag = 0;
		timer = 400; 
		car1 = GameObject.Find ("pig_cart_1p");
		ran = GameObject.Find ("Coins3");
	}
	
	// Update is called once per frame
	void Update () {
		timer++;
//			Debug.Log ("po : " + car1.transform.position);
//			Debug.Log ("poo : " + GameObject.Find ("Coins1").transform.position);
		if(timer >= 800)
		{
			timer = 0;
			
			ran.transform.position = car1.transform.position;
	//		GameObject.Find ("Coins1").transform.position = car1.transform.position;

//			flag++;
//			if(flag == 3) flag = 0;
//			coinN[flag].transform.position = car1.transform.position;
//			coinN[flag+3].transform.position = car2.transform.position;

		}
	}
}
