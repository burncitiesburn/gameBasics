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

	// Use this for initialization
	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{

		//look up down
		float yaw = Input.GetAxis ("Mouse X") * mSensitivity;
		transform.Rotate (0, yaw, 0);


		//look left right
		pitch -= Input.GetAxis ("Mouse Y") * mSensitivity;
		pitch = Mathf.Clamp (pitch, -neckStop, neckStop);
		Camera.main.transform.localRotation = Quaternion.Euler (pitch, 0, 0);

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

		speed = transform.rotation * speed;
		cController.Move (speed * Time.deltaTime);

	}
}
