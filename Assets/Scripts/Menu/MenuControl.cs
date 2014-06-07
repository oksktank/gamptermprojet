using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class MenuControl : MonoBehaviour 
{
	public bool isQuit = false;
	private int sel=0;
	private int num = 0, sound = 0, light = 0, effect = 0;
	private GameObject[] menus = new GameObject[6];
	
	private string path;
	
	protected string text = "";
	protected StreamReader reader = null;
	protected FileInfo theSourceFile = null;
	GameObject Contro;
	String test;
	int flag1=0, flag5 = 0,fcnt = 0;
//	public Color JPtestColor;
//	public int test2;
//	public float test3;
//	public bool test4;
	
	
	void Start()
	{
		Time.timeScale = 1;
		sel = 0;
		menus[0] = GameObject.Find ("Gstart");
		menus[1] = GameObject.Find ("Rank");
		menus[2] = GameObject.Find ("Tutorial");
		menus[3] = GameObject.Find ("Help");
		menus[4] = GameObject.Find ("Option");
		menus[5] = GameObject.Find ("Exit");
		Contro = GameObject.Find ("Controller");
		flag1 = 0;
		flag5 = 0;
		fcnt = 0;
		ReadFile();
		
		menus[sel].SendMessage ("SelectMenu");
		
//		Debug.Log(Gstart.transform.position.y);
	}
	
	void ReadFile()
	{
		path = Application.dataPath + "/opvalue.ini";
		
		if(File.Exists (path) == false)
		{
			MakeFile();
			Debug.Log ("File Not Exist");
		}
		
		theSourceFile = new FileInfo(path);
		reader = theSourceFile.OpenText ();
		
		text = reader.ReadLine ();
		sound = System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		light = System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		effect = System.Convert.ToInt32 (text);
		
		reader.Close ();
	}
	
	void MakeFile()
	{
		path = Application.dataPath + "/opvalue.ini";
		
		var mf = File.CreateText (path);
		mf.WriteLine ("2");
		mf.WriteLine ("0");
		mf.WriteLine ("1");
		mf.Close ();
	}
	
	void Update(){
//		if(count == 20)
//		{
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
			if (sel <4) sel = (sel+2)%6;
		
		}
		else if((test =="FW") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if (sel > 1)sel = sel - 2;
		} 
		else if((test == "WL" || test == "FL") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel % 2 == 1)sel = sel-1;
		}
		else if((test == "WR" || test == "FR") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel % 2 == 0) sel = (sel+1);
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
				Application.LoadLevel (2);
			}
			else if(sel == 1)
			{
				Application.LoadLevel (3);
			}
			else if(sel == 2)
			{
				Application.LoadLevel (12);
			}
			else if(sel == 3)
			{
				Application.LoadLevel (14);
			}
			else if(sel == 4)
			{
				Application.LoadLevel (5);
			}
			else if(sel == 5)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",0);
				Application.Quit ();
			}
		}				
		
		
		menus[sel].SendMessage("releaseMenu");
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			sel = (sel+2)%6;
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			sel = (sel+4)%6;
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			sel = (sel+5)%6;
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			sel = (sel+1)%6;
		}
		menus[sel].SendMessage ("SelectMenu");
		
		if(Input.GetKeyDown (KeyCode.Return))
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			if(sel == 0)
			{
				Application.LoadLevel (2);
			}
			else if(sel == 1)
			{
				Application.LoadLevel (3);
			}
			else if(sel == 2)
			{
				Application.LoadLevel (12);
			}
			else if(sel == 3)
			{
				Application.LoadLevel (14);
			}
			else if(sel == 4)
			{
				Application.LoadLevel (5);
			}
			else if(sel == 5)
			{
				Application.Quit ();
			}
		}
//		}
//		renderer.material.SetColor ("Gstart",Color.blue);
	}
	
}
