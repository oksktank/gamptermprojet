var pickupCount : int;
var coins : GameObject;

function Start(){
	
	pickupCount = 0;
	
}	
	
function Update(){

}

function OnGUI (){

	GUI.Label(Rect(Screen.width/2, 25, 500, 70), "Picked up " + pickupCount + "coins");

}


function OnTriggerEnter (coll : Collider){
	
	if(coll.gameObject.tag == "coin"){
		
		coll.gameObject.SendMessage("reLoad");
		pickupCount ++;
		
	}
}

	
	
	
		