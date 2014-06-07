//#pragma strict
var wheelFL : WheelCollider;
var wheelFR : WheelCollider;
var wheelRL : WheelCollider;
var wheelRR : WheelCollider;
var wheelFLTrans : Transform;
var wheelFRTrans : Transform;
var wheelRLTrans : Transform;
var wheelRRTrans : Transform;
var lowestSteerAtSpeed : float = 50;
var lowSpeedSteerAngel : float = 10;
var highSpeedSteerAngel : float = 1;
var decellarationSpeed : float = 40;
var maxReverseSpeed : float = 50;
private var mySidewayFriction : float;
private var myForwardFriction : float;
private var slipSidewayFriction : float;
private var slipForwardFriction : float;
var gearRatio : int[];

var maxTorque : float = 50;
//@HideInInspector
var currentSpeed : float = 150;
var topSpeed : float;
var centerOfMass : Vector3;
private var braked : boolean = false;
var maxBrakeTorque : float = 200;
var speedOMeterDial : Texture2D;
var speedOMeterPointer : Texture2D;

var inventory : Texture2D;
var inventory2 : Texture2D;
var con : Texture2D;
var dis : Texture2D;

var flag1 : int = 1;

//var gearRatio : int[];
var spark : GameObject;
var collisionSound : GameObject;
var shieldFlag : int;

var dir = 1;

public var itemCnt0 : int;
public var itemCnt1 : int;
public var itemCnt2 : int;
public var itemCnt3 : int;
public var itemCnt4 : int;
public var itemCnt5 : int;
public var itemTime;
public var playMode : int;

var ctrler : GameObject;
var test = "";
public var gogo : float;
public var hoi : float;
var Iflag = 0;
var obj_item : GameObject;

public var boostFlag : int;


var Item1 : GameObject;
var Item2 : GameObject;
var Item3 : GameObject;
var Item4 : GameObject;
var Item5 : GameObject;
var Item6 : GameObject;

var mT : int;
var tS : int;
var lowA : float;
var highA : int;
var miss : int;

var rot;
var posflag : int;
var colflag = 0;
var cart : int;
var b_Iflag : int;
var connect;

function Pause()
{
	maxTorque = 0;
	flag1 = 1;
}

function Resume()
{
	maxTorque = mT;
	flag1 = 0;
}

function Start () {
	Iflag = 0 ;
	flag1 = 1;
	b_Iflag = 0;
	rigidbody.centerOfMass = centerOfMass;
	
///////////////////////////////////////////////	
	ctrler = GameObject.Find("Controller");
	obj_item = GameObject.Find("pig_cart_1p");
	gogo =0;
	hoi = 0;
	posflag = 0;
	rot = obj_item.transform.rotation;
	colflag = 0;
///////////////////////////////////////////////

	Item1 = GameObject.Find("Barrier1");
	Item2 = GameObject.Find("Shield1");
	Item3 = GameObject.Find("Turtle1");
	Item4 = GameObject.Find("UNAssigned1");
	Item5 = GameObject.Find("Booster1");
	Item6 = GameObject.Find("Recovery1");
	
	Item1.transform.position = new Vector3(-150,-100,-300);
	Item2.transform.position = new Vector3(-150,-100,-300);
	Item3.transform.position = new Vector3(-150,-100,-300);
	Item4.transform.position = new Vector3(-150,-100,-300);
	Item5.transform.position.y = obj_item.transform.position.y + 1000;
	Item6.transform.position.y = obj_item.transform.position.y + 1000;

	SetValues();
	dir = 1;
	shieldFlag = 0;
	
	itemCnt0 = 0;
	itemCnt1 = 0;
	itemCnt2 = 0;
	itemCnt3 = 0;
	itemCnt4 = 0;
	itemCnt5 = 0;
	itemTime = Time.time;
}
function SetValues () {
//	myForwardFriction = wheelRR.forwardFriction.stiffness;
//	mySidewayFriction = wheelRR.sidewaysFriction.stiffness;
	slipForwardFriction = 0.04;
	slipSidewayFriction = 0.08;
}


function FixedUpdate () {
	Controle ();
	HandBrake();
}

