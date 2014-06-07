#pragma strict

var target : Transform;
var distance : float = 6.4;
var height : float = 1.4;
var rotationDamping : float = 3.0;
var heightDamping : float = 4.0;
var zoomRacio : float = 0.5;
var DefaultFOV : float = 60;
private var rotationVector : Vector3;
private var Cart1 : GameObject;
private var Cart2 : GameObject;
private var save : float[];
private var sum : float;
private var i : int;

function Awake()
{
	sum = 0;
	i = 0;
	save = new float[30];
}

function Start () {

	
	heightDamping = 4.0;
	zoomRacio = 0.8;
	Cart1 = GameObject.Find("pig_cart_1p");
	if(GameObject.Find("pig_cart_2p") != null) Cart2 = GameObject.Find("pig_cart_2p");
}


function LateUpdate () {
	var wantedAngle = rotationVector.y;
	var wantedHeight = target.position.y + height;
	var myAngle = transform.eulerAngles.y;
	var myHeight = transform.position.y;
	
	myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
	myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
	
	var currentRotation = Quaternion.Euler(0, myAngle, 0);

//	i1 = (i1 + 1) % 5;
//	sum1 = sum1 - save1[i] + myHeight;
//	save1[i] = myHeight;
//	myHeight = sum1 / 5;
	
	transform.position = target.position;
	transform.position -= currentRotation * Vector3.forward * distance;
	transform.position.y = myHeight;
	transform.LookAt(target);
}

function FixedUpdate(){

var localVilocity = target.InverseTransformDirection(target.rigidbody.velocity);
if(localVilocity.z<-0.5){
rotationVector.y = target.eulerAngles.y + 180;
}
else {
rotationVector.y = target.eulerAngles.y;
}
var acc = (target.rigidbody.velocity.magnitude);


i = (i + 1) % 30;
sum = sum - save[i] + acc;
save[i] = acc;
acc = sum / 30;

camera.fieldOfView = DefaultFOV + acc*zoomRacio;
//Debug.Log ("asdf " + acc + "asdfsadf  " + target.rigidbody.velocity);
}

function UpDown(){
	DefaultFOV = -60;	
}

function UnUpDown(){
	DefaultFOV = 60;
}