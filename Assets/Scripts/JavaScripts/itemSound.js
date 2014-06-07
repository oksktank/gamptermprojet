
var boostitem1p : GameObject;
var boostitem2p : GameObject;
var bflag1p : int;
var bflag2p : int;
function Start () {
	boostitem1p = GameObject.Find("pig_cart_1p");
	if(GameObject.Find("pig_cart_2p") != null){
		boostitem2p = GameObject.Find("pig_cart_2p");
	}
	
	bflag1p = 0;
	bflag2p = 0;
}

function Update () {
	if(bflag1p == 0){
		bflag1p = boostitem1p.GetComponent(CarControlScript).boostFlag;
	}
	if(bflag1p >= 2){
//		bflag1p++;
		if(audio.time == 0) bflag1p = 0;
	}

	if(GameObject.Find("pig_cart_2p") != null){
		if(bflag2p == 0) bflag2p = boostitem2p.GetComponent(CarControlScript2).boostFlag;
		if(bflag2p >= 2){
//			bflag2p++;
			if(audio.time == 0)
			{
				bflag2p = 0;
			}
		}
	}
	
	
	if(bflag1p == 1){
		audio.Play();
		bflag1p++;
		//if(bflag1p == 140) bflag1p = 0;
	}
	
	if(bflag2p == 1){
		audio.Play();
		bflag2p++;
		//if(bflag2p == 140) bflag2p = 0;
	}	
}