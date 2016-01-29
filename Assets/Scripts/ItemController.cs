﻿using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour
{
	public float speed = 10.0f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (Vector3.forward, speed * Time.deltaTime);
	}

	void OnTriggerEnter (Collider c)
	{

		Debug.Log ("Collision");

		if (c.gameObject.tag == "Player" && tag == "Key") {
			c.gameObject.GetComponent<InventoryController> ().pickupKey ();
			Destroy (gameObject);
		}
	}
}