function SetCart(cc : int)
{
	cart = cc;
	if(cc == 1)
	{
		mT = 70;
		tS = 120;
		lowA = 3.5;
		highA = 1;
		miss = 10;
	}
	else if(cc == 2)
	{
		mT = 60;
		tS = 130;
		lowA = 3.5;
		highA = 1;
		miss = 10;
	}
	else
	{
		mT = 80;
		tS = 140;
		lowA = 3.5;
		highA = 1;
		miss = 10;
		wheelFLTrans.transform.position.y = -40;
		wheelFRTrans.transform.position.y = -40;
		wheelRLTrans.transform.position.y = -40;
		wheelRRTrans.transform.position.y = -40;
	}
	maxTorque = mT;
	topSpeed = tS;
	lowSpeedSteerAngel = lowA;
	highSpeedSteerAngel = highA;
	Pause();
}



function Controle () {
	var kk;	
	
///////////////////////////////////////////////
	test = ctrler.GetComponent("Controller").direction1;
	connect = ctrler.GetComponent("Controller").P1_connect;

	
//	Debug.Log("test1 : " + test);
//	Debug.Log("gogo : " + gogo + " hoi : " + hoi);
	
	/*
	press = int.Parse(test);
	
	if(press < 100)
		gogo = press / 70.0;
	else if(press < 300)
		gogo = -1 * ((press%200) / 70.0);
	else if(press < 500)
		hoi = -1 * (press%200) / 70.0;
	else if(press < 700)
		hoi = ((press % 200) /70.0);
	*/
	if(test.Length > 0)
	{
		if(test[0] == 'F')
		{
			gogo += 0.2;
		}
		else if(test[0] == 'B')
		{
			gogo -= 0.2;
		}
		
		if(test[1] == 'L')
		{
			hoi -= 0.05;
			if(test[0] == 'W') gogo += 0.1;
			if(currentSpeed <5) hoi = -1;
		}
		else if(test[1] == 'R')
		{
			hoi += 0.05;
			if(test[0] == 'W') gogo += 0.1;
			if(currentSpeed < 5) hoi = 1;
		}
		else
		{
			hoi = 0;
		}
		
	  	 if(test[0] == 'I' || test[1] == 'I')
	      {
	         // using item.
	         if(b_Iflag == 0)
	         {
	            obj_item.GetComponent("getItem").UseItem();
	            b_Iflag = 1;
	            Iflag = 0;
	         }
	      }
	      
	      if(b_Iflag == 1)
	      {
	         Iflag ++;
	         
	         if(Iflag > 200)
	         {
	          Iflag = 0;
	          b_Iflag = 0;
	         }
	      }
	}
	
	gogo = Mathf.Clamp(gogo,-1.000f,1.000f);
	hoi = Mathf.Clamp(hoi,-1.000f,1.000f);
	


///////////////////////////////////////////////
	if(test.Length <= 0 || test == "EE")
	{
		gogo = Input.GetAxis("Vertical");
	}	

	currentSpeed = 2*22/7*wheelRL.radius*wheelRL.rpm*60/1000;
	currentSpeed = Mathf.Round(currentSpeed);
	
	if(gogo > 0 && currentSpeed < -5)
	{
		maxTorque = 200;
	}
	else
	{
		maxTorque = mT;
	}
	
	
	if(colflag >= 1)
	{
		maxTorque = 0;
		lowA = 6;
//		gogo = -0.3f;
		colflag++;
		if(colflag > 7)
		{	
			//gogo = -1f;
			//maxTorque = 1000;
		}
		if(colflag == 20)
		{
			gogo = 0f;
		}
		else if(colflag > 20)
		{
			maxTorque = mT;
			if(colflag == 60)
			{
				colflag = 0;
				lowA = 3.5;
			}
		}
	}
	
	
	if(itemCnt3 > 0) maxTorque = 0;
	
	if (currentSpeed <= topSpeed && currentSpeed >= -maxReverseSpeed){
	
	wheelRR.motorTorque = maxTorque * gogo;
	wheelRL.motorTorque = maxTorque * gogo;
	}
	else {
	wheelRR.motorTorque = 0;
	wheelRL.motorTorque = 0;
	}
	
	if (Input.GetButton("Vertical") == false || test == "EE" || itemCnt3 > 0){
		wheelRR.brakeTorque = decellarationSpeed;
		wheelRL.brakeTorque = decellarationSpeed;
	}
	else {
	wheelRR.brakeTorque = 0;
	wheelRL.brakeTorque = 0;
	}

	var speedFactor = rigidbody.velocity.magnitude/lowestSteerAtSpeed;
	var currentSteerAngel = Mathf.Lerp(lowSpeedSteerAngel,highSpeedSteerAngel,speedFactor);

	
	if(test.Length <= 0 || test == "EE")
	{
		hoi = Input.GetAxis("Horizontal");
	}
	if(currentSpeed < 0) hoi *= -1;
	currentSteerAngel *= hoi * dir;
	
	if((gogo > 0 && hoi == 0 && currentSpeed > 5) || colflag >=1)
	{
		if(posflag == 0)
		{
			posflag = 1;
			rot = obj_item.transform.rotation; 
		}
		obj_item.transform.rotation.y = rot.y;
	}
	else
	{
		posflag = 0;
	}

	wheelFL.steerAngle = currentSteerAngel;
	wheelFR.steerAngle = currentSteerAngel;
}

