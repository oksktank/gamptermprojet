using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class PlayControl : MonoBehaviour 
{
	public bool isQuit = false;
	private int sel=0;
//	private int count;
	private GameObject[] menus = new GameObject[4];
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;
	
	GameObject Contro;
	int flag1=0, flag5=0, fcnt=0;
	String test;
	
//	public Color JPtestColor;
//	public int test2;
//	public float test3;
//	public bool test4;
	
	
	void Start()
	{
		flag1 = 0;
		flag5 = 0;
		fcnt = 0;
		Contro = GameObject.Find ("Controller");
//		count = 0;
		sel = 0;
		menus[0] = GameObject.Find ("Single");
		menus[1] = GameObject.Find ("Tutorial");
		menus[2] = GameObject.Find ("pvp");
		menus[3] = GameObject.Find ("Back");
		
		menus[sel].SendMessage ("SelectMenu");
		
//		Debug.Log(Gstart.transform.position.y);
	}
	
	void MakeSelec(int mode)
	{
		path = Application.dataPath + "/PlayInfo.ini";
		var mf = File.CreateText (path);
		mf.WriteLine (mode.ToString ());
		mf.WriteLine (mode.ToString ());
		mf.Close ();
	}
	
	void Update(){
//		count++;
//		if(count == 20)
//		{
//			count = 0;
		test = Contro.GetComponent<Controller>().direction1;
	
		if(test.Length <= 0) test = "PP";
		fcnt++;
		if(fcnt > 20)
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
			sel = 3;
//			if(sel < 2) sel = (sel+2);
		}
		else if((test =="FW") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 3) sel = 0;
//			if(sel > 1) sel = (sel-2);
			
		} 
		else if((test == "WL" || test == "FL" )&& flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			
			sel = 0;
			
//			if(sel == 1) sel = 0;
//			else if(sel == 3) sel = 2;
		}
		else if((test == "WR" || test == "FR") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			
			sel = 2;
			
//			if(sel == 0) sel = 1;
//			else if(sel == 2) sel = 3;
		}
		menus[sel].SendMessage ("SelectMenu");
		
		if((test[0] == 'I' || test[1] == 'I') && flag5 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag1 = 1;
			flag5 = 1;
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			if(sel == 0)
			{
				MakeSelec(1);
				Application.LoadLevel (8);
			}
			else if(sel == 1)
			{
				Application.LoadLevel (12);
			}
			else if(sel == 2)
			{
				MakeSelec(2);
				Application.LoadLevel (8);
			}
			else if(sel == 3)
			{
				Application.LoadLevel (0);
			}
		}		
		
		menus[sel].SendMessage("releaseMenu");
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			sel = 3;
//			sel = (sel+2)%4;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			sel = 0;
//			sel = (sel+2)%4;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			sel = 0;
//			sel = (sel+3)%4;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			sel = 2;
//			sel = (sel+1)%4;
		}
		menus[sel].SendMessage ("SelectMenu");
		if(Input.GetKeyDown (KeyCode.Return))
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			if(sel == 0)
			{
				MakeSelec(1);
				Application.LoadLevel (8);
			}
			else if(sel == 1)
			{
				Application.LoadLevel (15);
			}
			else if(sel == 2)
			{
				MakeSelec(2);
				Application.LoadLevel (8);
			}
			else if(sel == 3)
			{
				Application.LoadLevel (0);
			}

		}
//		}
//		renderer.material.SetColor ("Gstart",Color.blue);
	}
}
