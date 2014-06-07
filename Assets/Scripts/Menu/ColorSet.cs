using UnityEngine;
using System.Collections;

public class ColorSet : MonoBehaviour {
	
	public TextMesh textout;
	// Use this for initialization
	void Start () {
//		renderer.material.color = Color.blue;
	}
	
	public void SelectMenu()
	{
		renderer.material.color = Color.blue;
	}
	public void SelectEffect()
	{
		renderer.material.color = Color.red;
	}
	public void SetText(string pri)
	{
		textout.text = pri;
	}
	public void SelectChar()
	{
		renderer.material.color = Color.magenta;
	}
	
	public void releaseMenu()
	{
		renderer.material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
