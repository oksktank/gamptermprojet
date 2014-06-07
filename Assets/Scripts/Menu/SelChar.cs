using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class SelChar : MonoBehaviour 
{
	private int sel=0;
	private GameObject[] menus = new GameObject[4];
	private GameObject mainout, boy, girl;
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;
	
	private int playMode, selectCnt, P1Char, P2Char, P1Car, P2Car;

	int fcnt = 0, flag1 = 0, flag5 = 0;
	String test1, test2;
	GameObject Contro;	
	
	void Start()
	{
		Contro = GameObject.Find ("Controller");
		fcnt = 0;
		flag1 = 0;

		flag5 = 0;
		
		sel = 0;
		menus[0] = GameObject.Find ("Baby_M");
		menus[1] = GameObject.Find ("Baby_W");
		menus[2] = GameObject.Find ("Back");
//		menus[3] = GameObject.Find ("");
		mainout = GameObject.Find ("main");
		
		boy = GameObject.Find ("baby_m");
		girl = GameObject.Find ("baby_g");
		
		menus[sel].SendMessage ("SelectMenu");
		
		ReadInfo();
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
		
		if(playMode == 2 && selectCnt == 1)
		{
			text = reader.ReadLine ();
			P1Char = System.Convert.ToInt32 (text);
			text = reader.ReadLine ();
			P1Car = System.Convert.ToInt32 (text);
			mainout.SendMessage ("SetText", "Select Baby (2P)");
		}
		else{
			mainout.SendMessage ("SetText", "Select Baby (1P)");
		}
		reader.Close ();
		
	}
	
	void Makedumy()
	{
		path = Application.dataPath + "/PlayInfo.ini";
		var mf = File.CreateText (path);
		mf.WriteLine ("2");
		mf.WriteLine ("2");
		mf.WriteLine ("1");
		mf.WriteLine ("1");
		mf.Close ();	
	}
	
	void Makefile()
	{
		path = Application.dataPath + "/PlayInfo.ini";
		var mf = File.CreateText (path);
		
		mf.WriteLine (playMode.ToString ());
		mf.WriteLine (selectCnt.ToString ());
		mf.WriteLine (P1Char.ToString ());
		
		if(playMode == 2 && selectCnt == 1)
		{
			mf.WriteLine (P1Car.ToString ());
			mf.WriteLine (P2Char.ToString ());
		}
		
		mf.Close ();
	}
	
	void Update(){
		test1 = Contro.GetComponent<Controller>().direction1;
		test2 = Contro.GetComponent<Controller>().direction2;
		
		if(test1.Length <= 0) test1 = "PP";
		if(test2.Length <=0) test2 = "PP";
		
		fcnt++;
		if(fcnt > 25)
		{
			fcnt = 0;
			flag1 = 0;
			flag5 = 0;
		}		
		
		
		menus[sel].SendMessage("releaseMenu");
		if(playMode == 2 && selectCnt == 1)
		{
			if(test2 == "WW" || (test2[0] != 'W' && test2[1] != 'W'))
			{
				flag1 = 1;
				flag5 = 0;
			}
			if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.DownArrow )  || (test2 == "BW" && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					boy.animation.Stop ("SpinCharacter");
					boy.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					girl.animation.Stop ("SpinCharacter");
					girl.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 2;
			}
			else if(Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.UpArrow) || (test2 == "FW" && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 2) sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.LeftArrow) || ((test2 == "WL" || test2 == "FL") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					boy.animation.Stop ("SpinCharacter");
					boy.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					girl.animation.Stop ("SpinCharacter");
					girl.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.RightArrow) || ((test2 == "WR" || test2 == "FR") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					boy.animation.Stop ("SpinCharacter");
					boy.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					girl.animation.Stop ("SpinCharacter");
					girl.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 1;
			}
			
			
			menus[sel].SendMessage ("SelectMenu");
			if(sel == 0) boy.animation.CrossFadeQueued ("SpinCharacter");
			else if(sel == 1) girl.animation.CrossFadeQueued ("SpinCharacter");
			
			if(Input.GetKeyDown (KeyCode.Return) || ((test2[0] == 'I' || test2[1] == 'I') && flag5 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 1;
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				if(sel == 0)
				{
					if(playMode == 2 && selectCnt == 1) P2Char = 1;
					else P1Char = 1;
					
					Makefile();
					Application.LoadLevel (9);
				}
				else if(sel == 1)
				{
					if(playMode == 2 && selectCnt == 1) P2Char = 2;
					else P1Char = 2;
					
					Makefile();
					Application.LoadLevel (9);
				}
				else if(sel == 2)
				{
					Application.LoadLevel (2);
				}
			}
			
		} else{
			if(test1 == "WW" || (test1[0] != 'W' && test1[1] != 'W'))
			{
				flag1 = 1;
				flag5 = 0;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow) || ((test1 == "BW") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					boy.animation.Stop ("SpinCharacter");
					boy.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					girl.animation.Stop ("SpinCharacter");
					girl.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 2;
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow) || ((test1 =="FW") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 2) sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow) || ((test1 == "WL" || test1 == "FL") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					boy.animation.Stop ("SpinCharacter");
					boy.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					girl.animation.Stop ("SpinCharacter");
					girl.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.RightArrow) || ((test1 == "WR" || test1 == "FR") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					boy.animation.Stop ("SpinCharacter");
					boy.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					girl.animation.Stop ("SpinCharacter");
					girl.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 1;
			}
			
			
			menus[sel].SendMessage ("SelectMenu");
			if(sel == 0) boy.animation.CrossFadeQueued ("SpinCharacter");
			else if(sel == 1) girl.animation.CrossFadeQueued ("SpinCharacter");
			
			if(Input.GetKeyDown (KeyCode.Return) || ((test1[0] == 'I' || test1[1] == 'I') && flag5 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 1;
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				if(sel == 0)
				{
					if(playMode == 2 && selectCnt == 1) P2Char = 1;
					else P1Char = 1;
					
					Makefile();
					Application.LoadLevel (9);
				}
				else if(sel == 1)
				{
					if(playMode == 2 && selectCnt == 1) P2Char = 2;
					else P1Char = 2;
					
					Makefile();
					Application.LoadLevel (9);
				}
				else if(sel == 2)
				{
					Application.LoadLevel (2);
				}
			}
		}

//		}
//		renderer.material.SetColor ("Gstart",Color.blue);
	}
}
