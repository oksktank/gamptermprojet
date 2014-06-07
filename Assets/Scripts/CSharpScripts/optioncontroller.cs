using UnityEngine;
using System.Collections;

using System;
using System.IO;


public class optioncontroller : MonoBehaviour {
	
	private int sound = 2, lightlevel = 0, effect = 0;
	private string path;
	
	protected string text ="";
	protected StreamReader reader = null;
	protected FileInfo theSourceFile = null;
	
	private GameObject dLight;
	private GameObject bgMusic;
	
	void MakeFile()
	{
		path = Application.dataPath + "/opvalue.ini";
		
		var mf = File.CreateText (path);
		mf.WriteLine (sound );
		mf.WriteLine (lightlevel );
		mf.WriteLine (effect);
		mf.Close ();
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
		lightlevel = System.Convert.ToInt32 (text);
		text = reader.ReadLine ();
		effect = System.Convert.ToInt32 (text);
		
		reader.Close ();
	}
	
	
	// Use this for initialization
	void Start () {
		sound = 2;
		lightlevel = 0;
		effect = 1;
		ReadFile();
		
		dLight = GameObject.Find("Directional_light");
		dLight.GetComponent<Light>();
		
		if(GameObject.Find ("bgm") != null) bgMusic = GameObject.Find("bgm");
		if(GameObject.Find ("MenuBgm") != null) bgMusic = GameObject.Find ("MenuBgm");
		bgMusic.GetComponent<AudioSource>();
		
		if(lightlevel == 1){
			dLight.light.intensity = 0.5f;

		}
		else if(lightlevel == 0){
			dLight.light.intensity = 0.7f;
		}
		
		
		if(sound == 0){
			bgMusic.audio.volume = 0.03f;
		}
		else if(sound == 1){
			bgMusic.audio.volume = 0.1f;
		}
		else if(sound == 2){
			bgMusic.audio.volume = 0.5f;
		}
	
	}
	
	void doit()
	{
		ReadFile();
		
		if(lightlevel == 1){
			dLight.light.intensity = 0.4f;

		}
		else if(lightlevel == 0){
			dLight.light.intensity = 0.7f;
		}
		
		
		if(sound == 0){
			bgMusic.audio.volume = 0.03f;
		}
		else if(sound == 1){
			bgMusic.audio.volume = 0.1f;
		}
		else if(sound == 2){
			bgMusic.audio.volume = 0.5f;
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		

		
		
		
		
		
			
	}
}