function SetMode(TT:int)
{
	playMode = TT%10;
}


function WheelPosition(){
	if(cart == 3) return;
	var hit : RaycastHit;
	var wheelPos : Vector3;
	if (Physics.Raycast(wheelFL.transform.position, -wheelFL.transform.up, hit, wheelFL.radius+wheelFL.suspensionDistance)){
		wheelPos = hit.point + wheelFL.transform.up * wheelFL.radius;
	}

	else {
		wheelPos = wheelFL.transform.position - wheelFL.transform.up*wheelFL.suspensionDistance;
	}

	wheelFLTrans.position = wheelPos;

	if (Physics.Raycast(wheelFR.transform.position, -wheelFR.transform.up, hit, wheelFR.radius+wheelFR.suspensionDistance)){
		wheelPos = hit.point + wheelFR.transform.up * wheelFR.radius;
	}

	else {
		wheelPos = wheelFR.transform.position - wheelFR.transform.up*wheelFR.suspensionDistance;
	}
	
	wheelFRTrans.position = wheelPos;

	if (Physics.Raycast(wheelRL.transform.position, -wheelRL.transform.up, hit, wheelRL.radius+wheelRL.suspensionDistance)){
		wheelPos = hit.point + wheelRL.transform.up * wheelRL.radius;
	}

	else {
		wheelPos = wheelRL.transform.position - wheelRL.transform.up*wheelRL.suspensionDistance;
	}
		
	wheelRLTrans.position = wheelPos;

	if (Physics.Raycast(wheelRR.transform.position, -wheelRR.transform.up, hit, wheelRR.radius+wheelRR.suspensionDistance)){
		wheelPos = hit.point + wheelRR.transform.up * wheelRR.radius;
	}

	else {
		wheelPos = wheelRR.transform.position - wheelRR.transform.up*wheelRR.suspensionDistance;
	}

	wheelRRTrans.position = wheelPos;
}


function HandBrake(){
	if (Input.GetButton("Jump") || flag1 == 1 || currentSpeed < -50 || currentSpeed > topSpeed){
		braked = true;
	}
	else{
		braked = false;
	}

	if (braked){
		wheelFR.brakeTorque = maxBrakeTorque;
		wheelFL.brakeTorque = maxBrakeTorque;
		wheelRR.motorTorque = 0;
		wheelRL.motorTorque = 0;	
//		if (rigidbody.velocity.magnitude > 1 ){
//		SetSlip(slipForwardFriction, slipSidewayFriction);
//		}
		
//		else {
//		SetSlip(1, 1);
//		}
	}
	else{
		wheelFR.brakeTorque = 0;
		wheelFL.brakeTorque = 0;
//		SetSlip (myForwardFriction, mySidewayFriction);
	}
}
/*
function SetSlip (currentForwardFriction : float, currentSidewayFriction : float){
	wheelRR.forwardFriction.stiffness = currentForwardFriction;
	wheelRL.forwardFriction.stiffness = currentForwardFriction;
	wheelRR.sidewaysFriction.stiffness = currentSidewayFriction;
	wheelRL.sidewaysFriction.stiffness = currentSidewayFriction;

}
*/

