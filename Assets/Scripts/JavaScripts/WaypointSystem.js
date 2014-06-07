//for reading from a setting file
#pragma strict
import System.IO;


//general
var waypoint : Collider;
var statisticsSkin : GUISkin;
var resultsSkin : GUISkin;
var statisticsStyle : GUIStyle;
var resultStyle : GUIStyle;

var Win : Texture2D;
var Lose : Texture2D;
var Wow : Texture2D;
var Success : Texture2D;
var Timeover : Texture2D;
var Draw : Texture2D;
var Empty : Texture2D;

//laps
static var Car1Lap = 1;
static var Car2Lap = 1;

//static var lapsToReachInt = parseInt(File.ReadAllText(Path.GetDirectoryName("/Laps.txt") + "/Laps.txt"));
static var lapsToReachInt = 1;

//static var gameMode = File.ReadAllText(Path.GetDirectoryName("/Mode.txt") + "/Mode.txt");
static var gameMode = "multiplayer";

static var lapsToMake : int = lapsToReachInt;

static var LapMade1 = "Laps: 1/" + lapsToMake;
static var LapMade2 = "Laps: 1/" + lapsToMake;

//waypoints, player1
//static var Car1Waypoints = new Array(false, false, false, false);
static var Car1Waypoint1 = false;
static var Car1Waypoint2 = false; 
static var Car1Waypoint3 = false; 
static var Car1Waypoint4 = false; 
static var Car1FinalWaypoint = false;
static var WaypointName1 = "Waypoint1 unreached";
static var Car1Result : Texture2D;

//waypoints, player2
//static var Car2Waypoints = new Array(false, false, false, false);
static var Car2Waypoint1 = false; static var Car2Waypoint2 = false; static var Car2Waypoint3 = false; static var Car2Waypoint4 = false;
static var Car2FinalWaypoint = false;
static var WaypointName2 = "Waypoint1 unreached";
static var Car2Result : Texture2D;

var Mode : int;
var rr : int;
var Mapnum : int;
var score1 : int;
var score2 : int;
var scoreout : int;

/*function Update()
{
	if(Input.GetKeyDown(KeyCode.Backspace))
	{
		Debug.Log("AA");
		GameObject.Find("Timer").SendMessage("Stop", 2);
	}

}*/



function Start(){
	rr = 1;
	scoreout = 0;
//	Mapnum = 0;
	Car1Waypoint1 = false;
	Car1Waypoint2 = false;
	Car1Waypoint3 = false;
	Car1Waypoint4 = false;
	Car1FinalWaypoint = false;
	Car2Waypoint1 = false;
	Car2Waypoint2 = false;
	Car2Waypoint3 = false;
	Car2Waypoint4 = false;
	Car2FinalWaypoint = false;
	Car1Lap = 1;
	Car2Lap = 1;
	Car1Result = Empty;
	Car2Result = Empty;
	Time.timeScale = 1;
}

function Update()
{
//	Debug.Log("asdf : " + Mapnum + "  " + score2);
//	Debug.Log("Mode   : " + Mapnum);
//	Debug.Log("1  " +	Car1Waypoint1 + "   " + Car1Waypoint2 + "   " + Car1Waypoint3 + "   " + Car1Waypoint4 + "   " + Car1FinalWaypoint );
//	Debug.Log("2  " +	Car2Waypoint1 + "   " + Car2Waypoint2 + "   " + Car2Waypoint3 + "   " + Car2Waypoint4 + "   " + Car2FinalWaypoint );

}

