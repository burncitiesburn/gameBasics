using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float zPosMargin = 1f;
	public float zNegMargin = 1f;
	public float xMargin = 1f;
	public float speed = 1f;
	Transform player;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	bool moveX ()
	{
		return Mathf.Abs (transform.position.x - player.position.x) > xMargin;
	}
	
	
	bool moveZ ()
	{
		if (Mathf.Abs (transform.position.z - player.position.z) > zNegMargin) {
			return Mathf.Abs (transform.position.z - player.position.z) > zPosMargin;
		} else {
			return Mathf.Abs (transform.position.z - player.position.z) < zNegMargin;
		}

	}
	
	
	void FixedUpdate ()
	{
		Follow ();
		RaycastHit[] hits;

		hits = Physics.RaycastAll (transform.position, transform.forward, Vector3.Distance (transform.position, player.position));

		foreach (RaycastHit hit in hits) {
			Renderer R = hit.collider.GetComponent<Renderer> ();
			if (R == null) {
				continue;
			}
			AutoTransparent AT = R.GetComponent<AutoTransparent> ();
			if (AT == null) {
				AT = R.gameObject.AddComponent<AutoTransparent> ();
			}
			AT.Transparent ();
		}
	}

	void Follow ()
	{

		float targetX = transform.position.x;
		float targetZ = transform.position.z;
		

		if (moveX ()) {
			targetX = Mathf.Lerp (transform.position.x, player.position.x, speed * Time.deltaTime);
		}

		if (moveZ ()) {
			targetZ = Mathf.Lerp (transform.position.z, player.position.z - zPosMargin, speed * Time.deltaTime);
		}
		transform.position = new Vector3 (targetX, transform.position.y, targetZ);
	}
}
