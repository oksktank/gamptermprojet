using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class SelMap : MonoBehaviour {

	private int sel=0;
	private GameObject[] menus = new GameObject[4];
	private GameObject mainout;
	private GameObject M1, M2, M3;
	
	private string path;
	
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
		menus[0] = GameObject.Find ("Map1");
		menus[1] = GameObject.Find ("Map2");
		menus[2] = GameObject.Find ("Map3");
		menus[3] = GameObject.Find ("Back");
		mainout = GameObject.Find ("main");
		M1 = GameObject.Find ("M1");
		M2 = GameObject.Find ("M2");
		M3 = GameObject.Find ("M3");
		
		menus[sel].SendMessage ("SelectMenu");
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
		if(Input.GetKeyDown(KeyCode.DownArrow) || ((test == "BW") && flag1 != 1))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			
			if(sel == 0)
			{
				M1.animation.Stop ("SpinMap1");
				M1.transform.rotation = Quaternion.Euler (6,85,314);
			}
			else if(sel == 1){
				M2.animation.Stop ("SpinMap2");
				M2.transform.rotation = Quaternion.Euler (334,305,45);			
			}
			else if(sel == 2){
				M3.animation.Stop ("SpinMap3");
				M3.transform.rotation = Quaternion.Euler (29,0,0);
			}
			sel = 3;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) || ((test =="FW") && flag1 != 1))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 3) sel = 0;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow) || ((test == "WL" || test == "FL") && flag1 != 1))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 0)
			{
				M1.animation.Stop ("SpinMap1");
				M1.transform.rotation = Quaternion.Euler (6,85,314);
			}
			else if(sel == 1){
				M2.animation.Stop ("SpinMap2");
				M2.transform.rotation = Quaternion.Euler (334,305,45);			
			}
			else if(sel == 2){
				M3.animation.Stop ("SpinMap3");
				M3.transform.rotation = Quaternion.Euler (29,0,0);
			}
			if(sel > 0 && sel < 3) sel--;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow) || ((test == "WR" || test == "FR") && flag1 != 1))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 0)
			{
				M1.animation.Stop ("SpinMap1");
				M1.transform.rotation = Quaternion.Euler (6,85,314);
			}
			else if(sel == 1){
				M2.animation.Stop ("SpinMap2");
				M2.transform.rotation = Quaternion.Euler (334,305,45);			
			}
			else if(sel == 2)
			{
				M3.animation.Stop ("SpinMap3");
				M3.transform.rotation = Quaternion.Euler (29,0,0);
			}
			if(sel < 2) sel++;
		}

		menus[sel].SendMessage ("SelectMenu");
		
		if(sel == 0)
		{
			M1.animation.CrossFade ("SpinMap1");
		}
		else if(sel == 1)
		{
			M2.animation.CrossFade ("SpinMap2");
		}
		else if(sel == 2)
		{
			M3.animation.CrossFade ("SpinMap3");
		}
		
		if(Input.GetKeyDown (KeyCode.Return) || ((test[0] == 'I' || test[1] == 'I') && flag5 != 1))
		{
			if(sel == 0)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",2);
				MakeMapFile(1);
				Application.LoadLevel (6);
			}
			else if(sel == 1)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",2);
				MakeMapFile(2);
				Application.LoadLevel (11);
			}
			else if(sel == 2)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",2);
				MakeMapFile(3);
				Application.LoadLevel (13);
			}
			else if(sel == 3)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				Application.LoadLevel (9);
			}
		}
//		}
//		renderer.material.SetColor ("Gstart",Color.blue);
	}
	
	void MakeMapFile(int num)
	{
		path = Application.dataPath + "/Map.ini";
		var mf = File.CreateText (path);
		mf.WriteLine (num);
		mf.Close ();
	}
}
