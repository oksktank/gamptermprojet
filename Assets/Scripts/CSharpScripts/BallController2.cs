﻿using UnityEngine;
using System.Collections;

public class BallController2 : MonoBehaviour {
	GameObject cart;
	GameObject ball;
	Vector3 pos;
	
	// Use this for initialization
	void Start () {
		cart = GameObject.Find ("pig_cart_2p");
		ball = GameObject.Find ("2p_point");
		pos = ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		ball.transform.position = new Vector3(cart.transform.position.x, pos.y , cart.transform.position.z);
	}
}
