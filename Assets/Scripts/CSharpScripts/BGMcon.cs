using UnityEngine;
using System.Collections;

using System;
using System.IO;

public class BGMcon : MonoBehaviour {
	float currentMusicTime;
	
	private string path;
	private StreamReader reader = null;
	private FileInfo theSourceFile = null;
	private int start = 0;
	
	// Use this for initialization

	void Start () {
		GetTime();
		audio.time = currentMusicTime;
		audio.Play();
		start = 1;
	}	
	
/*	void OnLevelWasLoaded () {
		GetTime();
		audio.time = currentMusicTime;
		audio.Play();
		start = 1;
	}*/

	void OnApplicationQuit()
	{
		SaveAudio (0);
	}
	
	void GetTime()
	{
		string text;
		path = Application.dataPath + "/AudioTime.ini";
		
		if(File.Exists (path) == false) SaveAudio (2);
		
		theSourceFile = new FileInfo(path);
		reader = theSourceFile.OpenText ();
		
		text = reader.ReadLine ();
		currentMusicTime = (float)System.Convert.ToDouble(text);
		
		reader.Close ();
	}
	
	void SaveAudio(int st)
	{
		path = Application.dataPath + "/AudioTime.ini";
		var mf = File.CreateText (path);
		if(st == 1) mf.WriteLine (audio.time);
		else mf.WriteLine (0);
		mf.Close ();	
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log ("nadasjdfkljsakldfjklsdjfkla      " + currentMusicTime + "           " + audio.time);
//		if(start == 1) currentMusicTime = audio.time;
	}
}
