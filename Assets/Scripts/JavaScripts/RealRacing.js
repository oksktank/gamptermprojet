#pragma strict

//general
var RealChecker : Collider;

var Car1 : GameObject;
var Car2 : GameObject;
var fen0 : GameObject;
var fen1 : GameObject;
var fen2 : GameObject;
var fen3 : GameObject;
var fen4 : GameObject;
var fen5 : GameObject;
var fen6 : GameObject;
var wayP : GameObject;
var kind1 : int;
var kind2 : int;

var Pause : Texture2D;
var Reverse : Texture2D;
var Replace : Texture2D;

static var tmp1 : Vector3;
static var tmp2 : Vector3;
static var Ttmp11 : Vector3;
static var Ttmp12 : Vector3;
static var Ttmp21 : Vector3;
static var Ttmp22 : Vector3;

static var cur1 = 0;
static var cur2 = 0;
static var chd1 = 0;
static var chd2 = 0;
static var chcnt1 = 0;
static var chcnt2 = 0;
static var fchk1 = 0;
static var fchk2 = 0;

static var out1 = "";
static var out2 = "";

static var Winner = 1;

static var C1Real = new Array(false, false, false, false, false, false,false);
static var C2Real = new Array(false, false, false, false, false, false,false);

static var C1reach = 0.0;
static var C2reach = 0.0;
static var timerMsg1 : int;
static var timerMsg2 : int;
static var TM1 : int;
static var TT1 : int;
static var TM2 : int;
static var TT2 : int;
var checkFix : int;
var checkFix2 : int;
static var savePosition1 : Vector3;
static var savePosition2 : Vector3;
static var Ccnt : int;

var score1 : int;
var score2 : int;
var RC1 = 0;
var RC2 = 0;

private var Mapnum : int;
var Mode = 2;
var pflag : int;

function Start(){

	var i = 0;
	fchk1 = 0;
	fchk2 = 0;
	pflag = 0;
	chcnt1 = 0;
	chcnt2 = 0;
	wayP = GameObject.Find("WaypointFinal");
	score1 = 0;
	score2 = 0;
	RC1 = 0;
	RC2 = 0;
	for(i=0;i<6;i++)
	{
		C1Real[i] = false;
		C2Real[i] = false;
	}
	C1Real[6] = true;
	C2Real[6] = true;
	C1reach = 0.0;
	C2reach = 0.0;
	cur1 = 0;
	cur2 = 0;
	chd1 = 6;
	chd2 = 6;
	out1 = "Rank1";
	out2 = "Rank2";
	Winner = 1;
	timerMsg1 = 0;
	timerMsg2 = 0;
	TM1 = 0;
	TM2 = 0;
	TT1 = 0;
	TT2 = 0;
	checkFix = 0;
	checkFix2 = 0;
	Ccnt = 0;
	kind1 = 0;
	kind2 = 0;
	
	Car1 = GameObject.Find("pig_cart_1p");
	if( GameObject.Find("pig_cart_2p") != null) Car2 = GameObject.Find("pig_cart_2p");
	
	savePosition1 = Car1.transform.position;
	if( GameObject.Find("pig_cart_2p") != null) savePosition2 = Car2.transform.position;

	if(Mapnum < 3){
		fen0 = GameObject.Find("RaceCheck0");	
		fen1 = GameObject.Find("RaceCheck1");
		fen2 = GameObject.Find("RaceCheck2");
		fen3 = GameObject.Find("RaceCheck3");
		fen4 = GameObject.Find("RaceCheck4");
		fen5 = GameObject.Find("RaceCheck5");
		fen6 = GameObject.Find("RaceCheck6");
	}
	
	Ttmp11 = fen0.transform.position - Car1.transform.position;
	if(Mode == 2) Ttmp21 = fen0.transform.position - Car2.transform.position;
//	GameObject.Find("Rank1").SendMessage("rankout", out1);
	
}

function GetMapInfo(str : int)
{
	Mapnum = str;
}

function Setscore(scorenum : int)
{
	if(scorenum == 1) score1++;
	else score2++;
	
	wayP.SendMessage("Setscore", scorenum);
	
	if(Mapnum == 4)
	{
		if(score1 >= 15 && score2 >= 15) GameObject.Find("WaypointFinal").SendMessage("FinishTuto", 3);
		else if(score1 == 15) GameObject.Find("WaypointFinal").SendMessage("FinishTuto", 1);
		else if(score2 == 15) GameObject.Find("WaypointFinal").SendMessage("FinishTuto", 2);
	}
}

