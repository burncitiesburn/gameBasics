using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour
{
	public GameObject bullet_prefab;
	public float bulletSpeed = 100f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			GameObject bullet = (GameObject)Instantiate (bullet_prefab, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
			bullet.GetComponent<Rigidbody> ().AddForce (Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
		}
	}
}
