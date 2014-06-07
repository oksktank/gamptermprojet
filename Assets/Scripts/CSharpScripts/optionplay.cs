using UnityEngine;
using System.Collections;

using System;
using System.IO;


public class optionplay : MonoBehaviour {
	
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
		
		if(GameObject.Find ("pig_cart_1p") != null) bgMusic = GameObject.Find("pig_cart_1p");
		bgMusic.GetComponent<AudioSource>();		
		
		if(sound == 0){
			bgMusic.audio.volume = 0.03f;
		}
		else if(sound == 1){
			bgMusic.audio.volume = 0.1f;
		}
		else if(sound == 2){
			bgMusic.audio.volume = 0.5f;
		}
		
		if(GameObject.Find ("pig_cart_2p") != null) bgMusic = GameObject.Find("pig_cart_2p");
		bgMusic.GetComponent<AudioSource>();		
		
		if(sound == 0){
			bgMusic.audio.volume = 0.03f;
		}
		else if(sound == 1){
			bgMusic.audio.volume = 0.1f;
		}
		else if(sound == 2){
			bgMusic.audio.volume = 0.5f;
		}
		
		if(GameObject.Find ("ItemSound") != null) bgMusic = GameObject.Find("ItemSound");
		bgMusic.GetComponent<AudioSource>();		
		
		if(sound == 0){
			bgMusic.audio.volume = 0.03f;
		}
		else if(sound == 1){
			bgMusic.audio.volume = 0.1f;
		}
		else if(sound == 2){
			bgMusic.audio.volume = 0.5f;
		}
		
		if(GameObject.Find ("Readygo") != null)
		{
			bgMusic = GameObject.Find("Readygo");
			bgMusic.GetComponent<AudioSource>();		
			
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
		if(GameObject.Find ("Readygo2") != null)
		{
			bgMusic = GameObject.Find("Readygo2");
			bgMusic.GetComponent<AudioSource>();		
		
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
	}
	
	// Update is called once per frame
	void Update () {		
	}
}
