using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {
	
	public TextMesh textout;
//	public GUIText gui;
//	private int score;
//	private TextMesh txt;
//	private GameObject gd;
//	private MeshRenderer mr;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	//	gui.text = "Score: " + score;
	}
		
	public void SetText(string pri)
	{
		textout.text = pri;
	}
	
}