function Cstart()
{
	Ccnt = 1;
}

function OnGUI()
{
//	if(Time.timeScale < 1) return;
	if(Mode == 2){
	GUI.skin.label.fontSize = 25;
	GUI.contentColor = Color.red;
	GUI.Label(Rect(Screen.width - 150, Screen.height/8+10, 285, 53), out1);
	GUI.Label(Rect(Screen.width - 150, Screen.height/2+100, 285, 53), out2);
	}

	GUI.skin.label.fontSize = 50;
	GUI.contentColor = Color.magenta;

	if(TM1 > 0)
	{
//		timerMsg1 ++;
		if(Time.timeScale == 0) GUI.DrawTexture(Rect(Screen.width/2 - 125 , Screen.height/4 - 100 , 250, 200), Pause);
		else if(Mapnum < 3 && Time.timeScale == 1) GUI.DrawTexture(Rect(Screen.width/2 - 100, Screen.height/4 - 100 , 200, 200), Reverse);
//		if(timerMsg1 == 1000) timerMsg1 = 0;
		if(Mode == 1) return;
	}	
	if(TM2 > 0 && Mode == 2)
	{
//		timerMsg2 ++;
		if(Time.timeScale == 0) GUI.DrawTexture(Rect(Screen.width/2 - 125, (Screen.height/4) * 3 - 100 , 250, 200), Pause);
		else if(Mapnum < 3 && Time.timeScale == 1) GUI.DrawTexture(Rect(Screen.width/2 - 100, (Screen.height/4) * 3 - 100 , 200, 200), Reverse);
//		if(timerMsg2 == 1000) timerMsg2 = 0;
		return;
	}
	
	if(kind1 > 0 && Time.timeScale == 1)
	{
		GUI.DrawTexture(Rect(Screen.width/2 - 200 , Screen.height/4 - 200 , 400, 400), Replace);
	}
	if(kind2 > 0&& Time.timeScale == 1)
	{
		GUI.DrawTexture(Rect(Screen.width/2 - 200 , (Screen.height/4) * 3 - 200, 400, 400), Replace);
	}
	
}

