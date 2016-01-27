using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

	
	public float enemySightDistance = 10.0f;
	public float maxAttackDistance = 0.5f;
	public float rotationSpeed = 2.5f;
	public float mvSpeed = 1.0f;
	Transform myPosition;
	Transform target;
    Vector3 aimpoint;
	// Use this for initialization

	void Awake ()
	{
		myPosition = transform;
	}
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;  
	}
	
	// Update is called once per frame
	void Update ()
	{
        aimpoint = target.position;
        aimpoint.y = transform.position.y;
		watchForPlayer ();
	}

	public void watchForPlayer ()
	{
        
		Vector3 direction = (aimpoint - transform.position).normalized;
		float dotDirection = Vector3.Dot (direction, transform.forward);
		if (Vector3.Distance (aimpoint, myPosition.position) < enemySightDistance && dotDirection > 0) {
			playerDetected ();
		}
	}

	public void playerDetected ()
	{
		Debug.DrawLine (aimpoint, myPosition.position, Color.red);

		myPosition.rotation = Quaternion.Slerp (myPosition.rotation, Quaternion.LookRotation (aimpoint - myPosition.position), rotationSpeed * Time.deltaTime);
		if (Vector3.Distance (aimpoint, myPosition.position) > maxAttackDistance) {
			myPosition.position += myPosition.forward * mvSpeed * Time.deltaTime;
		}

	}
}
