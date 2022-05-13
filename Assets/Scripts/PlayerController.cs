using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public float speed = 10.0F;
	public float gravity = 20.0F;

	public float horizontalSpeed = 2.0F;
	public float verticalSpeed = 2.0F;

	public KeyBehavior keyTotal;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;

	void Start()
	{
		controller = GetComponent<CharacterController>();
		Cursor.visible = false;
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
        {
			speed = 30.0F;
        }
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			speed = 15.0F;
		}

		if (controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
		}

		float h = horizontalSpeed * Input.GetAxis("Mouse X");
		float v = verticalSpeed * Input.GetAxis("Mouse Y");

		// Apply gravity manually.
		moveDirection.y -= gravity * Time.deltaTime;
		// Move Character Controller
		controller.Move(moveDirection * Time.deltaTime);
		controller.transform.Rotate(0, h, 0);
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Key")
		{
			KeyBehavior.AddScore();
			Destroy(hit.gameObject);

			if (KeyBehavior.GetScore() == 3)
			{
				Cursor.visible = true;
				SceneManager.LoadScene("WinScene");
			}

		}
	}
}
