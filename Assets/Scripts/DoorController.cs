using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
	public string doorType = "lock";
	public bool requireKey = true;
	public bool open = false;
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (open) {
			DoorAnimation ();
		}

	}

	void DoorAnimation ()
	{
		transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, 5, transform.position.z), 0.01f);
		transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, -15, transform.position.z), 0.01f);
	}
	void OnTriggerEnter (Collider c)
	{
	
		if (c.gameObject.tag == "Player") {
			InventoryController playerInventory = c.gameObject.GetComponent<InventoryController> ();
			if (requireKey && playerInventory.hasKey ()) {
				openDoor ();
				playerInventory.useKey ();
			}
		}
	}

	void openDoor ()
	{
		open = true;
	}

	void closeDoor ()
	{
		open = false;
	}
}