function OnTriggerExit (other : Collider)
{
	if(other.attachedRigidbody.name == "pig_cart_2p")
	{
		rr = 10;
		if(Car1Lap <= lapsToMake && Car1FinalWaypoint == false)
		{
			if(Car1Waypoint1 == false && waypoint.gameObject.name == "Waypoint1")
			{
			Car1Waypoint1 = true;
			WaypointName1 = waypoint.gameObject.name + " reached";
			}
			else if(Car1Waypoint1 == true && waypoint.gameObject.name == "Waypoint2" )
			{
			Car1Waypoint2 = true;
			WaypointName1 = waypoint.gameObject.name + " reached";
			}
			else if(Car1Waypoint2 == true && waypoint.gameObject.name == "Waypoint3")
			{
			Car1Waypoint3 = true;
			WaypointName1 = waypoint.gameObject.name + " reached";
			}
			else if(Car1Waypoint3 == true && waypoint.gameObject.name == "Waypoint4")
			{
			Car1Waypoint4 = true;
			WaypointName1 = waypoint.gameObject.name + " reached";
			}
			else if(Car1Waypoint4 == true && waypoint.gameObject.name == "WaypointFinal")
			{
			Car1FinalWaypoint = true;
			WaypointName1 = waypoint.gameObject.name + " reached";
			}
		}
		else if(Car1Lap != lapsToMake && Car1FinalWaypoint == true)
		{
			Car1Lap++;
			
			Car1Waypoint1 = false;
			Car1Waypoint2 = false;
			Car1Waypoint3 = false;
			Car1Waypoint4 = false;
			Car1FinalWaypoint = false;
			LapMade1 = "Laps: " + Car1Lap + "/" + lapsToMake;
			WaypointName1 = "Waypoint1 unreached";
		}
		else if(Car1Lap == lapsToMake && Car1FinalWaypoint == true && Time.timeScale == 1)
		{
			scoreout = 3;
			if(Mode == 2) GameObject.Find("Timer").SendMessage("Stop", 2);
			else GameObject.Find("Timer2").SendMessage("Stop", 2);
			Time.timeScale = 0.7;
			Car1Result = Win;
			Car2Result = Lose;
			yield WaitForSeconds(1);
			Time.timeScale = 0.5;
			yield WaitForSeconds(1);
			Time.timeScale = 0.2;
			GameObject.Find ("SaveFinish").SendMessage("SFinish");
		}
	}
	
	else if(other.attachedRigidbody.name == "pig_cart_1p")
	{
		if(Car2Lap <= lapsToMake && Car2FinalWaypoint == false)
		{
			if(Car2Waypoint1 == false && waypoint.gameObject.name == "Waypoint1")
			{
			Car2Waypoint1 = true;
			WaypointName2 = waypoint.gameObject.name + " reached";
			}
			else if(Car2Waypoint1 == true && waypoint.gameObject.name == "Waypoint2" )
			{
			Car2Waypoint2 = true;
			WaypointName2 = waypoint.gameObject.name + " reached";
			}
			else if(Car2Waypoint2 == true && waypoint.gameObject.name == "Waypoint3")
			{
			Car2Waypoint3 = true;
			WaypointName2 = waypoint.gameObject.name + " reached";
			}
			else if(Car2Waypoint3 == true && waypoint.gameObject.name == "Waypoint4")
			{
			Car2Waypoint4 = true;
			WaypointName2 = waypoint.gameObject.name + " reached";
			}
			else if(Car2Waypoint4 == true && waypoint.gameObject.name == "WaypointFinal")
			{
			Car2FinalWaypoint = true;
			WaypointName2 = waypoint.gameObject.name + " reached";
			}
		}
		else if(Car2Lap != lapsToMake && Car2FinalWaypoint == true)
		{
			Car2Lap++;
			Car2Waypoint1 = false;
			Car2Waypoint2 = false;
			Car2Waypoint3 = false;
			Car2Waypoint4 = false;
			Car2FinalWaypoint = false;
			LapMade2 = "Laps: " + Car2Lap + "/" + lapsToMake;
			WaypointName2 = "Waypoint1 unreached";
		}
		else if(Car2Lap == lapsToMake && Car2FinalWaypoint == true && Time.timeScale == 1)
		{
			if(Mode == 2) GameObject.Find("Timer").SendMessage("Stop", 1);
			else GameObject.Find("Timer2").SendMessage("Stop", 1);
			Time.timeScale = 0.7;
			if(Mode == 2){
				scoreout = 3;
				Car1Result = Lose;
				Car2Result = Win;
			}
			else
			{
				scoreout = 3;
				Car1Result = Wow;
				Car2Result = Success;
			}
			yield WaitForSeconds(1);
			Time.timeScale = 0.5;
			yield WaitForSeconds(1);
			Time.timeScale = 0.2;
			GameObject.Find ("SaveFinish").SendMessage("SFinish");
		
		}
	}
	
	//RecoveryItem();
}

function FinishTuto(resul : int)
{
	scoreout = 3;
	if(Mode == 1)
	{
	Car1Result = Wow;
		Car2Result = Success;
		yield WaitForSeconds(1);
		Time.timeScale = 0.5;
		yield WaitForSeconds(1);
		Time.timeScale = 0.2;
		Application.LoadLevel(0);
	}
	
	if(resul == 1)
	{
		Car2Result = Success;
	}
	else if(resul == 2)
	{
		Car1Result = Success;
	}
	else if(resul == 3)
	{
		Car1Result = Success;
		Car2Result = Success;
		yield WaitForSeconds(1);
		Time.timeScale = 0.5;
		yield WaitForSeconds(1);
		Time.timeScale = 0.2;
		Application.LoadLevel(0);
	}
	
}

function Setscore(scorenum : int)
{
	if(scorenum == 1) score1++;
	else score2++;
}