function EngineSound(){

	for (var i = 0; i < gearRatio.Length; i++){
	if(gearRatio[i] > currentSpeed){
	break;
	}
	}
	var gearMinValue : float = 0.00;
	var gearMaxValue : float = 0.00;
	if (i == 0){
	gearMinValue = 0;
	gearMaxValue = gearRatio[i];

	}
	else  if(i < gearRatio.Length ){	
	gearMinValue = gearRatio[i-1];
	gearMaxValue = gearRatio[i];
	}
	var enginePitch : float = (currentSpeed - gearMinValue)/(gearMaxValue - gearMinValue);
	audio.pitch = enginePitch;
}

function OnGUI (){
	GUI.skin.label.fontSize = 50;
	GUI.contentColor = Color.magenta;
//	if(itemCnt0 == 1) GUI.Label(Rect(Screen.width/5 * 2, Screen.height/6, 400, 70), "Booster!!");
//	else if(itemCnt1 == 1) GUI.Label(Rect(Screen.width/5 * 2, Screen.height/6, 400, 70), "Confused!!");
//	else if(itemCnt2 == 1) GUI.Label(Rect(Screen.width/5 * 2, Screen.height/6, 400, 70), "UpsideDown!!");
//	else if(itemCnt3 == 1) GUI.Label(Rect(Screen.width/5 * 2, Screen.height/6, 400, 70), "Slow!!");
//	else if(itemCnt4 == 1) GUI.Label(Rect(Screen.width/5 * 2, Screen.height/6, 400, 70), "Shield!!");
//	else if(itemCnt5 == 1) GUI.Label(Rect(Screen.width/5 * 2, Screen.height/6, 400, 70), "Recovery!!");

	if(connect)
	{
		GUI.DrawTexture(Rect(Screen.width/2 - 100 , 0 ,200,60),con);
	}
	else
	{
		GUI.DrawTexture(Rect(Screen.width/2 - 100 , 0 ,200,60),dis);	
	}
	
	GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), inventory);
	GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30 ,80,80), inventory2);
	
	if(playMode == 2) GUI.DrawTexture(Rect(Screen.width - 300,Screen.height/2 -150,300,150), speedOMeterDial);  //530
	else GUI.DrawTexture(Rect(Screen.width - 300,Screen.height -150,300,150), speedOMeterDial);

	var speedFactor : float = currentSpeed / (tS + 50);
	var rotationAngle : float;
	if (currentSpeed >= 0){
	rotationAngle = Mathf.Lerp(0,180,speedFactor);
	}
	else {
	rotationAngle = Mathf.Lerp(0,180,-speedFactor);
	}
	
	if(playMode == 2){
		GUIUtility.RotateAroundPivot(rotationAngle, Vector2(Screen.width-150 , Screen.height/2 ));
		GUI.DrawTexture(Rect(Screen.width - 300,Screen.height/2 -150 ,300,300), speedOMeterPointer);  //380
	} else {
		GUIUtility.RotateAroundPivot(rotationAngle, Vector2(Screen.width-150 , Screen.height ));
		GUI.DrawTexture(Rect(Screen.width - 300,Screen.height -150 ,300,300), speedOMeterPointer);
	}

}

function OnCollisionEnter (other : Collision){
	if(other.gameObject.name == "fence"){
		if (other.transform != transform && other.contacts.Length != 0){
			for (var i = 0; i <other.contacts.length; i++){
				Instantiate(spark, other.contacts[i].point, Quaternion.identity);
				var clone : GameObject = Instantiate(collisionSound, other.contacts[i].point, Quaternion.identity);
				clone.transform.parent = transform;
			}
		}
	}
	if( other.gameObject.name == "fence" && currentSpeed >= 0) colflag = 1;
}

function UsingItem()
{
	if(Time.time > itemTime + 1)
	{
		if(itemCnt0 == 1)
		{
			itemCnt0 = 2;

		}
		if(itemCnt1 == 1)
		{
			itemCnt1 = 2;
			if(shieldFlag == 0) dir *= -1;
		}
		if(itemCnt2 == 1)
		{
			itemCnt2 = 2;
			if(shieldFlag == 0 && playMode == 2) GameObject.Find ("Camera_1p").SendMessage("UpDown");
			else if(shieldFlag == 0) GameObject.Find("Camera_single").SendMessage("UpDown");
		}
		if(itemCnt3 == 1)
		{
			itemCnt3 = 2;
			if(shieldFlag == 0) maxTorque = 0;
		}
		if(itemCnt4 == 1)
		{
//			itemCnt4 = 2;
			shieldFlag = 1;
		}
		if(itemCnt5 == 1)
		{
			Item6.transform.position = new Vector3(-150,-100,-300);
			itemCnt5 = 0;
			UnBlind();
			UnStop();
			UnReverse();
		}
	}
}

