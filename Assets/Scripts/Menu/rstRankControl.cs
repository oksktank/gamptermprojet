using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class rstRankControl : MonoBehaviour {
	private int sel = 0;
	private int mapnum;
	private int rank;
	private int time, tmp;
	private string name, savename, timeout;
	private GameObject[] rst = new GameObject[40];
	private GameObject[] prin = new GameObject[3];
	private GameObject Nametext;
	private string[] chr = new string[40] {"A","B","C","D","E","F","G","H","I","J",
										"K","L","M","N","O","P","Q","R","S","T",
										"U","V","W","X","Y","Z","-",".","0","1",
										"2","3","4","5","6","7","8","9","Erase","End"};
	private int i, len;	
	private string path;
	protected string text ="";
	protected StreamReader reader = null;
	protected FileInfo theSourceFile = null;
	private bool check = false;
	protected int winner;
	
	protected int MM, SS, DS;
	
	private string[] Lname = new string[10] {"Empty","Empty","Empty","Empty","Empty","Empty","Empty","Empty","Empty","Empty"};
	private int[] Ltime = new int[10] {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};

	GameObject Contro;
	string test1;
	int flag1=0, flag5=0, fcnt=0;
	string test2;
	
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		flag1=0;
		flag1=0;
		flag1=0;
		flag1=0;
		flag5=0;
		fcnt=0;
		Contro = GameObject.Find ("Controller");
		sel = 0;
		check = true;
		Nametext = GameObject.Find ("Name");
		GetInfo();
		if(check)
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			Application.LoadLevel (3);
		}
		
		for(i=0;i<40;i++)
		{
			rst[i] = GameObject.Find (chr[i]);
		}
		prin[0] = GameObject.Find ("Rankval");
		prin[1] = GameObject.Find ("Timeval");
		prin[2] = GameObject.Find ("Nameval");
		
		rst[sel].SendMessage ("SelectChar");
		prin[0].SendMessage ("SetText",(rank+1).ToString());
		prin[1].SendMessage ("SetText",timeout);
		name = "";
		len = 0;
		prin[2].SendMessage ("SetText",name);
	}
	
	void GetInfo()
	{
		path = Application.dataPath + "/Gresult.ini";
		if(File.Exists (path) == false)
		{
			MakeInfo();
		}
		
		theSourceFile = new FileInfo(path);
		reader = theSourceFile.OpenText ();
		
		text = reader.ReadLine ();
		winner = System.Convert.ToInt32 (text);
		
		if(winner == -1)
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			reader.Close ();
			Application.LoadLevel (3);
		}
		
		text = reader.ReadLine ();
		mapnum = System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		time = System.Convert.ToInt32 (text);
		Nametext.SendMessage ("SetText", "Name( " + winner + "P) :");
		
		if(mapnum != 3)
		{
			tmp = time;
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
		}
		else{
			timeout = (time).ToString ();
		}
		
		reader.Close ();
		ReadFile();
	}
	
	void MakeInfo()
	{
		path = Application.dataPath + "/Gresult.ini";
		var mf = File.CreateText (path);
		mf.WriteLine ("1");
		mf.WriteLine ("1");
		mf.WriteLine ("1000");
		mf.Close ();
	}
	
	void ReadFile()
	{
		if(mapnum == 1)
		{
			path = Application.dataPath + "/score1.ini";
		}
		else if(mapnum == 2)
		{
			path = Application.dataPath + "/score2.ini";
		}
		else if(mapnum == 3)
		{
			path = Application.dataPath + "/score3.ini";
			GameObject.Find ("Time").SendMessage ("SetText", "Score :");
		}
		
		if(File.Exists (path) == false)
		{
			MakeFile();
		}
		
		theSourceFile = new FileInfo(path);
		reader = theSourceFile.OpenText ();
		
		if(mapnum == 3) time = 30000 - time;
		rank = 11;
		for(i =0; i<10;i++)
		{
			text=reader.ReadLine ();
			Lname[i] = text;
			text = reader.ReadLine ();
			Ltime[i] = System.Convert.ToInt32 (text);
			if(check)
			{
				if(Ltime[i] > time || Ltime[i] == -1)
				{
					rank = i;
					check = false;
				}
			}
		}
		for(i = 9; i>rank; i--)
		{
			Lname[i] = Lname[i-1];
			Ltime[i] = Ltime[i-1];
		}
		if(rank <= 9) Ltime[rank] = time;
		reader.Close ();
	}
	
	void MakeFile()
	{
		if(mapnum == 1)
		{
			path = Application.dataPath + "/score1.ini";
		}
		else if(mapnum == 2)
		{
			path = Application.dataPath + "/score2.ini";
		}
		else if(mapnum == 3)
		{
			path = Application.dataPath + "/score3.ini";
		}
		
		var mf = File.CreateText (path);
		for(i=0;i<10;i++)
		{
			mf.WriteLine (Lname[i]);
			mf.WriteLine (Ltime[i]);
		}
		mf.Close ();
	}
	
	// Update is called once per frame
	void Update () {
		test1 = Contro.GetComponent<Controller>().direction1;
		test2 = Contro.GetComponent<Controller>().direction2;
		
		if(test1.Length <= 0) test1 = "PP";
		if(test2.Length <=0) test2 = "PP";
		
		Debug.Log ("log : " + test1);
		
		fcnt++;
		if(fcnt > 25)
		{
			fcnt = 0;
			flag1 = 0;
			flag1 = 0;
			flag1 = 0;
			flag1 = 0;
			
		}
		
		if(((test1 == "WW" || (test1[0] != 'W' && test1[1] != 'W')) && winner == 1) || (test2 == "WW" && winner == 2))
		{
//			fcnt = 0;
			flag1 = 1;
			flag1 = 1;
			flag1 = 1;
			flag1 = 1;
			flag5 = 0;
		}
		else if((test1 == "BW" && flag1 != 1 && winner == 1) || (test2 == "BW" && flag1 !=1 && winner == 2))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			rst[sel].SendMessage ("releaseMenu");
			if(sel <= 16)
			{
				sel+=11;
			}
			else if(sel <= 21)
			{
				if(sel == 21) sel--;
				sel+=17;
			}
			else if(sel <= 27)
			{
				sel+=6;
			}
			else if(sel < 33)
			{
				sel = 38;
			}
			else if(sel < 38)
			{
				sel = 39;
			}
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		else if((test1 == "FW" && flag1 != 1 && winner == 1) || (test2 == "FW" && flag1 !=1 && winner == 2))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			rst[sel].SendMessage ("releaseMenu");
			if(sel >= 11 && sel <= 27)
			{
				sel-=11;
			}
			else if(sel >= 28 && sel <= 33)
			{
				sel-= 6;
			}
			else if(sel >= 34 && sel <= 37)
			{
				sel-=17;
			}
			else if(sel == 38 || sel == 39)
			{
				sel = 28;
			}
			
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		else if(((test1 == "WL" || test1 == "FL") && flag1 != 1 && winner == 1) || ((test2 == "WL" || test2 == "FL") && flag1 !=1 && winner == 2))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			rst[sel].SendMessage ("releaseMenu");
			if(sel > 0)
			{
				sel--;
			}
			
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		else if(((test1 == "WR" || test1 == "FR") && flag1 != 1 && winner == 1) || ((test2 == "WR" || test2 == "FR") && flag1 !=1 && winner == 2))
		{
			fcnt = 0;
			flag1 = 1;
			flag5 = 0;
			rst[sel].SendMessage ("releaseMenu");
			if(sel < 39)
				sel++;
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		
		if(((test1[0] == 'I' || test1[1] == 'I') && winner == 1 && flag5 != 1) || ((test2[0] == 'I' || test2[1] == 'I') && winner==2&& flag5 != 1))
		{
			fcnt = 0;
			flag1 = 1;
			flag1 = 1;
			flag1 = 1;
			flag1 = 1;
			flag5 = 1;
			
			if(sel < 38 && len < 5)
			{
				len++;
				name = name + chr[sel];
				prin[2].SendMessage ("SetText",name);
				
				if(len == 5)
				{
					rst[sel].SendMessage ("releaseMenu");
					sel = 39;
					rst[sel].SendMessage ("SelectMenu");
				}
				
			}
			else if(sel == 38 && len > 0)
			{
				len--;
				name = name.Substring (0,len);
				prin[2].SendMessage ("SetText",name);
			}
			else if(sel == 39 && len > 0)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				Lname[rank] = name;
				MakeFile();
				Application.LoadLevel(3);
			}
		}
		
		
		Debug.Log (Input.GetKeyDown (KeyCode.DownArrow));
		if((Input.GetKeyDown (KeyCode.DownArrow) && winner == 1) || (Input.GetKeyDown (KeyCode.K) && winner == 2))
		{
			rst[sel].SendMessage ("releaseMenu");
			if(sel <= 16)
			{
				sel+=11;
			}
			else if(sel <= 21)
			{
				if(sel == 21) sel--;
				sel+=17;
			}
			else if(sel <= 27)
			{
				sel+=6;
			}
			else if(sel < 33)
			{
				sel = 38;
			}
			else if(sel < 38)
			{
				sel = 39;
			}
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		else if((Input.GetKeyDown (KeyCode.UpArrow) && winner == 1) || (Input.GetKeyDown (KeyCode.I) && winner == 2))
		{
			rst[sel].SendMessage ("releaseMenu");
			if(sel >= 11 && sel <= 27)
			{
				sel-=11;
			}
			else if(sel >= 28 && sel <= 33)
			{
				sel-= 6;
			}
			else if(sel >= 34 && sel <= 37)
			{
				sel-=17;
			}
			else if(sel == 38 || sel == 39)
			{
				sel = 28;
			}
			
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
			
		}
		else if((Input.GetKeyDown (KeyCode.LeftArrow) && winner == 1) || (Input.GetKeyDown (KeyCode.J) && winner == 2))
		{
			rst[sel].SendMessage ("releaseMenu");
			if(sel > 0)
			{
				sel--;
			}
			
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		else if((Input.GetKeyDown (KeyCode.RightArrow) && winner == 1) || (Input.GetKeyDown (KeyCode.L) && winner == 2))
		{
			rst[sel].SendMessage ("releaseMenu");
			if(sel < 39)
				sel++;
			if(sel == 38 || sel == 39)
				rst[sel].SendMessage ("SelectMenu");
			else
				rst[sel].SendMessage ("SelectChar");
		}
		
		if(Input.GetKeyDown (KeyCode.Return))
		{
			if(sel < 38 && len < 5)
			{
				len++;
				name = name + chr[sel];
				prin[2].SendMessage ("SetText",name);
				
				if(len == 5)
				{
					rst[sel].SendMessage ("releaseMenu");
					sel = 39;
					rst[sel].SendMessage ("SelectMenu");
				}
				
			}
			else if(sel == 38 && len > 0)
			{
				len--;
				name = name.Substring (0,len);
				prin[2].SendMessage ("SetText",name);
			}
			else if(sel == 39 && len > 0)
			{
				GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
				Lname[rank] = name;
				MakeFile();
				Application.LoadLevel(3);
			}
		}
	}
}
