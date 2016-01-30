using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{

	CharacterController cController;


	public float mSensitivity = 7.0f;
	public float mvSpeed = 5.0f;
	public float neckStop = 60.0f;
	public float gravity = 9.81f;
	public float jumpSpeed = 5.0f;
	public float crouchHeight = 0.5f;
	float vVelocity = 0;
	bool isCrouching = false;
	bool isSprinting = false;
	float pitch = 0;
	float stamina = 100.0f;
	Vector3 cameraRay;
	Vector3 mouseVector;

	// Use this for initialization
	void Start ()
	{


		cController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//look up down
		//aiming
		
		float rotSpeed = 1.0f;
		RaycastHit hit;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		//We need to user a layerMask to specify all layers except the player layer (8)
		int layerMask = 1 << 8; //set it to 8
		layerMask = ~layerMask; //Inverse it to get eveything else

		bool diditHit = Physics.Raycast (ray, out hit, 100f, layerMask);
		if (diditHit) {
			mouseVector = hit.point;
		}
				
		mouseVector.y = transform.position.y;
		GameObject.FindGameObjectWithTag ("Cursor").transform.position = mouseVector;
		transform.LookAt (mouseVector);

		/*
		//look left right
		pitch -= Input.GetAxis ("Mouse Y") * mSensitivity;
		pitch = Mathf.Clamp (pitch, -neckStop, neckStop);
		Camera.main.transform.localRotation = Quaternion.Euler (pitch, 0, 0);
        */
		//move forward/back/left/right
		float fSpeed = Input.GetAxis ("Vertical") * mvSpeed;
		float sSpeed = Input.GetAxis ("Horizontal") * mvSpeed;

		//Sprinting
		if (cController.isGrounded && Input.GetButton ("Sprint") && stamina > 0) {
			isSprinting = true;
		} 
		if (stamina <= 0) {
			isSprinting = false;
		}
		if (isSprinting == true) {
			fSpeed *= 1.1f;
			stamina = stamina - 20.0f * Time.deltaTime;
		} else {
			stamina = stamina + 10.0f * Time.deltaTime;
		}
		stamina = Mathf.Clamp (stamina, 0, 100);

		vVelocity += Physics.gravity.y * Time.deltaTime; // gravity

		//jumping
		if (cController.isGrounded && Input.GetButton ("Jump")) {
			vVelocity = jumpSpeed;
		}

		//crouching
		if (cController.isGrounded && Input.GetButtonDown ("Crouch")) {
			cController.height -= crouchHeight;
			cController.center -= new Vector3 (0, crouchHeight / 2, 0);
			Camera.main.transform.position -= new Vector3 (0, crouchHeight, 0);
			isCrouching = true;
		}
		if (cController.isGrounded && Input.GetButtonUp ("Crouch")) {
			cController.height += crouchHeight;
			cController.center += new Vector3 (0, crouchHeight / 2, 0);
			Camera.main.transform.position += new Vector3 (0, crouchHeight, 0);
			isCrouching = false;
		}

		if (isCrouching) {
			fSpeed *= 0.1f;
		}
		Vector3 speed = new Vector3 (sSpeed, vVelocity, fSpeed);
		cController.Move (speed * Time.deltaTime);

	}
}
