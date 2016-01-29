using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{

	public int keys = 0;
	public int potions = 0;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI ()
	{
		GUI.Box (new Rect (Screen.width / 2 - 100, Screen.height / 4 * 3, 100, 25), "Keys: " + keys);
		GUI.Box (new Rect (Screen.width / 2, Screen.height / 4 * 3, 100, 25), "Potions: " + potions);
	}



	public void pickupKey ()
	{
		keys++;
	}

	public void pickupPotion ()
	{
		potions++;
	}

	public void useKey ()
	{
		keys--;
	}

	public void usePotion ()
	{
		potions--;
	}
}
