using UnityEngine;
using System.Collections;

public class BodyController2 : MonoBehaviour {
	
	public bool useQueuedAnim = false;
	
	readonly string LD = "Hleftdown";
	readonly string LU = "Hleftup";
	readonly string RD = "Hrightdown";
	readonly string RU = "Hrightup";
	readonly string FD = "Hfrontdown";
	readonly string FU = "Hfrontup";
	readonly string BD = "Hbackdown";
	readonly string BU = "Hbackup";
	readonly string LS = "Hleftswing";
	readonly string RS = "Hrightswing";
	readonly string LSB = "Hleftswingback";
	readonly string RSB = "Hrightswingback";

	GameObject Car2;
	string test;
	int flag1, flag2, flag3, flag4;
	
	// Use this for initialization
	void Start () {
		animation[LD].wrapMode = WrapMode.ClampForever;
		animation[LU].wrapMode = WrapMode.ClampForever;
		animation[RD].wrapMode = WrapMode.ClampForever;
		animation[RU].wrapMode = WrapMode.ClampForever;
		animation[FD].wrapMode = WrapMode.ClampForever;
		animation[FU].wrapMode = WrapMode.ClampForever;
		animation[BD].wrapMode = WrapMode.ClampForever;
		animation[BU].wrapMode = WrapMode.ClampForever;
		animation[LS].wrapMode = WrapMode.ClampForever;
		animation[RS].wrapMode = WrapMode.ClampForever;
		animation[LSB].wrapMode = WrapMode.ClampForever;
		animation[RSB].wrapMode = WrapMode.ClampForever;
		Car2 = GameObject.Find ("Controller");
	}
	
	// Update is called once per frame
	void Update () {
		
		
		
/////////////////////////////////////////////////
		test = Car2.GetComponent<Controller>().direction2;		
		if(test.Length > 0)
		{
			if(test[0] == 'F')// && flag1 == 0)
			{
				flag1 = 1;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (BD, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (BD, 0.4f);
				}	
			}
			else if(test[0] == 'B')// && flag2 == 0)
			{
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (FD, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (FD, 0.4f);
				}
			}
			else if(test[0] == 'W' && flag1 == 1)
			{
				flag1 = 0;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (BU, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (BU, 0.4f);
				}
			}
			else if(test[0] == 'W' && flag2 == 1)
			{
				flag2 = 0;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (FU, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (FU, 0.4f);
				}
			}
			
			if(test[1] == 'L')// && flag3 == 0)
			{
				flag3 = 1;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (LD, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (LD, 0.4f);
				}
			}
			else if(test[1] == 'R')// && flag4 == 0)
			{
				flag4 = 1;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (RD, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (RD, 0.4f);
				}
			}
			else if(test[1] == 'W' && flag3 == 1)
			{
				flag3 = 0;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (LU, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (LU, 0.4f);
				}
			}
			else if(test[1] == 'W' && flag4 == 1)
			{
				flag4 = 0;
				if(useQueuedAnim)
				{
					animation.CrossFadeQueued (RU, 0.4f, QueueMode.PlayNow);
				}
				else{
					animation.CrossFade (RU, 0.4f);
				}
			}

		}
	
/////////////////////////////////////////////////	

		
		
		if(Input.GetKeyDown(KeyCode.W))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (BD, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (BD, 0.4f);
			}
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (FD, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (FD, 0.4f);
			}
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (LD, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (LD, 0.4f);
			}
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (RD, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (RD, 0.4f);
			}
		}

		
		if(Input.GetKeyUp(KeyCode.W))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (BU, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (BU, 0.4f);
			}
		}
		else if(Input.GetKeyUp(KeyCode.S))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (FU, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (FU, 0.4f);
			}
		}
		else if(Input.GetKeyUp(KeyCode.A))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (LU, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (LU, 0.4f);
			}
		}
		else if(Input.GetKeyUp(KeyCode.D))
		{
			if(useQueuedAnim)
			{
				animation.CrossFadeQueued (RU, 0.4f, QueueMode.PlayNow);
			}
			else{
				animation.CrossFade (RU, 0.4f);
			}
		}
	}
}