function Update ()
{
/*Debug.Log("read0   " + C1Real[0]);
Debug.Log("read1   " + C1Real[1]);
Debug.Log("read2   " + C1Real[2]);
Debug.Log("read3   " + C1Real[3]);
Debug.Log("read4   " + C1Real[4]);
Debug.Log("read5   " + C1Real[5]);
Debug.Log("read5   " + C1Real[6]);
Debug.Log("cur1    " + cur1);
Debug.Log("chd1    " + chd1);
Debug.Log("cur2    " + cur2);
Debug.Log("chd2    " + chd2);*/
//Debug.Log("1x,y,z =>" + Car1.transform.position);
//Debug.Log("========>" + savePosition1);
//Debug.Log("2x,y,z =>" + Car2.transform.position);
//Debug.Log("========>" + savePosition2);
//Debug.Log("check1 : " + checkFix);
//Debug.Log("check2 : " + checkFix2);


	if(Time.timeScale == 0)
	{
		checkFix = 0;
		checkFix2 = 0;
	}
	
	if(Time.timeScale == 0 && pflag == 0)
	{
		pflag = 1;
		TM1 = 1;
		TM2 = 1;
	}
	else if(Time.timeScale == 0 && pflag > 0)
	{
		pflag++;
		if(pflag > 20)
		{
			TM1 *= -1;
			TM2 *= -1;
			pflag = 1;
		}
	}
	else if(Time.timeScale == 1 && pflag > 0)
	{
		pflag = 0;
		TM1 = 0;
		TM2 = 0;
	}

	checkFix += Ccnt;
	if(checkFix > 40)
	{
//		checkFix = 0;
		if((savePosition1 - Car1.transform.position).magnitude < 0.07 && kind1 == 0)
		{

			RePlaceCar1();
			//Car1.transform.position.y += 1.5;
			kind1 = 1;
		}
//		Car1.transform.position.y += 0.05;
//		checkFix = 0;
	}
	if(checkFix > 80)
	{
		savePosition1 = Car1.transform.position;
		checkFix = 0;
		kind1 = 0;
	}
	
	if(Mode == 2){
		checkFix2 += Ccnt;
		if(checkFix2 > 40)
		{
			if((savePosition2 - Car2.transform.position).magnitude < 0.07 && kind2 == 0)
			{
				RePlaceCar2();
				//Car2.transform.position.y += 1.5;
				kind2 = 1;
			}
		}
		if(checkFix2 > 80)
		{
			savePosition2 = Car2.transform.position;
			checkFix2 = 0;
			kind2 = 0;
		}
	}


	if(Car1.transform.position.y < -5)
	{
		RePlaceCar1();
	}

	if(Mode == 2){
		if(Car2.transform.position.y < -5)
		{
			RePlaceCar2();
		}
	}

	if(TT1 > 0)
	{
		fchk1 = 1;
		TT1++;
		if(TT1 == 10)
		{
			C1Real[cur1%7] = true;
			C1Real[chd1] = false;
			cur1++;
			chd1 = (cur1 + 6) % 7;
			TT1 = 0;
		}
	}
	if(Mode == 2){
		if(TT2 > 0)
		{
			fchk2 = 1;
			TT2++;
			if(TT2 == 10)
			{
				C2Real[cur2%7] = true;
				C2Real[chd2] = false;
				cur2++;
				chd2 = (cur2 + 6) % 7;
				TT2 = 0;
			}
		}
	}

	if(Mapnum < 3)
	{
		if(timerMsg1 > 0)
		{
			checkFix = 0;
			timerMsg1++;
			if(timerMsg1 % 20 == 0) TM1 *= -1;
			if(timerMsg1 == 10)
			{
				Car1.SendMessage("Stop",0);
				Car1.SendMessage("Stop",0);
			}
			else if(timerMsg1 == 50)
			{
				Car1.SendMessage("Callback");
				Car1.SendMessage("Callback");
			}
			else if(timerMsg1 == 110)
			{
				Car1.SendMessage("UnStop");
				Car1.SendMessage("UnStop");
	
				timerMsg1 = 0;
				TM1 = -1;
				RePlaceCar1();
			}
		}
		
		if(timerMsg2 > 0 && Mode == 2)
		{
			checkFix = 0;
			timerMsg2++;
			if(timerMsg2 % 20 == 0) TM2 *= -1;
			if(timerMsg2 == 10)
			{
				Car2.SendMessage("Stop",0);
				Car2.SendMessage("Stop",0);
			}
			else if(timerMsg2 == 50)
			{
				Car2.SendMessage("Callback");
				Car2.SendMessage("Callback");
			}
			else if(timerMsg2 == 110)
			{
				Car2.SendMessage("UnStop");
				Car2.SendMessage("UnStop");
				timerMsg2 = 0;
				TM2 = -1;
				RePlaceCar2();
			}
		}
	}
	
	chcnt1++;
	if(chcnt1 == 50)
	{
		chcnt1 = 0;
		if((cur1 % 7) == 0)
		{
			Ttmp12 = fen0.transform.position - Car1.transform.position;
		}
		else if((cur1 % 7) == 1)
		{
			Ttmp12 = fen1.transform.position - Car1.transform.position;
		}
		else if((cur1 % 7) == 2)
		{
			Ttmp12 = fen2.transform.position - Car1.transform.position;
		}
		else if((cur1 % 7) == 3)
		{
			Ttmp12 = fen3.transform.position - Car1.transform.position;
		}
		else if((cur1 % 7) == 4)
		{
			Ttmp12 = fen4.transform.position - Car1.transform.position;
		}
		else if((cur1 % 7) == 5)
		{
			Ttmp12 = fen5.transform.position - Car1.transform.position;
		}
		else if((cur1 % 7) == 6)
		{
			Ttmp12 = fen6.transform.position - Car1.transform.position;
		}
		if(Ttmp11.magnitude + 0.5 < Ttmp12.magnitude)
		{
			RC1++;
			if(RC1 == 4)
			{
				timerMsg1 = 1;
				TM1 = 1;
			}
		}
		else
		{
			RC1 = 0;
		}
		Ttmp11 = Ttmp12;
	}
	
	if(Mode == 2)
	{
		chcnt2++;
		if(chcnt2 == 50)
		{
			chcnt2 = 0;
			if((cur2 % 7) == 0)
			{
				Ttmp22 = fen0.transform.position - Car2.transform.position;
			}
			else if((cur2 % 7) == 1)
			{
				Ttmp22 = fen1.transform.position - Car2.transform.position;
			}
			else if((cur2 % 7) == 2)
			{
				Ttmp22 = fen2.transform.position - Car2.transform.position;
			}
			else if((cur2 % 7) == 3)
			{
				Ttmp22 = fen3.transform.position - Car2.transform.position;
			}
			else if((cur2 % 7) == 4)
			{
				Ttmp22 = fen4.transform.position - Car2.transform.position;
			}
			else if((cur2 % 7) == 5)
			{
				Ttmp22 = fen5.transform.position - Car2.transform.position;
			}
			else if((cur2 % 7) == 6)
			{
				Ttmp22 = fen6.transform.position - Car2.transform.position;
			}
			if(Ttmp21.magnitude + 0.5 < Ttmp22.magnitude)
			{
				RC2++;
				if(RC2 == 4)
				{
					timerMsg2 = 1;
					TM2 = 1;
				}
			}
			else
			{
				RC2 = 0;
			}
			Ttmp21 = Ttmp22;
		}
	}

	if(Mode == 2 && Mapnum < 3){
		if(cur1 > cur2) Winner = 1;
		else if(cur1 < cur2) Winner = 2;
		else
		{
			tmp1 = Car1.transform.position;
			tmp2 = Car2.transform.position;
			if((cur1 % 7) == 0)
			{
				tmp1 = tmp1 - fen0.transform.position;
				tmp2 = tmp2 - fen0.transform.position;
			}
			else if((cur1 % 7) == 1)
			{
				tmp1 = tmp1 - fen1.transform.position;
				tmp2 = tmp2 - fen1.transform.position;
			}
			else if((cur1 % 7) == 2)
			{
				tmp1 = tmp1 - fen2.transform.position;
				tmp2 = tmp2 - fen2.transform.position;
			}
			else if((cur1 % 7) == 3)
			{
				tmp1 = tmp1 - fen3.transform.position;
				tmp2 = tmp2 - fen3.transform.position;
			}
			else if((cur1 % 7) == 4)
			{
				tmp1 = tmp1 - fen4.transform.position;
				tmp2 = tmp2 - fen4.transform.position;
			}
			else if((cur1 % 7) == 5)
			{
				tmp1 = tmp1 - fen5.transform.position;
				tmp2 = tmp2 - fen5.transform.position;
			}
			else if((cur1 % 7) == 6)
			{
				tmp1 = tmp1 - fen6.transform.position;
				tmp2 = tmp2 - fen6.transform.position;
			}
			if(tmp1.magnitude < tmp2.magnitude) Winner = 1;
			else Winner = 2;
		}
		
		if(Winner == 1)
		{
			out1 = "Rank : 1/2";
			out2 = "Rank : 2/2";
		}
		else
		{
			out1 = "Rank : 2/2";
			out2 = "Rank : 1/2";
		}
	}
	if(Mapnum >= 3)
	{
		if(score1 > score2)
		{
			out1 = "Rank : 1/2";
			out2 = "Rank : 2/2";
		}
		else if(score1 < score2)
		{
			out1 = "Rank : 2/2";
			out2 = "Rank : 1/2";
		}
		else
		{
			out1 = "Rank : 1/2";
			out2 = "Rank : 1/2";
		}
	}
}

