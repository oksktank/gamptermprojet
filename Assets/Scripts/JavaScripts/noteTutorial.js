//#pragma strict
var note1 : Texture2D;
var note2 : Texture2D;
var note3 : Texture2D;
var note4 : Texture2D;
var note5 : Texture2D;
var note6 : Texture2D;

var idx : int;
var sel : int;

var Contro : GameObject;
var test : String;
var test2 : String;
var flag1 : int;
var flag3 : int;
var fcnt : int;
var cnt : int;

function Start () {
	test ="PP";
	test2 = "PP";
	cnt = 0;
	flag1 = 0;
	flag1 = 0;
	flag3 = 0;
	fcnt = 0;
	Contro = GameObject.Find("Controller");

	idx = 1;
	sel = 0;
	
}

function Update () {
	test = Contro.GetComponent("Controller").direction1;
	test2 = Contro.GetComponent("Controller").direction2;
	
	if(test.Length <=0) test = "PP";
	if(test2.Length <= 0) test2 = "PP";
	
	if(cnt > 30)
	{
		idx++;
		cnt = 0;
	}
	
	if((test == "FW" ||test2 == "FW" || Input.GetKey(KeyCode.UpArrow)) && idx == 1)
	{
		cnt++;
	}
	else if((test == "BW" || test2 == "BW" || Input.GetKey(KeyCode.DownArrow)) && idx == 2)
	{
		cnt++;
	}
	else if((test == "WL" || test2 == "WL" || Input.GetKey(KeyCode.LeftArrow)) && idx == 3)
	{
		cnt++;
	}
	else if((test == "WR" || test2 == "WR" || Input.GetKey(KeyCode.RightArrow)) && idx == 4)
	{
		cnt++;
	}
	
	if((test[0] == 'I' || test[1] == 'I' || test2[0] == 'I' || test2[1] == 'I' || Input.GetKeyDown(KeyCode.Return)) && idx >= 5)
	{
		if(idx == 5)
		{
			idx = 6;
		}
		else if(idx >= 6)
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",0);
			Application.LoadLevel(13);
		}
	}
}


function OnGUI(){

	if(idx == 1){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note1);
	}
	else if(idx == 2){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note2);
	}
	else if(idx == 3){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note3);
	}
	else if(idx == 4){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note4);
	}
	else if(idx == 5){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note5);
	}
	else if(idx == 6){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note6);
	}
}




