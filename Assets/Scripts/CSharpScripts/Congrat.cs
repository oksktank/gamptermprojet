using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class Congrat : MonoBehaviour {
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;

	private string path1;
	private StreamReader reader1 = null;
	private FileInfo theSourceFile1 = null;
	
	private int playMode, selectCnt, P1Char, P2Char, P1Car, P2Car;
	private int winner;
	private int timemin = -1;
	
	public Texture2D winimg;
	public Texture2D p1;
	public Texture2D p2;
	
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		
		Invoke("nextScene", 9);
		
		ReadInfo();
		ReadResult();
		if(winner == 1){
			Destroy(GameObject.Find ("pig_cart_2p"));
			if(P1Car == 1)
			{
				Destroy(GameObject.Find ("monkey_cart_body1"));
				Destroy(GameObject.Find ("Shopping Cart1"));
			}
			else if(P1Car == 2)
			{
				Destroy (GameObject.Find ("pig_body1"));
				Destroy(GameObject.Find ("Shopping Cart1"));
			}
			else{
				Destroy(GameObject.Find ("pig_body1"));
				Destroy(GameObject.Find ("monkey_cart_body1"));
				Destroy(GameObject.Find ("WheelTransforms1"));
			}
			
			if(P1Char == 1)
			{
				Destroy (GameObject.Find ("baby_g1"));
				Destroy(GameObject.Find ("baby_g3"));
				if(P1Car == 3)
				{
					Destroy(GameObject.Find ("baby_m1"));
				}
				else{
					Destroy(GameObject.Find ("baby_m3"));
				}
			}
			else 
			{
				Destroy (GameObject.Find ("baby_m1"));
				Destroy(GameObject.Find ("baby_m3"));
				if(P1Car == 3)
				{
					Destroy(GameObject.Find ("baby_g1"));
				}
				else{
					Destroy(GameObject.Find ("baby_g3"));
				}
			}
		}
		else if(winner == 2){
			Destroy(GameObject.Find ("pig_cart_1p"));	
			if(playMode == 2)
			{
				if(P2Car == 1)
				{
					Destroy(GameObject.Find ("monkey_cart_body2"));
					Destroy(GameObject.Find ("Shopping Cart2"));
				}
				else if(P2Car == 2){
					Destroy (GameObject.Find ("pig_body2"));
					Destroy(GameObject.Find ("Shopping Cart2"));
				}
				else{
					Destroy(GameObject.Find ("pig_body2"));
					Destroy(GameObject.Find ("monkey_cart_body2"));
					Destroy(GameObject.Find ("WheelTransforms2"));

				}
				
				if(P2Char == 1){
					Destroy (GameObject.Find ("baby_g2"));
					Destroy(GameObject.Find ("baby_g4"));
					if(P2Car == 3)
					{
						Destroy(GameObject.Find ("baby_m2"));
					}
					else{
						Destroy(GameObject.Find ("baby_m4"));
					}
				}
				else{
					Destroy (GameObject.Find ("baby_m2"));
					Destroy(GameObject.Find ("baby_m4"));
					if(P2Car == 3)
					{
						Destroy(GameObject.Find ("baby_g2"));
					}
					else{
						Destroy(GameObject.Find ("baby_g4"));
					}
				}
			}
		}
	
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
	
	void ReadResult()
	{
		string text;
		path1 = Application.dataPath + "/Gresult.ini";
		
		if(File.Exists (path1) == false) winner = 1;
		
		theSourceFile1 = new FileInfo(path1);
		reader1 = theSourceFile1.OpenText ();
		
		text = reader1.ReadLine ();
		winner = System.Convert.ToInt32 (text);
		
		reader1.Close ();
	}

	
	void Makedumy(){
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
	
	void nextScene () {
		Application.LoadLevel(4);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI(){
		
		GUI.DrawTexture(new Rect(Screen.width/2 - 500, 50,1000,200), winimg);
		if(winner == 1){
			GUI.DrawTexture(new Rect(Screen.width/2-65 ,200 ,130,140), p1);
		}
		else if(winner == 2){
			GUI.DrawTexture(new Rect(Screen.width/2-70 ,200 ,140, 130), p2);	
		}
	}
}