function RePlaceCar1()
{
	RC1 = 0;
	if(Mapnum <3){
	
		if(fchk1 == 0)
		{
			Car1.transform.position.y += 1.5;
		}
		else if(cur1 % 7 == 1)
		{
			Car1.transform.position = new Vector3(35,-1.8,365);
			Car1.transform.rotation = Quaternion.Euler(0,-112,0);
		}
		else if(cur1 % 7 == 2)
		{
			Car1.transform.position = new Vector3(1,-1.8,30);
			Car1.transform.rotation = Quaternion.Euler(0,-160,0);
		}
		else if(cur1 % 7 == 3)
		{
			Car1.transform.position = new Vector3(-197,-1.8,-40);
			Car1.transform.rotation = Quaternion.Euler(0,-143,0);
		}
		else if(cur1 % 7 == 4)
		{
			Car1.transform.position = new Vector3(-173,-1.8,-340);
			Car1.transform.rotation = Quaternion.Euler(0,-241,0);
		}
		else if(cur1 % 7 == 5)
		{
			Car1.transform.position = new Vector3(220,-1.8,-297);
			Car1.transform.rotation = Quaternion.Euler(0,-345,0);
		}
		else if(cur1 % 7 == 6)
		{
			if(Mapnum == 2) Car1.transform.position = new Vector3(243,15,-16);
			else Car1.transform.position = new Vector3(223,15,1);
			Car1.transform.rotation = Quaternion.Euler(0,-372,0);
		}
		else if(cur1 % 7 == 0)
		{
			Car1.transform.position = new Vector3(208,-1.8,351);
			Car1.transform.rotation = Quaternion.Euler(0,-411,0);
		}
	}
	else
	{
		if(kind1 == 1) Car1.transform.position = new Vector3(1947,256.3,2176);
		else Car1.transform.position.y += 1.5;
	}
}

