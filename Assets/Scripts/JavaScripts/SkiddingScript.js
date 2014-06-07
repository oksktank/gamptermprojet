//#pragma strict
private var CurrentFrictionValue : float;
var skidAt : float = 1.5;
var soundEmition : float = 15;
private var soundWait : float;
var skidSound : GameObject;
var skidSmoke : GameObject;
var smokeDepth : float = 0.4;
var markWidth : float = 0.2;
var rearWheel : boolean;
private var skidding : int;
private var lastPos = new Vector3[2];
var skidMaterial : Material;
var Car1 : GameObject;
var gogos = 0.0;

function Start () {
skidSmoke.transform.position = transform.position;
skidSmoke.transform.position.y -= smokeDepth;
Car1 = GameObject.Find("pig_cart_1p");

}

function Update () {
	var hit : WheelHit;
	
	gogos = Car1.GetComponent(CarControlScript).gogo;
	
	transform.GetComponent(WheelCollider).GetGroundHit(hit);
	CurrentFrictionValue = Mathf.Abs(hit.sidewaysSlip);
	var rpm = transform.GetComponent(WheelCollider).rpm;
	
	if (skidAt <= CurrentFrictionValue && soundWait <= 0 || rpm < 300 && (Input.GetAxis("Vertical") > 0 || gogos > 0) && soundWait <= 0 && rearWheel && hit.collider){
	Instantiate(skidSound, hit.point, Quaternion.identity);
	soundWait = 1;
	}
	soundWait -= Time.deltaTime*soundEmition;	
	
	if(Car1.GetComponent(CarControlScript).itemCnt0 > 0)
	{
		skidSmoke.particleEmitter.emit = true;
		return;
	}
	
	if (skidAt <= CurrentFrictionValue || rpm < 300 && (Input.GetAxis("Vertical") > 0 || gogos > 0) && rearWheel && hit.collider){
	skidSmoke.particleEmitter.emit = true;
	SkidMesh();
	}
	
	else {
	skidSmoke.particleEmitter.emit = false;
	skidding = 0;
	}
	

	
}

function SkidMesh(){
	var hit : WheelHit;
	transform.GetComponent(WheelCollider).GetGroundHit(hit);
	 
	var mark : GameObject = new GameObject("Mark");
	var filter : MeshFilter = mark.AddComponent(MeshFilter);
	mark.AddComponent(MeshRenderer);
	var markMesh : Mesh = new Mesh();
	var vertices = new Vector3 [4];
	var triangles = new int[6];
	
	if (skidding == 0) {
	vertices[0] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Vector3(markWidth, 0.01, 0);
	vertices[1] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Vector3(-markWidth, 0.01, 0);
	vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Vector3(-markWidth, 0.01, 0);
	vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Vector3(markWidth, 0.01, 0);
	lastPos[0] = vertices[2];
	lastPos[1] = vertices[3];
	skidding = 1;
	}
	else {
	vertices[1] = lastPos[0];
	vertices[0] = lastPos[1];
	vertices[2] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Vector3(-markWidth, 0.01, 0);
	vertices[3] = hit.point + Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z)*Vector3(markWidth, 0.01, 0);
	lastPos[0] = vertices[2];
	lastPos[1] = vertices[3];
	}
	triangles = [0,1,2,0,3,2];
	markMesh.vertices = vertices;
	markMesh.triangles = triangles;
	markMesh.RecalculateNormals();
	var uvm = new Vector2[4];
	uvm[0] = Vector2(1,0);
	uvm[1] = Vector2(0,0);
	uvm[2] = Vector2(0,1);
	uvm[3] = Vector2(1,1);
	
	for (var i = 0; i < uvm.length; i++){
	uvm[i] = Vector2(markMesh.vertices[i].x, markMesh.vertices[i].z);
	}
	markMesh.uv = uvm;
	filter.mesh = markMesh;
	mark.renderer.material = skidMaterial;
	mark.AddComponent(DestroyTimerScript);

}