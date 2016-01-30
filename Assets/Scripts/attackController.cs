using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class attackController : MonoBehaviour
{
	public GameObject bullet_prefab;
	public float bulletSpeed = 100f;
	public string[] weaponType = {"melee","ranged"};
	public float attackTimer = 0.0f;
	public float cooldown = 1.0f;
	public int selectedWeapon = 0;
	List<GameObject> nearEnemy = new List<GameObject> ();
	public string enemyTag;

	// Use this for initialization
	void Start ()
	{

	}
	void onGUI ()
	{
		if (tag == "Player") {
			GUI.Box (new Rect (10, 10, 0, 20), attackTimer + "/");
		}
	}
	// Update is called once per frame
	void Update ()
	{
		attackTimer = Mathf.Clamp (attackTimer, 0.0f, 1.0f);
		Vector3 playerPos = transform.position;
		if (attackTimer > 0) {
			attackTimer -= Time.deltaTime;
		}
		if (attackTimer <= 0) {
           
			if (Input.GetButtonDown ("Fire1")) {

				if (weaponType [selectedWeapon] == "ranged") {
					rangedAttack ();
				} else if (weaponType [selectedWeapon] == "melee") {
					meleeAttack (playerPos);
				}
				attackTimer = cooldown;
			}

			if (tag == "Enemy") {
				meleeAttack (playerPos);
				attackTimer = cooldown;
			}
           
		}
	}
	public void switchWeapon ()
	{
		if (selectedWeapon > weaponType.Length - 1) {
			selectedWeapon = 0;
		} else {
			selectedWeapon++;
		}
	}
	void rangedAttack ()
	{
		GameObject bullet = (GameObject)Instantiate (bullet_prefab, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
		bullet.GetComponent<Rigidbody> ().AddForce (Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
	}
	void meleeAttack (Vector3 playerPos)
	{
		Debug.Log ("Melee Attack");
        
		for (int i = 0; i < nearEnemy.Count; i++) {
			Vector3 enemyPos = nearEnemy [i].transform.position;
			Vector3 enemyDirection = enemyPos - playerPos;
			if (Vector3.Dot (enemyDirection, transform.forward) < 1.5) {
				Debug.Log ("Hit Enemy & Applied Damage");
				nearEnemy [i].GetComponent<HealthController> ().AdjustCurrentHealth (-10);
			}
		}
	}
	void OnTriggerEnter (Collider col)
	{

		if (col.gameObject.tag == enemyTag) {
			nearEnemy.Add (col.gameObject);
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == enemyTag) {
			nearEnemy.Remove (col.gameObject);
		}
	}
}