function RePlaceCar2()
{
RC2 = 0;
	if(Mapnum <3)
	{
		if(Mode == 2){
			if(fchk2 == 0)
			{
				Car2.transform.position.y += 1.5;
			}
			else if(cur2 % 7 == 1)
			{
				Car2.transform.position = new Vector3(35,-1.8,362);
				Car2.transform.rotation = Quaternion.Euler(0,-112,0);
			}
			else if(cur2 % 7 == 2)
			{
				Car2.transform.position = new Vector3(4,-1.8,30);
				Car2.transform.rotation = Quaternion.Euler(0,-160,0);
			}
			else if(cur2 % 7 == 3)
			{
				Car2.transform.position = new Vector3(-197,-1.8,-44);
				Car2.transform.rotation = Quaternion.Euler(0,-143,0);
			}
			else if(cur2 % 7 == 4)
			{
				Car2.transform.position = new Vector3(-177,-1.8,-340);
				Car2.transform.rotation = Quaternion.Euler(0,-241,0);
			}
			else if(cur2 % 7 == 5)
			{
				Car2.transform.position = new Vector3(220,-1.8,-293);
				Car2.transform.rotation = Quaternion.Euler(0,-345,0);
			}
			else if(cur2 % 7 == 6)
			{
				if(Mapnum == 2) Car2.transform.position = new Vector3(247,15,-16);
				else Car2.transform.position = new Vector3(227,15,1);
				Car2.transform.rotation = Quaternion.Euler(0,-372,0);
			}
			else if(cur2 % 7 == 0)
			{
				Car2.transform.position = new Vector3(212,-1.8,351);
				Car2.transform.rotation = Quaternion.Euler(0,-411,0);
			}
		}
	}
	else
	{
		if(kind2 == 1) Car2.transform.position = new Vector3(1947,256.3,2180);
		else Car2.transform.position.y+=1.5;
	}
}

function SetMode(TT : int)
{
	Mode = TT % 10;
}

function OnTriggerEnter (other : Collider)
{
	var txt;
	var txt1;
	
	if(other.attachedRigidbody.name == "pig_cart_1p")
	{
		txt = "RaceCheck" + (cur1%7).ToString();
		txt1 = "RaceCheck" + (chd1).ToString();
		
		if(C1Real[chd1] == true && RealChecker.gameObject.name == txt1)
		{
			//timerMsg1 = 1; TM1 = 1;
		}
		else if(C1Real[cur1%7] == false && RealChecker.gameObject.name == txt)
		{
			TT1 = 1;
		}
	}
	
	if(other.attachedRigidbody.name == "pig_cart_2p")
	{
		txt = "RaceCheck" + (cur2%7).ToString();
		txt1 = "RaceCheck" + (chd2).ToString();
		
		if(C2Real[chd2] == true && RealChecker.gameObject.name == txt1)
		{
			//timerMsg2 = 1; TM2 = 1;
		}
		else if(C2Real[cur2%7] == false && RealChecker.gameObject.name == txt)
		{
			TT2 = 1;
		}
	}
				//Debug.Log(cur1);

}