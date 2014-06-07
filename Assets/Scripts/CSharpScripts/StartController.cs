using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class StartController : MonoBehaviour {	
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;

	private string path1;
	private StreamReader reader1 = null;
	private FileInfo theSourceFile1 = null;
	
	private int playMode, selectCnt, P1Char, P2Char, P1Car, P2Car;
	private int timemin = -1;
	private int Map;
	
	protected int MM, SS, DS;
	private int TT;
	private int Ltime;
	
	// Use this for initialization
	void Start () {
		timemin = -1;
		ReadInfo();
		ReadMap();
		TT = 0;
		
		if(playMode == 1) SetTimer();
		if(Map > 2)
		{	
			timemin = 12000;
//		 	TT = 1;
		}
		
//		GameObject.Find ("pig_cart_1p").SendMessage ("SetItem");
		

		
		GameObject.Find ("Timer").SendMessage ("Getmin", timemin);
		GameObject.Find ("Timer2").SendMessage ("Getmin",timemin);
		
		if(P1Car == 1)
		{
			Destroy(GameObject.Find ("monkey_cart_body1"));
			Destroy (GameObject.Find ("Shopping Cart1"));
			GameObject.Find ("pig_cart_1p").SendMessage ("SetCart", 2);
		}
		else if(P1Car == 2)
		{
			Destroy (GameObject.Find ("Shopping Cart1"));
			Destroy (GameObject.Find ("pig_body1"));
			GameObject.Find ("pig_cart_1p").SendMessage ("SetCart",1);
		}
		else
		{
			GameObject.Find ("pig_cart_1p").SendMessage ("SetCart",3);
			Destroy (GameObject.Find ("pig_body1"));
			Destroy (GameObject.Find ("monkey_cart_body1"));
		}
		
		if(P1Char == 1)
		{
			Destroy (GameObject.Find ("baby_g1"));
			if(P1Car == 3) GameObject.Find ("baby_m1").transform.position += new Vector3 (0,0.7f,0);
		}
		else
		{
			Destroy (GameObject.Find ("baby_m1"));
			if(P1Car == 3) GameObject.Find ("baby_g1").transform.position += new Vector3(0,0.7f,0);
		}
		
		if(playMode == 2)
		{
			if(P2Car == 1)
			{
				Destroy(GameObject.Find ("monkey_cart_body2"));
				Destroy (GameObject.Find ("Shopping Cart2"));
				GameObject.Find ("pig_cart_2p").SendMessage ("SetCart",2);
			}
			else if(P2Car == 2){
				Destroy (GameObject.Find ("Shopping Cart2"));
				Destroy (GameObject.Find ("pig_body2"));
				GameObject.Find ("pig_cart_2p").SendMessage ("SetCart",1);
			}
			else
			{
				GameObject.Find ("pig_cart_2p").SendMessage ("SetCart",3);
				Destroy (GameObject.Find ("monkey_cart_body2"));
				Destroy (GameObject.Find ("pig_body2"));
				
			}
			
			if(P2Char == 1){
				Destroy (GameObject.Find ("baby_g2"));
				if(P2Car == 3) GameObject.Find ("baby_m2").transform.position += new Vector3(0,0.7f,0);
			}
			else
			{
				if(P2Car == 3) GameObject.Find ("baby_g2").transform.position += new Vector3(0,0.7f,0);
				Destroy (GameObject.Find ("baby_m2"));
			}
			
			GameObject.Find ("Readygo").SendMessage ("GetMap", Map);
			Destroy (GameObject.Find ("Readygo2"));
			Destroy (GameObject.Find ("Timer2"));
			Destroy (GameObject.Find ("Camera_single"));
		}
		else
		{
			Destroy (GameObject.Find ("Barrier2"));
			Destroy (GameObject.Find ("Turtle2"));
			Destroy (GameObject.Find ("UNAssigned2"));
			Destroy (GameObject.Find ("Shield2"));
			Destroy (GameObject.Find ("Booster2"));
			Destroy (GameObject.Find ("Recovery2"));
			
			Destroy (GameObject.Find ("pig_cart_2p"));
			Destroy (GameObject.Find ("Camera_2p"));
//			GameObject.Find ("Camera_1p").SendMessage ("SetMode");
			Destroy (GameObject.Find ("Camera_1p"));
			GameObject.Find ("Readygo2").SendMessage ("GetMap",Map);
			Destroy (GameObject.Find ("Readygo"));
			Destroy (GameObject.Find ("Timer"));
		}
		
			playMode = playMode + (Map*10);
			GameObject.Find ("pig_cart_1p").SendMessage ("SetMode", playMode);
			GameObject.Find ("RealRace").SendMessage ("SetMode", playMode);
			GameObject.Find ("WaypointFinal").SendMessage ("SetMode", playMode);
	}
	
	void ReadInfo()
	{
		string text;
		path = Application.dataPath + "/PlayInfo.ini";
		
		if(File.Exists (path) == false) Makedumy();
		
		theSourceFile = new FileInfo(path);
		reader = theSourceFile.OpenText ();
		
		text = reader.ReadLine ();
		playMode = System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		selectCnt = System.Convert.ToInt32 (text);

		text = reader.ReadLine ();
		P1Char = System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		P1Car = System.Convert.ToInt32 (text);
		
		if(playMode == 2)
		{
			text = reader.ReadLine ();
			P2Char = System.Convert.ToInt32 (text);
			text = reader.ReadLine ();
			P2Car = System.Convert.ToInt32 (text);
		}
		reader.Close ();
	}
	void ReadMap()
	{
		string text;
		path1 = Application.dataPath + "/Map.ini";
		
		if(File.Exists (path1) == false) Map = 1;
		
		theSourceFile1 = new FileInfo(path1);
		reader1 = theSourceFile1.OpenText ();
		
		text = reader1.ReadLine ();
		Map = System.Convert.ToInt32 (text);
		
		reader1.Close ();
	}
	
	void Makedumy()
	{
		path = Application.dataPath + "/PlayInfo.ini";
		var mf = File.CreateText (path);
		mf.WriteLine ("2");
		mf.WriteLine ("1");
		mf.WriteLine ("2");
		mf.WriteLine ("1");
		mf.WriteLine("1");
		mf.WriteLine ("2");
		mf.Close ();
	}
	
	void ReadTime()
	{
		int i;
		string txt;
		
		if(Map == 1)
		{
			path = Application.dataPath + "/score1.ini";
		}
		else if(Map == 2)
		{
			path = Application.dataPath + "/score2.ini";
		}
		else if(Map == 3)
		{
			path = Application.dataPath + "/score3.ini";
		}
		
		if(File.Exists (path) == true)
		{
			theSourceFile = new FileInfo(path);
			reader = theSourceFile.OpenText ();
			
			for(i=0;i<10;i++)
			{
				txt = reader.ReadLine ();
				txt = reader.ReadLine ();
				Ltime = System.Convert.ToInt32 (txt);
				if(Ltime == -1) break;
				else if(Ltime > timemin)
				{
					timemin = Ltime;
				}
			}
			if(timemin == -1) timemin = 20000;
			reader.Close ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
		{
			if( Time.timeScale == 0) Time.timeScale = 1;
			else Time.timeScale = 0;
		}
		
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			if(Time.timeScale == 0) Application.LoadLevel(0);
		}
		
//		Debug.Log("playMode : " + playMode + "  Map : " + Map);
//		Debug.Log ("asdfsdf    " + TT);
		if(TT > 0)
		{
			TT++;
			if(TT > 300)
			{
				TT = 1;
				GameObject.Find ("pig_cart_1p").SendMessage ("SetItem");
				if(playMode == 2) GameObject.Find ("pig_cart_2p").SendMessage ("SetItem");
			}
		}
	}
	
	void SetTimer()
	{
		
		ReadTime();
		
	}
}