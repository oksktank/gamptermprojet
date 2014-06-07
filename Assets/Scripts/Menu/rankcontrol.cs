using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class rankcontrol : MonoBehaviour 
{
	public bool isQuit = false;
	private int sel=1;
	private int num=0;
	private GameObject[] menus = new GameObject[3];
	private GameObject[] name = new GameObject[11];
	private GameObject[] time = new GameObject[10];
	
	private int tmp;
	private int i;
	private string ex, timeout;
	private string path, path1;
	private int MM, SS, DS;
	
	protected string text ="";
	protected StreamReader reader = null;
	protected FileInfo theSourceFile = null;
	protected StreamReader reader1 = null;
	protected FileInfo theSourceFile1 = null;
	
	GameObject Contro;
	String test;
	int flag1=0, flag3=0, fcnt=0;
	
	void Start()
	{
		Time.timeScale = 1;
		num = 0;
		sel = 1;
		GetMap ();
		if(num == 3) num = 0;
		if(num == 2) GameObject.Find ("Time").SendMessage ("SetText", "Score");
		else GameObject.Find ("Time").SendMessage ("SetText", "Time");
		menus[0] = GameObject.Find ("Prev");
		menus[1] = GameObject.Find ("Back");
		menus[2] = GameObject.Find ("Next");
		Contro = GameObject.Find ("Controller");
		flag1 = 0;
		flag1 = 0;
		flag3 = 0;
		fcnt = 0;
		for(i=1;i<=10;i++)
		{
			ex = i + "name";
			name[i-1] = GameObject.Find (ex);
			ex = i + "time";
			time[i-1] = GameObject.Find (ex);
		}
		name[10] = GameObject.Find ("Mapnum");
		
		menus[sel].SendMessage ("SelectMenu");
		
		showrank ();
		
//		Debug.Log(Gstart.transform.position.y);
	}
	
	void GetMap()
	{
		path1 = Application.dataPath + "/Map.ini";
		if(File.Exists (path1) == false)
		{
			num = 0;
		}
		else{
		theSourceFile1 = new FileInfo(path1);
		reader1=theSourceFile1.OpenText();
		
		text = reader1.ReadLine ();
		num = System.Convert.ToInt32 (text);
		num = num-1;
		
		reader1.Close ();}
	}
	
	void ReadFile()
	{
		if(num == 0)
		{
			path = Application.dataPath + "/score1.ini";
		}
		else if(num == 1)
		{
			path = Application.dataPath + "/score2.ini";
		}
		else if(num == 2)
		{
			path = Application.dataPath + "/score3.ini";
		}
		
		if(File.Exists (path) == false)
		{
			MakeFile();
			Debug.Log ("File Not Exist");
		}
		
		theSourceFile = new FileInfo(path);
		reader=theSourceFile.OpenText ();
		
		for(int i=0;i<10;i++)
		{
			text=reader.ReadLine ();
			if(text == null) break;
			name[i].SendMessage ("SetText",text);

			text=reader.ReadLine ();
			tmp = System.Convert.ToInt32 (text);
			
			if(num == 2)
			{
				if(tmp == -1) time[i].SendMessage ("SetText", "-----");
				else{
					timeout = (30000 - tmp).ToString ();
					time[i].SendMessage ("SetText", timeout);
				}

				continue;
			}
			
			if(tmp == -1) time[i].SendMessage ("SetText", "-- : -- : --");
			else{
			    DS = tmp % 100;
			    tmp = tmp / 100;
			    SS = tmp % 60;
			    tmp = tmp / 60;
			    MM = tmp % 60;
			
				if(MM < 10) timeout = "0" + MM.ToString() + ":";
				else timeout = MM.ToString();
				if(SS < 10) timeout += "0";
				timeout += (SS.ToString() + ":");
				if(DS < 10) timeout += "0";
				timeout += DS.ToString();		
				
				if(text == null) break;
				time[i].SendMessage ("SetText", timeout);
			}
		}
		reader.Close ();
	}
	
	void MakeFile()
	{
		if(num == 0)
		{
			path = Application.dataPath + "/score1.ini";
		}
		else if(num == 1)
		{
			path = Application.dataPath + "/score2.ini";
		}
		else if(num == 2)
		{
			path = Application.dataPath + "/score3.ini";
		}
		
		var mf = File.CreateText (path);
		for(int i=0; i<10; i++)
		{
			mf.WriteLine ("Empty");
			mf.WriteLine ("-1");
		}
		mf.Close ();
	}
	
	void showrank()
	{
		name[10].SendMessage ("SetText","Map "+(num+1));
		ReadFile ();
	}
	
	void Update(){
		test = Contro.GetComponent<Controller>().direction1;
		
		Debug.Log (test);
	
		if(test.Length <= 0) test = "PP";
		fcnt++;
		if(fcnt > 25)
		{
			fcnt = 0;
			flag1 = 0;

			flag3 = 0;		
		}
		
		if(test == "WW" || (test[0] != 'W' && test[1] != 'W'))
		{
			flag1 = 1;
			flag3 = 0;
		}
		else if((test == "BW" || test =="WL" || test == "FL") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag3 = 0;
			if(sel > 0)
			{
				menus[sel].SendMessage ("releaseMenu");
				sel -= 1;
				menus[sel].SendMessage ("SelectMenu");
			}
		
		}
		else if((test =="FW" || test == "WR" || test == "FR") && flag1 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag3 = 0;
			if(sel < 2)
			{
				menus[sel].SendMessage ("releaseMenu");
				sel += 1;
				menus[sel].SendMessage ("SelectMenu");
			}
		}
		
		if((test[0] == 'I' || test[1] == 'I') && flag3 != 1)
		{
			fcnt = 0;
			flag1 = 1;
			flag1 = 1;
			flag3 = 1;
			if(sel == 0 && num > 0)
			{
				num -= 1;
				showrank ();
			}
			else if(sel == 1)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				Application.LoadLevel (0);
			}
			else if(sel == 2 && num < 2)
			{
				num++;
				showrank();
			}
		}		
		
		
		if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if(sel > 0)
			{
				menus[sel].SendMessage ("releaseMenu");
				sel -= 1;
				menus[sel].SendMessage ("SelectMenu");
			}
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
		{
			if(sel < 2)
			{
				menus[sel].SendMessage ("releaseMenu");
				sel += 1;
				menus[sel].SendMessage ("SelectMenu");
			}
		}
		
		if(Input.GetKeyDown (KeyCode.Return))
		{
			if(sel == 0 && num > 0)
			{
				num -= 1;
				GameObject.Find ("Time").SendMessage ("SetText", "Time");
				showrank ();
			}
			else if(sel == 1)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				Application.LoadLevel (0);
			}
			else if(sel == 2 && num < 2)
			{
				num++;
				if(num == 2) GameObject.Find ("Time").SendMessage ("SetText", "Score");
				else GameObject.Find ("Time").SendMessage ("SetText", "Time");
				showrank();
			}
		}
	}
	
/*	void OnMouseEnter()
	{
		renderer.material.color = Color.blue;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
	}
	
	void OnMouseDown()
	{
		if(isQuit)
		{
			Application.Quit();
		}
		else
		{
			Application.LoadLevel(0);
		}
	}*/
}
