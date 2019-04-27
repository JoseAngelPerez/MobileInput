using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float jumpForce;

	[SerializeField] private GameObject ball;
	[SerializeField] private Transform hand;

	private float jumpIncrease = .1f;
	private Vector2 touchInitialPos;

    private PlayerMovement playerMovement;
	private Animator anim;
    private Rigidbody rb;

	private bool isAlive;

	public bool IsAlive
	{
		get { return isAlive; }
	}

    private void Awake()
    {
		isAlive = true;
        playerMovement = GetComponent<PlayerMovement>();
		anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
	{
		if (isAlive)
		{
			SetPlayerInteraction();
			anim.SetBool("isJumping", !playerMovement.IsGrounded);
		}
		else
		{
			anim.SetBool("isDead", !isAlive);
		}
	}

	private void SetPlayerInteraction()
	{
		if (playerMovement.IsGrounded)
		{
			playerMovement.Move(movementSpeed);
			playerMovement.Rotate(rotationSpeed);
		}

		if (Input.touches.Length > 0)
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began)
			{
				jumpIncrease = 0.1f;
				touchInitialPos = touch.position;
			}
			if (touch.phase == TouchPhase.Stationary)
			{
				jumpIncrease += Time.deltaTime;
				Mathf.Clamp(jumpIncrease, 0, 2);
			}
			if (touch.phase == TouchPhase.Moved && touch.position.y > touchInitialPos.y)
			{
				anim.SetLayerWeight(anim.GetLayerIndex("Throw"), 100);
				anim.SetBool("Throwing", true);
			}
			if (touch.phase == TouchPhase.Ended)
			{
				if (touchInitialPos.y == touch.position.y)
				{
					playerMovement.Jump(rb, jumpForce * jumpIncrease);
				}
				jumpIncrease = 0;
			}
		}
	}

	public void Throw()
	{
		GameObject ballRef = Instantiate(ball, hand.position, Quaternion.identity);
		Rigidbody rbRef = ballRef.AddComponent<Rigidbody>();
		rbRef.AddTorque(ballRef.transform.right * 100);
		rbRef.AddForce(transform.forward * 1000);
		anim.SetLayerWeight(anim.GetLayerIndex("Throw"), 0);
		anim.SetBool("Throwing", false);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Obstacle")
		{
			isAlive = false;
		}
	}
}