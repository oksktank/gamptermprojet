//0
var timeItemBooster;
var timeItemBoosterStart;

//1
var timeItemReverse;
var timeItemReverseStart;

//2
var timeItemBlind;
var timeItemBlindStart;


//3
var timeItemStop;
var timeItemStopStart;

//4
var timeItemShield;
var timeItemShieldStart;

//5
var timeItemRecovery;
var timeItemRecoveryStart;

var useItem = new Array(6);
var timeItemStart = new Array(6);

var itemA;
var itemB;
var timeItemStart1;
var timeItemStart2;
var numA;
var numB;
var itemNum = -1;

var booster : Texture2D;
var reverse : Texture2D;
var blind : Texture2D;
var stop : Texture2D;
var shield : Texture2D;
var recovery : Texture2D;

static var TT : int;

var Mode : int;
var Mapnum : int;
var pickupCount : int;
var coin : int;
var race : GameObject;
var empty : int;
var i : int;

function Start(){
	race = GameObject.Find("RealRace");
	coin = 0;
	pickupCount = 0;
	empty = 0;

	for(i=0; i<6; i++)
	{
		useItem[i] = false;
	}	

	timeItemBooster = 3;
	
	timeItemReverse = 5;
	
	timeItemBlind = 5 ;
	
	timeItemStop = 7;
	
	timeItemShield = 5;
	
	timeItemRecovery = 1;
	
	itemA = -1;
	itemB = -1;
//	if(GameObject.Find("pig_cart_2p") == null) empty = 1;
	
	TT = 0;
	if(GameObject.Find("CoinPickup") != null) coin = 1;

}	
	
function Update(){

	if(GameObject.Find("pig_cart_2p") == null) empty = 1;

	if(TT > 0)
	{
		TT++;
		if(TT == 6)
		{
			pickupCount++;
			race.SendMessage("Setscore", 1);
			if(GameObject.Find("pig_cart_2p") == null) itemNum = 0;
			if(itemA == -1)
			{
				itemA = itemNum;
			}
			else if(itemB == -1 && itemA != -1)
			{
				itemB = itemNum;
			}
			TT = 0;
		}
	}


	if(Input.GetButtonDown("Fire1")){
		UseItem();
		//timer
	}
	
	if(useItem[0])
	{
		if((Time.time - timeItemStart[0]) > timeItemBooster)
		{
			useItem[0] = false;
			GetComponent(CarControlScript).UnBooster();
		}
	}
	if(useItem[1])
	{
		if((Time.time - timeItemStart[1]) > timeItemReverse)
		{
			useItem[1] = false;
			//GetComponent(CarControlScript2).UnReverse();
			GameObject.Find ("pig_cart_2p").SendMessage("UnReverse");
		}
	}
	if(useItem[2])
	{
		if((Time.time - timeItemStart[2]) > timeItemBlind)
		{
			useItem[2] = false;
			GameObject.Find ("pig_cart_2p").SendMessage("UnBlind");
		}
	}
	if(useItem[3])
	{
		if((Time.time - timeItemStart[3]) > timeItemStop)
		{
			useItem[3] = false;
			GameObject.Find ("pig_cart_2p").SendMessage("UnStop");
		}
	}
	if(useItem[4])
	{
		if((Time.time - timeItemStart[4]) > timeItemShield)
		{
			useItem[4] = false;
			GetComponent(CarControlScript).UnShield();
		}
	}
	if(useItem[5])
	{
		if((Time.time - timeItemStart[5]) > timeItemRecovery)
		{
			useItem[5] = false;
		}
	}	
	
/*	if(useItemA)
	{
		switch(numA){
		case -1:	//nothing
			break;
			
		case 0: 	//Booster
		
			if((Time.time - timeItemStart1) > timeItemBooster)
			{
				useItemA = false;
				GetComponent(CarControlScript).UnBooster();
			}
			break;
		
		case 1:		//Reverse
			if((Time.time - timeItemStart1) > timeItemReverse)
			{
				useItemA = false;
				//GetComponent(CarControlScript2).UnReverse();
				GameObject.Find ("pig_cart_2p").SendMessage("UnReverse");
			}
			break;
		
		case 2:		//Blind
			if((Time.time - timeItemStart1) > timeItemBlind)
			{
				useItemA = false;
				GameObject.Find ("pig_cart_2p").SendMessage("UnBlind");
			}
			break;
			
		case 3:		//Stop
			if((Time.time - timeItemStart1) > timeItemStop)
			{
				useItemA = false;
				GameObject.Find ("pig_cart_2p").SendMessage("UnStop");
			}
			break;
			
		case 4:		//Shield
			if((Time.time - timeItemStart1) > timeItemShield)
			{
				useItemA = false;
				GetComponent(CarControlScript).UnShield();
			}			
			break;	
			
		case 5:
			if((Time.time - timeItemStart1) > timeItemRecovery)
			{
				useItemA = false;
			}			
			break;	
		}
	}
	
	if(useItemB)
	{
		switch(numB){
		case -1:	//nothing
			break;
			
		case 0: 	//Booster
		
			if((Time.time - timeItemStart2) > timeItemBooster)
			{
				useItemB = false;
				GetComponent(CarControlScript).UnBooster();
			}
			break;
		
		case 1:		//Reverse
			if((Time.time - timeItemStart2) > timeItemReverse)
			{
				useItemB = false;
				//GetComponent(CarControlScript2).UnReverse();
				GameObject.Find ("pig_cart_2p").SendMessage("UnReverse");
			}
			break;
		
		case 2:		//Blind
			if((Time.time - timeItemStart2) > timeItemBlind)
			{
				useItemB = false;
				GameObject.Find ("pig_cart_2p").SendMessage("UnBlind");
			}
			break;
			
		case 3:		//Stop
			if((Time.time - timeItemStart2) > timeItemStop)
			{
				useItemB = false;
				GameObject.Find ("pig_cart_2p").SendMessage("UnStop");
			}
			break;
			
		case 4:		//Shield
			if((Time.time - timeItemStart2) > timeItemShield)
			{
				useItemB = false;
				GetComponent(CarControlScript).UnShield();
			}			
			break;	
			
		case 5:		//Recovery
			if((Time.time - timeItemStart2) > timeItemRecovery)
			{
				useItemB = false;
			}			
			break;	
		}	
	}*/
}

