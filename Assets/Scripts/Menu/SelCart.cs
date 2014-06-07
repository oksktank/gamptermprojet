using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class SelCart : MonoBehaviour 
{
	private int sel=0;
	private GameObject[] menus = new GameObject[5];
	private GameObject mainout, pig, monkey;
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;
	
	private int playMode, selectCnt, P1Char, P2Char, P1Car, P2Car;

	int fcnt = 0, flag1 = 0, flag5 = 0;
	int selflag = 0;
	int f1;
	String test1, test2;
	GameObject Contro;	
	
	void Start()
	{
		f1 = 0;
		selflag = 0;
		Contro = GameObject.Find ("Controller");
		fcnt = 0;
		flag1 = 0;
		flag1 = 0;
		flag1 = 0;
		flag1 = 0;
		flag5 = 0;
		
		sel = 0;
		menus[0] = GameObject.Find ("Pig_Cart");
		menus[1] = GameObject.Find ("Monkey_Cart");
		menus[2] = GameObject.Find ("Back");
		menus[3] = GameObject.Find ("Shop");
		mainout = GameObject.Find ("main");
		pig = GameObject.Find ("Pig");
		monkey = GameObject.Find ("Monkey");
		
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
		
		text = reader.ReadLine ();
		P1Char = System.Convert.ToInt32 (text);
		
		if(playMode == 2 && selectCnt == 1)
		{
			text = reader.ReadLine ();
			P1Car = System.Convert.ToInt32 (text);
			text = reader.ReadLine ();
			P2Char = System.Convert.ToInt32 (text);
			mainout.SendMessage ("SetText", "Select Cart (2P)");
		}
		else mainout.SendMessage ("SetText", "Select Cart (1P)");
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
		mf.WriteLine ("1");
		mf.WriteLine (P1Char.ToString ());
		mf.WriteLine (P1Car.ToString ());
		
		if(playMode == 2 && selectCnt == 1)
		{
			mf.WriteLine (P2Char.ToString ());
			mf.WriteLine (P2Car.ToString ());
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
		
		if(f1 == 1) selflag++;
		
		
		menus[sel].SendMessage("releaseMenu");
		if(playMode == 2 && selectCnt == 1)
		{

			
			if(test2 == "WW" || (test2[0] != 'W' && test2[1] != 'W'))
			{
				flag1 = 1;
				flag5 = 0;
			}
			if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.DownArrow) || (test2 == "BW" && flag1 != 1))
			{

				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					pig.animation.Stop ("SpinCharacter");
					pig.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					monkey.animation.Stop ("SpinCharacter");
					monkey.transform.rotation = Quaternion.Euler (0,180,0);
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
			else if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.LeftArrow)|| ((test2 == "WL" || test2 == "FL") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					pig.animation.Stop ("SpinCharacter");
					pig.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					monkey.animation.Stop ("SpinCharacter");
					monkey.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.RightArrow) || ((test2 == "WR" || test2 == "FR") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					pig.animation.Stop ("SpinCharacter");
					pig.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					monkey.animation.Stop ("SpinCharacter");
					monkey.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 1;
			}
			
			if(Input.GetKey (KeyCode.I) || Input.GetKey(KeyCode.UpArrow) || (test2 == "FW"))
			{
				if(selflag < 50) f1 = 1;
				else f1 = 0;
			}
			else if(Input.GetKey (KeyCode.J) || Input.GetKey(KeyCode.LeftArrow) || (test2 == "WL" || test2 == "FL"))
			{
				if(selflag >= 50 && selflag < 100) f1 = 1;
				else f1 = 0;
			}
			else if(Input.GetKey (KeyCode.L) || Input.GetKey(KeyCode.RightArrow) || (test2 == "WR" || test2 == "FR"))
			{
				if(selflag >= 100 && selflag < 150) f1 = 1;
				else f1 = 0;
			}
			else if(Input.GetKey (KeyCode.K) || Input.GetKey(KeyCode.DownArrow) || (test2 == "BW"))
			{
				if(selflag >= 150 && selflag < 200) f1 = 1;
				else f1 = 0;
			}
			
			menus[sel].SendMessage ("SelectMenu");
			if(sel == 0) pig.animation.CrossFadeQueued ("SpinCharacter");
			else if(sel == 1) monkey.animation.CrossFadeQueued ("SpinCharacter");
			
			if(selflag >= 50)
			{
				mainout.SendMessage ("SelectEffect");
			}
			if(selflag >= 100)
			{
				menus[0].SendMessage ("SelectEffect");
			}
			if(selflag >= 150)
			{
				menus[1].SendMessage ("SelectEffect");
			}
			if(selflag >= 200)
			{
				menus[2].SendMessage ("SelectEffect");
			}
			
			if(Input.GetKeyDown (KeyCode.Return)|| ((test2[0] == 'I' || test2[1] == 'I') && flag5 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 1;
				if(selflag >= 200)
				{
					if(selflag == 211)
					{
						GameObject.Find ("hidden").SendMessage ("SelectEffect");
					}
					else if(selflag >= 212)
					{
						if(playMode == 2 && selectCnt == 1) P2Car = 3;
						else P1Car = 3;
						GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
						Makefile();
						
						if(playMode == 2 && selectCnt == 2) Application.LoadLevel (8);
						else Application.LoadLevel (10);
					}
					else GameObject.Find ("Setting").transform.position += new Vector3(0,-5,0);
					selflag++;

				}
				else if(sel == 0)
				{
					if(playMode == 2 && selectCnt == 1) P2Car = 1;
					else P1Car = 1;
					GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
					Makefile();
					
					if(playMode == 2 && selectCnt == 2) Application.LoadLevel (8);
					else Application.LoadLevel (10);
				}
				else if(sel == 1)
				{
					if(playMode == 2 && selectCnt == 1) P2Car = 2;
					else P1Car = 2;
					GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
					Makefile();
					
					if(playMode == 2 && selectCnt == 2) Application.LoadLevel (8);
					else Application.LoadLevel (10);
				}
				else if(sel == 2)
				{
					GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
					Application.LoadLevel (8);
				}
			}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		} else{
			if(test1 == "WW" || (test1[0] != 'W' && test1[1] != 'W'))
			{
				flag1 = 1;
				flag5 = 0;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow)|| ((test1 == "BW") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					pig.animation.Stop ("SpinCharacter");
					pig.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					monkey.animation.Stop ("SpinCharacter");
					monkey.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 2;
			}
			else if(Input.GetKeyDown(KeyCode.UpArrow)|| ((test1 =="FW") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 2) sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.LeftArrow)|| ((test1 == "WL" || test1 == "FL") && flag1 != 1))
			{
				if(selflag < 100) selflag++;
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					pig.animation.Stop ("SpinCharacter");
					pig.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					monkey.animation.Stop ("SpinCharacter");
					monkey.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 0;
			}
			else if(Input.GetKeyDown(KeyCode.RightArrow)|| ((test1 == "WR" || test1 == "FR") && flag1 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 0;
				if(sel == 0) {
					pig.animation.Stop ("SpinCharacter");
					pig.transform.rotation = Quaternion.Euler (0,180,0);
				}
				else if(sel == 1){
					monkey.animation.Stop ("SpinCharacter");
					monkey.transform.rotation = Quaternion.Euler (0,180,0);
				}
				sel = 1;
			}

			if(Input.GetKey (KeyCode.UpArrow) || (test1 == "FW"))
			{
				if(selflag < 50) f1 = 1;
				else f1 = 0;
			}
			else if(Input.GetKey (KeyCode.LeftArrow) || (test1 == "WL" || test1 == "FL"))
			{
				if(selflag >= 50 && selflag < 100) f1 = 1;
				else f1 = 0;
			}
			else if(Input.GetKey (KeyCode.RightArrow) || (test1 == "WR" || test2 == "FR"))
			{
				if(selflag >= 100 && selflag < 150) f1 = 1;
				else f1 = 0;
			}
			else if(Input.GetKey (KeyCode.DownArrow) || (test1 == "BW"))
			{
				if(selflag >= 150 && selflag < 200) f1 = 1;
				else f1 = 0;
			}			
			
			menus[sel].SendMessage ("SelectMenu");
			if(sel == 0) pig.animation.CrossFadeQueued ("SpinCharacter");
			else if(sel == 1) monkey.animation.CrossFadeQueued ("SpinCharacter");

			if(selflag >= 50)
			{
				mainout.SendMessage ("SelectEffect");
			}
			if(selflag >= 100)
			{
				menus[0].SendMessage ("SelectEffect");
			}
			if(selflag >= 150)
			{
				menus[1].SendMessage ("SelectEffect");
			}
			if(selflag >= 200)
			{
				menus[2].SendMessage ("SelectEffect");
			}			
			
			if(Input.GetKeyDown (KeyCode.Return)|| ((test1[0] == 'I' || test1[1] == 'I') && flag5 != 1))
			{
				fcnt = 0;
				flag1 = 1;
				flag5 = 1;
				
				if(selflag >= 200)
				{
					if(selflag == 211)
					{
						GameObject.Find ("hidden").SendMessage ("SelectEffect");
					}
					else if(selflag >= 212)
					{
						if(playMode == 2 && selectCnt == 1) P2Car = 3;
						else P1Car = 3;
						GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
						Makefile();
						
						if(playMode == 2 && selectCnt == 2) Application.LoadLevel (8);
						else Application.LoadLevel (10);
					}
					else GameObject.Find ("Setting").transform.position += new Vector3(0,-5,0);
					selflag++;

				}
				else if(sel == 0)
				{
					if(playMode == 2 && selectCnt == 1) P2Car = 1;
					else P1Car = 1;
					GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
					Makefile();
					
					if(playMode == 2 && selectCnt == 2) Application.LoadLevel (8);
					else Application.LoadLevel (10);
				}
				else if(sel == 1)
				{
					if(playMode == 2 && selectCnt == 1) P2Car = 2;
					else P1Car = 2;
					GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
					Makefile();
					
					if(playMode == 2 && selectCnt == 2) Application.LoadLevel (8);
					else Application.LoadLevel (10);
				}
				else if(sel == 2)
				{
					GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
					Application.LoadLevel (8);
				}
			}
		}

//		}
//		renderer.material.SetColor ("Gstart",Color.blue);
	}
}
