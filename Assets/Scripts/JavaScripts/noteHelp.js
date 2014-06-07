//#pragma strict
var note1 : Texture2D;
var note2 : Texture2D;
var note3 : Texture2D;
var note4 : Texture2D;
var note5 : Texture2D;
var note6 : Texture2D;
var note7 : Texture2D;
var note8 : Texture2D;
var note9 : Texture2D;

var idx : int;
var sel : int;
var menus = new GameObject[3];

var Contro : GameObject;
var test : String;
var flag1 : int;
var flag3 : int;
var fcnt : int;

function Start () {

	flag1 = 0;
	flag1 = 0;
	flag3 = 0;
	fcnt = 0;
	Contro = GameObject.Find("Controller");

	idx = 1;
	sel = 1;
	menus[0] = GameObject.Find ("Prev");
	menus[1] = GameObject.Find ("Back");
	menus[2] = GameObject.Find ("Next");
	
	menus[sel].SendMessage ("SelectMenu");
}

function Update () {
	test = Contro.GetComponent("Controller").direction1;
	
	if(test.Length <= 0) test = "PP";
	fcnt++;
	if(fcnt > 25)
	{
		fcnt = 0;
		flag1 = 0;
		flag1 = 0;
		flag3 = 0;
	
	}
	
	if(test == "WW" || (test[0] != 'W' && test[1] != 'W'))
	{
		flag1 = 1;
		flag3 = 0;
	}
	else if((test == "BW" || test =="WL") && flag1 != 1)
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
	else if((test =="FW" || test == "WR") && flag1 != 1)
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
		if(sel == 0)
		{
			if(idx == 1){
				idx = 9;
			}
			else {
				idx --;
			}
		}
		
		else if(sel == 1)
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			Application.LoadLevel(0);
		}
		
		else if(sel == 2)
		{
			if(idx == 9){
			idx = 1;
			}
			else {
				idx ++;
			}
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
		if(sel == 0)
		{
			if(idx == 1){
				idx = 9;
			}
			else {
				idx --;
			}
		}
		
		else if(sel == 1)
		{
			GameObject.Find ("MenuBgm").SendMessage ("SaveAudio",1);
			Application.LoadLevel(0);
		}
		
		else if(sel == 2)
		{
			if(idx == 9){
			idx = 1;
			}
			else {
				idx ++;
			}
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
	else if(idx == 7){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note7);
	}
	else if(idx == 8){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note8);
	}
	else if(idx == 9){
		GUI.DrawTexture(Rect(Screen.width/2 - 360,Screen.height/2 - 220 ,720,440), note9);
	}
}