function OnGUI (){

	
	if(itemA == 0){
		GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30,80,80), booster);
	}
	else if(itemA == 1){
		GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30,80,80), reverse);
	}
	else if(itemA == 2){
		GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30,80,80), blind);
	}
	else if(itemA == 3){
		GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30,80,80), stop);
	}
	else if(itemA == 4){
		GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30,80,80), shield);
	}
	else if(itemA == 5){
		GUI.DrawTexture(Rect(Screen.width - 100,Screen.height/30,80,80), recovery);
	}
	
	if(itemB == 0){
		GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), booster);
	}
	else if(itemB == 1){
		GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), reverse);
	}
	else if(itemB == 2){
		GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), blind);
	}
	else if(itemB == 3){
		GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), stop);
	}
	else if(itemB == 4){
		GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), shield);
	}
	else if(itemB == 5){
		GUI.DrawTexture(Rect(Screen.width - 200,Screen.height/30,80,80), recovery);
	}

	if(coin == 1){
		GUI.contentColor = Color.yellow;
		GUI.skin.label.fontSize = 40;
		if(empty == 1)
		{
			GUI.Label(Rect(Screen.width - 230, Screen.height - 190 , 250, 70), "Coins : " + pickupCount);
		}
		else {
			GUI.Label(Rect(Screen.width - 230, Screen.height/2 - 190 , 250, 70), "Coins : " + pickupCount);
		}
	}
}


function OnTriggerEnter (coll : Collider){
	if(coll.gameObject.tag == "item"){
		coll.gameObject.SendMessage("getItem");
		if(GameObject.Find("CoinPickup") != null) GameObject.Find("CoinPickup").audio.Play();
		//Destroy(coll.gameObject);
		itemNum = Random.Range(0,6);
		//itemNum = 4;
		TT =1;
	}
}

function SetItem()
{
	TT = 1;
}


function UseItem(){

	var num = -1;
//	var sw = 0;
			
	if(itemB >= 0)
	{
		num = itemB;
		itemB = -1;
//		useItemB = true;
	}
	else if(itemA >=0)
	{
		num = itemA;
		itemA = -1;
//		useItemA = true;
//		sw = 1;
//		numA = num;
	}

	if(num >= 0)
	{
		useItem[num] = true;
		timeItemStart[num] = Time.time;
	}

	if(useItem[0])
	{
		GetComponent(CarControlScript).Booster();
	}
	if(useItem[1])
	{
		GameObject.Find ("pig_cart_2p").SendMessage("Reverse");
	}
	if(useItem[2])
	{
		GameObject.Find ("pig_cart_2p").SendMessage("Blind");
	}
	if(useItem[3])
	{
		GameObject.Find ("pig_cart_2p").SendMessage("Stop", 1);
	}
	if(useItem[4])
	{
		GetComponent(CarControlScript).Shield();
	}
	if(useItem[5])
	{
		GetComponent(CarControlScript).Recovery();
	}

/*	switch(num){
		case -1:	//nothing
			break;
		
		case 0: 	//Booster
			GetComponent(CarControlScript).Booster();	
			break;
		
		case 1:		//Reverse
			GameObject.Find ("pig_cart_2p").SendMessage("Reverse");
//			pig_cart_2p.GetComponent(CarControlScript2).Reverse();
			break;
		
		case 2:		//Blind
			GameObject.Find ("pig_cart_2p").SendMessage("Blind");
			break;
			
		case 3:		//Stop
			GameObject.Find ("pig_cart_2p").SendMessage("Stop", 1);
			break;
			
		case 4:		//Shield
			GetComponent(CarControlScript).Shield();
			break;
		
		case 5:		//Recovery
			GetComponent(CarControlScript).Recovery();
			break;
			
	}*/

}




	
	
	
		