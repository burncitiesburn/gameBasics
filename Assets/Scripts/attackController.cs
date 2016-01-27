using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class attackController : MonoBehaviour
{
    public GameObject bullet_prefab;
    public float bulletSpeed = 100f;
    public string weaponType = "ranged";
    List<GameObject> nearEnemy = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") )
        {
            if (weaponType == "ranged")
            {

                GameObject bullet = (GameObject)Instantiate(bullet_prefab, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * bulletSpeed, ForceMode.Impulse);
            }
            else if (weaponType == "melee")
            {
                Debug.Log("Melee Attack");
                Vector3 playerPos = transform.position;
                for (int i = 0; i < nearEnemy.Count; i++)
                {
                    Vector3 enemyPos = nearEnemy[i].transform.position;
                    Vector3 enemyDirection = enemyPos - playerPos;
                    if(Vector3.Dot(enemyDirection, transform.forward) < 1.5)
                    {
                        Debug.Log("Hit Enemy & Applied Damage");
                        nearEnemy[i].GetComponent<EnemyController>().AdjustCurrentHealth(-10);    
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            nearEnemy.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            nearEnemy.Remove(col.gameObject);
        }
    }
}