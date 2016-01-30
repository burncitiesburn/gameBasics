using UnityEngine;
using System.Collections;

public class AutoTransparent : MonoBehaviour
{

	private Shader oldShader = null;
	private Color oldColr = Color.black;
	private float transparency = 0.3f;
	private const float targetTransparency = 0.3f;
	private const float fallOff = 4.0f;
	private Renderer rend;

	public void Transparent ()
	{
		transparency = targetTransparency;

		if (oldShader == null) {
			oldShader = rend.material.shader;
			oldColr = rend.material.color;
			rend.material.shader = Shader.Find ("Transparent/Diffuse");
		}
	}
	// Use this for initialization
	void Start ()
	{
		rend = gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transparency < 1.0f) {
			Color C = rend.material.color;
			C.a = transparency;
			rend.material.color = C;
		} else {
			rend.material.shader = oldShader;
			rend.material.color = oldColr;
			Destroy (this);
		}
		transparency += ((1.0f - targetTransparency) * Time.deltaTime) / fallOff;
	}
}
