using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;
    public Vector2 velocity;

	public GameObject player;
	public Rigidbody2D playerRB;
	public float speed;

	public float minimum;
	public float maximum;
	static float t;
	static float t2;

	public bool isJumping;

	public bool falling;

	public SpringJoint2D wire;

	public float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) 
		{
			if (playerRB.velocity.y > 0.01)
			{
				isJumping = true;
				bool jumpOver = false;
				int revolutions = 0;
				timer += Time.deltaTime;

				// float timeElapsed = 0;
				speed = 0.48f;
				speed -= 0.2f * Time.deltaTime; // Cap at some max value too
				// maximum = 0.5f;
				// minimum = 0.1f;
				speed = Mathf.Lerp(maximum, minimum, t);
				t -= 2.5f * -Time.deltaTime;
				wire.frequency = speed;

				maximum = 0.7f;
				minimum = 0.3f;
				speed = Mathf.Lerp(maximum, minimum, t);
				t -= 2.5f * -Time.deltaTime;
				wire.frequency = speed;

			} else if (playerRB.velocity.y < 1)
			// Debug.Log("GRAV" + playerRB.gravityScale);
			{
				// wire.frequency = 0.7f;
				// wire.distance = 0.005f;
				// Debug.Log("GRAVY");
				// if (!falling)
				// {
				// 	wire.frequency = 0.6f;
				// 	falling = true;
				// }
				// if (falling)
				// {
				// wire.frequency -= 0.1f * -Time.deltaTime;
				// 	if (wire.frequency <= 0.3f)
				// 	{
				// 		falling = false;
				// 	}
				// }
				// jumpOver = true;
				
			}

			// if (falling)
			// {
			// 	// speed -= 0.001f * -Time.deltaTime;
			// 	wire.frequency = Mathf.Lerp(speed, 0.3f, t2);
			// 	if (wire.frequency == 0.3f)
			// 		{
			// 			falling = false;
			// 			timer = 0;
			// 		}
			// }

			// if (falling)
			// {
			// 	timer += Time.deltaTime;
			// 	Debug.Log(timer);
			// 	if (timer >= 2f)
			// 	{
			// 		Debug.Log("SPEED" + speed);
			// 		speed -= 0.001f * -Time.deltaTime;
			// 		wire.frequency = Mathf.Min(speed, 0.3f);
			// 		if (wire.frequency == 0.3f)
			// 		{
			// 			falling = false;
			// 			timer = 0;
			// 		}
			// 	}
			// }
			// if (jumpOver)
			// {
			// 	wire.frequency = 0.3f;
			// }


  		}
  		else 
		{
			// speed = 0.4f;
			t = 0.0f;
    		// speed -= 0.1f * -Time.deltaTime; // Cap at some min value too
			speed = minimum;
			// wire.frequency = Mathf.Min(speed, 0.2f);
			wire.frequency = 0.2f;
			// Debug.Log("speed" + speed);
			// wire.frequency -= 0.35f * Time.deltaTime;
			// isJumping = false;

  		}
    }
}
