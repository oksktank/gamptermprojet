using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class OptionControl : MonoBehaviour 
{
	public bool isQuit = false;
	private int sel=0;
//	private int count;
	private GameObject[] menus = new GameObject[11];
	private int sound,light,effect;
	private int sou,lig,eff;
//	public Color JPtestColor;
//	public int test2;
//	public float test3;
//	public bool test4;
	
	private string path;
	protected string text ="";
	protected StreamReader reader = null;
	protected FileInfo theSourceFile = null;
	
	int fcnt=0, flag1=0, flag5=0;
	GameObject Contro;
	String test;
	
	void Start()
	{
//		count = 0;
		sel = 0;
		sound = 4;
		light = 7;
		effect = 9;
		sou = 4;
		lig = 7;
		eff = 9;
		ReadFile ();
		Contro = GameObject.Find ("Controller");
		fcnt = 0;
		flag1 = 0;
		flag5 = 0;
		
		menus[0] = GameObject.Find ("Sound");
		menus[1] = GameObject.Find ("Light");
		menus[2] = GameObject.Find ("Effect");
		menus[3] = GameObject.Find ("Back");
		menus[4] = GameObject.Find ("Small");
		menus[5] = GameObject.Find ("Middle");
		menus[6] = GameObject.Find ("Large");
		menus[7] = GameObject.Find ("Bright");
		menus[8] = GameObject.Find ("Dark");
		menus[9] = GameObject.Find ("On");
		menus[10] = GameObject.Find ("Off");
		
		menus[sel].SendMessage ("SelectMenu");
		menus[sound].SendMessage ("SelectEffect");
		menus[light].SendMessage ("SelectEffect");
		menus[effect].SendMessage ("SelectEffect");
		
//		Debug.Log(Gstart.transform.position.y);
	}
	
	void ReadFile()
	{
		path = Application.dataPath + "/opvalue.ini";
		
		if(File.Exists (path) == false)
		{
			Makedumy();
			Debug.Log ("File Not Exist");
		}
		
		theSourceFile = new FileInfo(path);
		reader = theSourceFile.OpenText ();
		
		text = reader.ReadLine ();
		sound += System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		light += System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		effect += System.Convert.ToInt32 (text);
		
		reader.Close ();
	}
	
	void Makedumy()
	{
		path = Application.dataPath + "/opvalue.ini";
		
		var mf = File.CreateText (path);
		mf.WriteLine (2);
		mf.WriteLine (light - 7);
		mf.WriteLine (1);
		mf.Close ();
	}
	
	void MakeFile()
	{
		path = Application.dataPath + "/opvalue.ini";
		
		var mf = File.CreateText (path);
		mf.WriteLine (sound - 4);
		mf.WriteLine (light - 7);
		mf.WriteLine (effect - 9);
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
		if(fcnt > 25)
		{
			fcnt = 0;
			flag1 = 0;
			flag5 = 0;
		}
		
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
			menus[sel].SendMessage("releaseMenu");
			if(sel == 0) sel = 1;
			else if(sel == 1) sel = 3;
			menus[sel].SendMessage ("SelectMenu");
		
		}
		else if((test =="FW") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			menus[sel].SendMessage("releaseMenu");
			if(sel == 3) sel = 1;
			else if(sel == 1) sel = 0;
			menus[sel].SendMessage ("SelectMenu");
		} 
		else if((test == "WL" || test == "FL") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 0)
			{
				menus[sound].SendMessage ("releaseMenu");
				if(sound > 4) sound--;
				menus[sound].SendMessage ("SelectEffect");
			}
			else if(sel == 1)
			{
				menus[light].SendMessage ("releaseMenu");
				if(light > 7) light--;
				menus[light].SendMessage ("SelectEffect");
			}
			else if(sel == 2)
			{
				menus[effect].SendMessage ("releaseMenu");
				if(effect == 9) effect = 10;
				else effect--;
				menus[effect].SendMessage ("SelectEffect");
			}
		}
		else if((test == "WR" || test == "FR") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			if(sel == 0)
			{
				menus[sound].SendMessage ("releaseMenu");
				if(sound < 6) sound++;
				menus[sound].SendMessage ("SelectEffect");
				
			}
			else if(sel == 1)
			{
				menus[light].SendMessage ("releaseMenu");
				if(light < 8) light++;
				menus[light].SendMessage ("SelectEffect");
			}
			else if(sel == 2)
			{
				menus[effect].SendMessage ("releaseMenu");
				if(effect == 10) effect = 9;
				else effect++;
				menus[effect].SendMessage ("SelectEffect");
			}
		}
		
		if((test[0] == 'I' || test[1] == 'I') && flag5 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 1;
			if(sel == 0)
			{

			}
			else if(sel == 1)
			{

			}
			else if(sel == 2)
			{

			}
			else if(sel == 3)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				MakeFile();
				Application.LoadLevel (0);
			}
		}		
		
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			menus[sel].SendMessage("releaseMenu");
			sel = (sel+1)%4;
			if(sel == 2) sel = 3;
			menus[sel].SendMessage ("SelectMenu");
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			menus[sel].SendMessage("releaseMenu");
			sel = (sel+3)%4;
			if(sel == 2) sel = 1;
			menus[sel].SendMessage ("SelectMenu");
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(sel == 0)
			{
				menus[sound].SendMessage ("releaseMenu");
				if(sound == 4) sound = 6;
				else sound--;
				menus[sound].SendMessage ("SelectEffect");
			}
			else if(sel == 1)
			{
				menus[light].SendMessage ("releaseMenu");
				if(light == 7) light = 8;
				else light--;
				menus[light].SendMessage ("SelectEffect");
			}
			else if(sel == 2)
			{
				menus[effect].SendMessage ("releaseMenu");
				if(effect == 9) effect = 10;
				else effect--;
				menus[effect].SendMessage ("SelectEffect");
			}
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(sel == 0)
			{
				menus[sound].SendMessage ("releaseMenu");
				if(sound == 6) sound = 4;
				else sound++;
				menus[sound].SendMessage ("SelectEffect");
				
			}
			else if(sel == 1)
			{
				menus[light].SendMessage ("releaseMenu");
				if(light == 8) light = 7;
				else light++;
				menus[light].SendMessage ("SelectEffect");
			}
			else if(sel == 2)
			{
				menus[effect].SendMessage ("releaseMenu");
				if(effect == 10) effect = 9;
				else effect++;
				menus[effect].SendMessage ("SelectEffect");
			}
		}
		
		if(sou != sound || lig != light || eff != effect)
		{
			sou = sound;
			lig = light;
			MakeFile ();
			GameObject.Find ("option").SendMessage ("doit");
		}
		
		if(Input.GetKeyDown (KeyCode.Return))
		{
			if(sel == 0)
			{

			}
			else if(sel == 1)
			{

			}
			else if(sel == 2)
			{

			}
			else if(sel == 3)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				MakeFile();
				Application.LoadLevel (0);
			}
		}
	}
}
