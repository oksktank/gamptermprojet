//#pragma strict

static var startGame = false;

var Map : int;

var Get : Texture2D;
var Set : Texture2D;
var Ready : Texture2D;
var Go : Texture2D;

var getcheck;

function Start ()
{
	getcheck = Time.time;
	

 GameObject.Find("pig_cart_1p").SendMessage("Pause");
 if(GameObject.Find("pig_cart_2p") != null) GameObject.Find("pig_cart_2p").SendMessage("Pause");
 
/*
 guiText.text = "Get";
 yield WaitForSeconds (1.0);
 
// if(Map == 4) guiText.text = "These";
 guiText.text = "Set";
 yield WaitForSeconds (1.0);
 
// if(Map == 4)guiText.text = "15 coins!";
 guiText.text = "Ready";
 yield WaitForSeconds (1.0);
 
 //if(Map == 4) guiText.text = "15 coins!";
 guiText.text = "GO!";
 yield WaitForSeconds (1.0);
*/
 yield WaitForSeconds (4.0);
 
// guiText.text = "";
 startGame = true;
if(GameObject.Find("Timer") != null) GameObject.Find("Timer").SendMessage("go");
if(GameObject.Find("Timer2") != null) GameObject.Find("Timer2").SendMessage("go");
 
 GameObject.Find("pig_cart_1p").SendMessage("Resume");
 if(GameObject.Find("pig_cart_2p") != null) GameObject.Find("pig_cart_2p").SendMessage("Resume");
 GameObject.Find("RealRace").SendMessage("Cstart");
}


function OnGUI(){
	if(Time.time - getcheck > 0 && Time.time - getcheck < 1 ){
		GUI.DrawTexture(Rect(Screen.width/2 - 175,Screen.height/2 - 250,350,350), Get);	
	}
	else if(Time.time - getcheck > 1 && Time.time - getcheck < 2){
		GUI.DrawTexture(Rect(Screen.width/2- 175,Screen.height/2 - 250,350,350), Set);
	}
	else if(Time.time - getcheck > 2 && Time.time - getcheck < 3){
		GUI.DrawTexture(Rect(Screen.width/2-250,Screen.height/2 - 325,500,500), Ready);
	}
	else if(Time.time - getcheck > 3 && Time.time - getcheck < 4){
		GUI.DrawTexture(Rect(Screen.width/2- 175,Screen.height/2 - 250,350,350), Go);
	}
}


function GetMap(MM : int)
{
	Map = MM;
}
