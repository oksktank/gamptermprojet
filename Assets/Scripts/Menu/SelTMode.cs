using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class SelTMode : MonoBehaviour 
{
	private int sel=0;
	private GameObject[] menus = new GameObject[3];
	private GameObject mainout, pig, monkey;
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;
	
	private string path1;
	private StreamReader reader1 = null;
	private FileInfo theSourceFile1 = null;
	
	private int playMode, selectCnt, P1Char, P2Char, P1Car, P2Car;
	
	int fcnt = 0, flag1 = 0, flag5 = 0;
	String test;
	GameObject Contro;

	void Start()
	{
		Contro = GameObject.Find ("Controller");
		fcnt = 0;
		flag1 = 0;
		flag5 = 0;
		
		sel = 0;
		menus[0] = GameObject.Find ("Single_Play");
		menus[1] = GameObject.Find ("Multi_Play");
		menus[2] = GameObject.Find ("Back");
//		menus[3] = GameObject.Find ("");
		mainout = GameObject.Find ("main");
		
		menus[sel].SendMessage ("SelectMenu");
		mainout.SendMessage ("SetText", "Select Tutorial Mode");
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
		
		path1 = Application.dataPath + "/Map.ini";
		mf = File.CreateText (path1);
		mf.WriteLine ("4");
		mf.Close ();
	}
	
	void Update(){

		
		test = Contro.GetComponent<Controller>().direction1;
	
		if(test.Length <= 0) test = "PP";
		fcnt++;
		if(fcnt > 25)
		{
			fcnt = 0;
			flag1 = 0;
			flag5 = 0;
		}
		
		menus[sel].SendMessage("releaseMenu");
		if(test == "WW" || (test[0] != 'W' && test[1] != 'W'))
		{
			flag1 = 1;
			flag5 = 0;
		}
		else if((test == "BW") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			sel = 2;
		
		}
		else if((test =="FW") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 2) sel = 0;
		} 
		else if((test == "WL" || test == "FL") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			sel = 0;
		}
		else if((test == "WR" || test == "FR") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			sel = 1;
		}

		menus[sel].SendMessage ("SelectMenu");
		if((test[0] == 'I' || test[1] == 'I') && flag5 != 1)
		{
			fcnt = 0;
			
			flag1 = 1;
			flag5 = 1;
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			if(sel == 0)
			{
				playMode = 1;
				selectCnt = 1;
				P1Char = 2;
				P1Car = 1;
				
				Makefile();
				
				Application.LoadLevel (15);
			}
			else if(sel == 1)
			{
				playMode = 2;
				selectCnt = 1;
				P1Char = 1;
				P1Car = 2;
				P2Char = 2;
				P2Car = 2;
				
				Makefile();
				
				Application.LoadLevel (15);
			}
			else if(sel == 2)
			{
				Application.LoadLevel (0);
			}
		}		

		
		menus[sel].SendMessage("releaseMenu");
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			sel = 2;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(sel == 2) sel = 0;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			sel = 0;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			sel = 1;
		}
		menus[sel].SendMessage ("SelectMenu");
		
		if(Input.GetKeyDown (KeyCode.Return))
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			if(sel == 0)
			{
				playMode = 1;
				selectCnt = 1;
				P1Char = 2;
				P1Car = 1;
				
				Makefile();
				
				Application.LoadLevel (15);
			}
			else if(sel == 1)
			{
				playMode = 2;
				selectCnt = 1;
				P1Char = 1;
				P1Car = 2;
				P2Char = 2;
				P2Car = 2;
				
				Makefile();
				
				Application.LoadLevel (15);
			}
			else if(sel == 2)
			{
				Application.LoadLevel (0);
			}
		}
//		}
//		renderer.material.SetColor ("Gstart",Color.blue);
	}
}
