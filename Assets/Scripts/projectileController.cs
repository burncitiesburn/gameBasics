using UnityEngine;
using System.Collections;

public class projectileController : MonoBehaviour
{

	float lifespan = 3.0f;
	float targetAlpha = 1.0f;
	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
		lifespan -= Time.deltaTime;

		if (lifespan <= 0) {
			Disappear ();
		}
	}

	void OnCollisionEnter (Collision c)
	{
		
		c.gameObject.GetComponent<HealthController> ().AdjustCurrentHealth (-10);  

	}



	void Disappear ()
	{
		float fadeTime = 0.0f;

		Color c = gameObject.GetComponent<Renderer> ().material.color;
		float currentAlpha = c.a;
		int count = 0;
		while (fadeTime <= 1) {
			c.a = Mathf.Lerp (currentAlpha, targetAlpha, fadeTime);
			gameObject.GetComponent<Renderer> ().material.color = c;
			fadeTime += Time.deltaTime / lifespan;
			Debug.Log (fadeTime);
			count++;
			if (count == 10) {
				break;
			}
		}
		c.a = targetAlpha;
		gameObject.GetComponent<Renderer> ().material.color = c;
		Destroy (gameObject);
	}
}
