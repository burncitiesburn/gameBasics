using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    float currentHealth = 100.0f;
    float maxHealth = 100.0f;
    float healthBarLength = 0.0f;
	// Use this for initialization
	void Start () {
        healthBarLength = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, healthBarLength, 20), currentHealth + "/" + maxHealth);
    }

    public void AdjustCurrentHealth(int adjustment){
        currentHealth += adjustment;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBarLength = (Screen.width / 2) * (currentHealth / maxHealth);
    }
}
