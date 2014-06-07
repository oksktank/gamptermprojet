using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
	// Use this for initialization
	
	//WheelCollider
	public WheelCollider FL;
	public WheelCollider FR;	
	public WheelCollider RR;
	public WheelCollider RL;
	
	//Wheel modeling
	public Transform WheelFL;
	public Transform WheelFR;
	public Transform WheelRL;
	public Transform WheelRR;
	
	//center of mass
	public Vector3 centerOfMass;
	
	//etc values setting
	public float maxSteer = 5f;
	public float maxTorque = 20f;
	public float maxBrake = 100f;
	
	//current speed init
	float currentSpeed = 0f;
	
	// key value init
	float steer = 0f;
	float forward = 0f;
	float back = 0f;
	
	//determine forward or back
	bool reverse = false;
	
	//motor and brake init
	float motor = 0f;
	float brake = 0f;
	
	void Start () {
		//set center of mass
		rigidbody.centerOfMass = centerOfMass;		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		

		//save current speed
		currentSpeed = rigidbody.velocity.sqrMagnitude;
		
		//save input value
		steer = Mathf.Clamp(Input.GetAxis ("Horizontal"), -1, 1);
		forward = Mathf.Clamp(Input.GetAxis ("Vertical"), 0, 1);
		back = -1 * Mathf.Clamp(Input.GetAxis ("Vertical"), -1, 0);
		
		//determine forward or back when rigidbody.velocity == 0
		if(currentSpeed == 0f){
			if (back > 0){
				reverse = true;
			}
			if (forward > 0){
				reverse = false;
			}
		}
		
		//set motor and brake value
		if (reverse){
			motor = -1 * back;
			brake = back;
		} else{
			motor = forward;
			brake = back;
		}
		
		//rear wheel(motor)
		RL.motorTorque = maxTorque * motor;
		RR.motorTorque = maxTorque * motor;
		
		//rear wheel (brake)
		RL.brakeTorque = maxBrake * brake;
		RR.brakeTorque = maxBrake * brake;
		
		//set direction of front wheel
		FL.steerAngle = maxSteer * steer;
		FR.steerAngle = maxSteer * steer;
		
		// set rotation motion of front wheel
		WheelFL.localEulerAngles = new Vector3 (WheelFL.localEulerAngles.x, maxSteer * steer , WheelFL.localEulerAngles.z);	
		WheelFR.localEulerAngles = new Vector3 (WheelFR.localEulerAngles.x, maxSteer * steer , WheelFR.localEulerAngles.z);
		
		
		//read rpm value and rolling wheels
		WheelFL.Rotate(FL.rpm * Time.deltaTime, 0f, 0f);
		WheelFR.Rotate(FR.rpm * Time.deltaTime, 0f, 0f);
		WheelRL.Rotate(RL.rpm * Time.deltaTime, 0f, 0f);
		WheelRR.Rotate(RR.rpm * Time.deltaTime, 0f, 0f);
	
	}
}
