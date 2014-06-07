#pragma strict

var playedTime : double;
var tmp : double;
var timeDisplay : GUIText;
var flag : int;
var MM : int;
var SS : int;
var DS : int;
var timeout : String;
var winner : int;
var min : int;
var curTime : int;
 
function Start(){
	timeDisplay.text = "00:00:00";
	playedTime = 0;
	DS = 0;
	SS = 0;
	MM = 0;
	flag = 0;
	
}

function Getmin(valt : int)
{
	min = valt;
	
	if(min > 0){
	    tmp = min;
	    DS = tmp % 100;
	    tmp = tmp / 100;
	    SS = tmp % 60;
	    tmp = tmp / 60;
	    MM = tmp % 60;
		
		if(MM < 10) timeout = "0" + MM.ToString() + ":";
		else timeout = MM.ToString();
		if(SS < 10) timeout += "0";
		timeout += (SS.ToString() + ":");
		if(DS < 10) timeout += "0";
		timeout += DS.ToString();
	
		timeDisplay.text = timeout;
	}
}

function go()
{
	flag = 1;
	curTime = (Time.time * 100);
}

function Stop(num : int)
{
	flag = 0;
	winner = num;
	GameObject.Find("SaveFinish").SendMessage("Getwinner", winner);
	GameObject.Find("SaveFinish").SendMessage("Gettime", playedTime);
}
  
function Update(){

	//Debug.Log("min :  " + Time.time);
	
	if(flag == 1)
	{
	    playedTime = (Time.time * 100 - curTime);
	    if(min == -1) tmp = playedTime;
	    else tmp = min - playedTime;
	    
	    if(tmp <= 0 && min >= 0)
	    {
	    	tmp = 0;
	    	flag = 0;
	    	GameObject.Find("WaypointFinal").SendMessage("GameSet", -1);
	    }
	    
	    DS = tmp % 100;
	    tmp = tmp / 100;
	    SS = tmp % 60;
	    tmp = tmp / 60;
	    MM = tmp % 60;
		
		if(MM < 10) timeout = "0" + MM.ToString() + ":";
		else timeout = MM.ToString();
		if(SS < 10) timeout += "0";
		timeout += (SS.ToString() + ":");
		if(DS < 10) timeout += "0";
		timeout += DS.ToString();
   	 	
   		timeDisplay.text = timeout;
   	}
}