using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{

	float currentHealth = 100.0f;
	float maxHealth = 100.0f;
	float healthBarLength = 0.0f;
	public float enemySightDistance = 10.0f;
	public float maxAttackDistance = 2.5f;
	public float rotationSpeed = 2.5f;
	public float mvSpeed = 1.0f;
	Transform myPosition;
	Transform target;
	// Use this for initialization

	void Awake ()
	{
		myPosition = transform;
	}
	void Start ()
	{
		healthBarLength = Screen.width / 2;
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update ()
	{
		watchForPlayer ();
	}

	void OnGUI ()
	{
		GUI.Box (new Rect (10, 10, healthBarLength, 20), currentHealth + "/" + maxHealth);
	}

	public void AdjustCurrentHealth (int adjustment)
	{
		currentHealth += adjustment;
		currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth);
		healthBarLength = (Screen.width / 2) * (currentHealth / maxHealth);
	}

	public void watchForPlayer ()
	{
		Vector3 direction = (target.transform.position - transform.position).normalized;
		float dotDirection = Vector3.Dot (direction, transform.forward);
		Debug.Log ("distance: " + Vector3.Distance (target.position, myPosition.position));
		if (Vector3.Distance (target.position, myPosition.position) < enemySightDistance) {
			playerDetected ();
		}
	}

	public void playerDetected ()
	{
		Debug.DrawLine (target.position, myPosition.position, Color.red);

		myPosition.rotation = Quaternion.Slerp (myPosition.rotation, Quaternion.LookRotation (target.position - myPosition.position), rotationSpeed * Time.deltaTime);
		if (Vector3.Distance (target.position, myPosition.position) > maxAttackDistance) {
			myPosition.position += myPosition.forward * mvSpeed * Time.deltaTime;
		}

	}
}