function GameSet(res : int)
{
	if(Mapnum == 3 && Mode == 2)
	{
		if(score1 > score2)
		{
			Car2Result = Win;
			Car1Result = Lose;
			scoreout = 1;
			GameObject.Find("SaveFinish").SendMessage("Getwinner", 1);
			GameObject.Find("SaveFinish").SendMessage("Gettime", score1);
		}
		else if(score1 < score2)
		{
			Car1Result = Win;
			Car2Result = Lose;
			scoreout = 2;
			GameObject.Find("SaveFinish").SendMessage("Getwinner", 2);
			GameObject.Find("SaveFinish").SendMessage("Gettime", score2);
		}
		else
		{
			Car1Result = Draw;
			Car2Result = Draw;
			scoreout = -1;
		}
	}
	else if(Mapnum == 3 && Mode == 1)
	{
		Car1Result = Timeover;
		scoreout = -1;
		GameObject.Find("SaveFinish").SendMessage("Getwinner", 1);
		GameObject.Find("SaveFinish").SendMessage("Gettime", score1);
	}
	Time.timeScale = 0.7;
	yield WaitForSeconds(1);
	if(Mapnum != 3)
	{
	scoreout = -1;
		Car2Result = Timeover;
		if(Mode == 2) Car1Result = Timeover;
	}
	yield WaitForSeconds(1);
	Time.timeScale = 0.5;
	yield WaitForSeconds(1);
	Time.timeScale = 0.2;
	
	if(Mapnum == 4 || Car1Result == Timeover || Car2Result == Timeover)
	{
		Application.LoadLevel(0);
		return;
	}

	if(scoreout >= 1) GameObject.Find ("SaveFinish").SendMessage("SFinish");
	else Application.LoadLevel(0);
}

function SetMode(TT : int)
{
	Mode = TT % 10;
	Mapnum = TT / 10;
	if(Mapnum >= 3) 
	{
		LapMade1 = "";
		LapMade2 = "";
	}
}

/*function RecoveryItem()
{
	var FB = "";
	var i = 0;
	if((Car1Waypoint1 && Car2Waypoint1) && (Car1Waypoint2 || Car2Waypoint2))
	{
		FB = "W1B";
		for(i=1;i<4;i++)
		{
			FB += i.ToString();
			GameObject.Find(FB).SendMessage("reLoad");
		}
	}
	if((Car1Waypoint2 && Car2Waypoint2) && (Car1Waypoint3 || Car2Waypoint3))
	{
		FB = "W2B";
		for(i=1;i<4;i++)
		{
			FB += i.ToString();
			GameObject.Find(FB).SendMessage("reLoad");
		}
	}
	if((Car1Waypoint3 && Car2Waypoint3) && (Car1Waypoint4 || Car2Waypoint4))
	{
		FB = "W3B";
		for(i=1;i<4;i++)
		{
			FB += i.ToString();
			GameObject.Find(FB).SendMessage("reLoad");
		}
	}
	if((Car1Waypoint4 && Car2Waypoint4) && (Car1FinalWaypoint || Car2FinalWaypoint))
	{
		FB = "W4B";
		for(i=1;i<4;i++)
		{
			FB += i.ToString();
			GameObject.Find(FB).SendMessage("reLoad");
		}
	}
}*/

function GUIstatistics() {

	GUI.skin = statisticsSkin;
	GUI.skin.label.fontSize = 25;
	//Player1
	if(Mapnum < 3){
		if(Mode == 2){
		GUI.BeginGroup(Rect(Screen.width - 380, Screen.height - 80, 290, 53)); //70
		GUI.Box(Rect(0, 0, 310, 53), "");
		//GUI.Label(Rect(25, 15, 170, 50), WaypointName1);
		GUI.Label(Rect(185, 15, 150, 50), LapMade1);
		GUI.EndGroup();
		}
	}
 
	
	//Player2
	if(Mapnum < 3){
		if(Mode == 2) GUI.BeginGroup(Rect(Screen.width - 380, Screen.height/2 - 80, 290, 53));   //70
		else GUI.BeginGroup(Rect(Screen.width - 380, Screen.height - 80, 290, 53));
		GUI.Box(Rect(0, 0, 310, 53), "");
		//GUI.Label(Rect(25, 15, 170, 50), WaypointName2);
		GUI.Label(Rect(185, 15, 150, 50), LapMade2);
		GUI.EndGroup(); 
	}
}

function RaceResults() {
	//GUI.skin = resultsSkin;
	GUI.contentColor = Color.magenta;
	GUI.skin.label.fontSize = 90;
	
	if(Mode == 2) GUI.DrawTexture(Rect(Screen.width / 2 - 250, Screen.height/4 * 3 - 125 , 500, 250), Car1Result);
	else GUI.DrawTexture(Rect(Screen.width / 2 - 250, Screen.height / 2 - 125, 500, 250), Car1Result);
	GUI.DrawTexture(Rect(Screen.width / 2 - 250, Screen.height / 4 - 125, 500, 250), Car2Result);
}

function OnGUI() {

	GUIstatistics();
	if(scoreout != 0)	RaceResults();
}