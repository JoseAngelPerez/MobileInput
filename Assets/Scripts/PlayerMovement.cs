using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private float distanceToGround;
	private bool isGrounded = true;

	private Collider collider;
	
    public bool IsGrounded
    {
        get { return isGrounded; }
    }

    private void Awake()
	{
		collider = GetComponent<Collider>();
		distanceToGround = collider.bounds.extents.y;
    }

    private void Update()
    {
        isGrounded = CheckIsGrounded();
    }

    public void Move(float speed)
    {
		transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void Jump(Rigidbody rb, float jumpForce)
    {
        rb.AddForce(transform.up * jumpForce);
		rb.AddForce(transform.forward * jumpForce * 2);
    }

    public void Rotate(float rotSpeed)
    {
		transform.eulerAngles += new Vector3(0, 1, 0) * Input.acceleration.x * rotSpeed * Time.deltaTime;
	}

	private bool CheckIsGrounded()
	{
		return Physics.Raycast(collider.bounds.center, -transform.up, distanceToGround + 0.1f);
	}
}
