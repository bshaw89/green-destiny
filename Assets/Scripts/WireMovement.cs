using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    public Vector2 velocity;
    public Vector2 moveInput;

	public GameObject player;
	public Rigidbody2D playerRB;
	public float speed;

	public float minimum;
	public float maximum;
	static float t;
	static float t2;

	public bool isJumping;

	// public bool falling;
    public PlayerMovement playerMovement;
    public bool playerFalling;

	public SpringJoint2D wire;

	public float timer;
    void Start()
    {
        
    }

    void Update()
    {
        playerFalling = playerMovement.falling;
        moveInput.x = Input.GetAxisRaw("Horizontal");
		moveInput.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) 
		{
            bool maxReached = false;
			if (!playerFalling)
			{
				isJumping = true;
				bool jumpOver = false;
				int revolutions = 0;
				timer += Time.deltaTime;

				
				speed = Mathf.Lerp(maximum, minimum, t);
				t -= 1.5f * -Time.deltaTime;
				wire.frequency = speed;
                

			} else if (playerFalling)
			{
                // speed = Mathf.Lerp(minimum, maximum, t);
				// t += 0.5f * Time.deltaTime;
				wire.frequency = 0.6f;;
				
			}
  		}
  		else 
		{
			t = 0.0f;
			speed = minimum;
			wire.frequency = 0.2f;
  		}
    }
}
