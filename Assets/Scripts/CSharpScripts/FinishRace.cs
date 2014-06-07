using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class FinishRace : MonoBehaviour {
	private int winner;
	private double playedTime;
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;
	
	private string mapNum;
	
	// Use this for initialization
	void Start () {
		ReadInfo ();
	}
	
	void Getwinner(int num)
	{
		winner = num;
	}
	
	void Gettime(int num)
	{
		playedTime = num;
	}
		
	void SFinish()
	{
		
		Debug.Log ("Finish!!!!!!");
		MakeInfo ();
		Application.LoadLevel (16);
	}
	
	void ReadInfo ()
	{
		string text;
		path = Application.dataPath + "/Map.ini";
		
		if(File.Exists (path) == false) mapNum = "2";
		else
		{
			theSourceFile = new FileInfo(path);
			reader = theSourceFile.OpenText ();
			
			mapNum = reader.ReadLine ();
			reader.Close ();
		}
		GameObject.Find ("RealRace").SendMessage ("GetMapInfo",System.Convert.ToInt32 (mapNum));
	}
	
	void MakeInfo()
	{
		path = Application.dataPath + "/Gresult.ini";
		var mf = File.CreateText (path);
		mf.WriteLine (winner.ToString());
		mf.WriteLine (mapNum);
		mf.WriteLine (playedTime.ToString ());
		mf.Close ();
	}
	
	// Update is called once per frame
	void Update () {
	//	Debug.Log (winner);
//		Debug.Log (playedTime);
	}
}