function Update(){

//	Debug.Log("Speed " + currentSpeed + " maxTor " + maxTorque + "flag1 " + flag1 + " top : " + topSpeed + "Colflag " + colflag);

	UsingItem();
	


//	Debug.Log("currentSpeed : " + maxTorque);

	if(itemCnt0 >= 1) // Booster
	{
//		Item5.transform.position = obj_item.transform.position;
//		Item5.transform.rotation = obj_item.transform.rotation;
//		Item5.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
	}
	if(itemCnt1 >= 1 && itemCnt4 == 0) // Confuse
	{
		Item4.transform.position = obj_item.transform.position;
	}
	if(itemCnt2 >= 1) // Blind
	{
		
	}
	if(itemCnt3 >= 1 && itemCnt4 == 0) // Stop
	{
		Item3.transform.position = obj_item.transform.position;
	}
	if(itemCnt4 >= 1) // Shield
	{
		Item2.transform.position = obj_item.transform.position;
		Item1.transform.position = obj_item.transform.position;
	}
	if(itemCnt5 >= 1) // Recovery
	{
		
	}

	wheelFLTrans.Rotate(wheelFL.rpm/60*360*Time.deltaTime,0,0);
	wheelFRTrans.Rotate(wheelFR.rpm/60*360*Time.deltaTime,0,0);
	wheelRLTrans.Rotate(wheelRL.rpm/60*360*Time.deltaTime,0,0);
	wheelRRTrans.Rotate(wheelRR.rpm/60*360*Time.deltaTime,0,0);
	wheelFLTrans.localEulerAngles.y = wheelFL.steerAngle - wheelFLTrans.localEulerAngles.z;
	wheelFRTrans.localEulerAngles.y = wheelFR.steerAngle - wheelFRTrans.localEulerAngles.z;

	WheelPosition();
	EngineSound();

}


function Booster()
{
	Item5.transform.position = obj_item.transform.position;
	itemCnt0 = 1;
	itemTime = Time.time;
	maxTorque = mT + 50;
	topSpeed = tS + 50;
	boostFlag = 1;
}

function UnBooster()
{
	maxTorque = mT;
	topSpeed = tS;
	itemCnt0 = 0;
	Item5.transform.position = new Vector3(-150,-100,-300);
	boostFlag = 0;
}

function Reverse()
{
	if(shieldFlag == 1) return;
	itemCnt1 = 1;
	itemTime = Time.time;
}

function UnReverse()
{
	dir = 1;
	itemCnt1 = 0;
	Item4.transform.position = new Vector3(-150,-100,-300);
}


function Blind()
{
	if(shieldFlag == 1) return;
	itemCnt2 = 1;
	itemTime = Time.time;
}

function UnBlind()
{
	if(playMode == 2)
	{
		GameObject.Find ("Camera_1p").SendMessage("UnUpDown");
		GameObject.Find ("Camera_1p").SendMessage("UnUpDown");

	}
	else{
		GameObject.Find ("Camera_single").SendMessage("UnUpDown");
		GameObject.Find ("Camera_single").SendMessage("UnUpDown");

	}
	itemCnt2 = 0;

}

function Stop(sit : int)
{
	if(shieldFlag == 1) return;
	if(shieldFlag == 0) maxTorque = 0;
	if(sit == 1) itemCnt3 = 1;
	itemTime = Time.time;
}

function UnStop()
{
	maxTorque = mT;
	flag1 = 0;
	itemCnt3 = 0;
	Item3.transform.position = new Vector3(-150,-100,-300);
}

function Shield()
{
	shieldFlag = 1;
	itemCnt4 = 1;
	itemTime = Time.time;
}

function UnShield()
{
	shieldFlag = 0;
	itemCnt4 = 0;
	
	Item1.transform.position = new Vector3(-150,-100,-300);
	Item2.transform.position = new Vector3(-150,-100,-300);
}

function Recovery()
{
	Item6.transform.position = obj_item.transform.position;
	itemCnt5 = 1;
	itemTime = Time.time;
}

function Callback()
{
	flag1 = 1;
}