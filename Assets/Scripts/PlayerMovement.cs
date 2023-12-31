using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region INPUT PARAMS
    private Vector2 moveInput;
	public float LastPressedJumpTime { get; private set; }
	#endregion

    float DescentTime;
    float ascentTime;
    public float gravityScale;
    [SerializeField] [Range(0, 1)] private float tiltSpeed;
    [SerializeField] private float maxTilt;
    public LineRenderer lineRenderer;
    public AudioManager audioManager;


    public float distanceFromGround;

    #region COMPONENTS
    public Rigidbody2D RB;
	public Animator animHandler;
	public GameObject Wire;
    public Rigidbody2D WireRB;
    public SpringJoint2D WireSpring;
    public Vector2 previousPos;
    public bool falling;
    public SpriteRenderer spriteRend;
    

	#endregion


    #region STATE PARAMS
	public bool IsFacingRight { get; private set; }
	public bool IsJumping { get; private set; }
	public bool IsWallJumping { get; private set; }
	public bool IsSliding { get; private set; }
    
    public float LastOnGroundTime { get; private set; }
	public float LastOnWallTime { get; private set; }
	public float LastOnWallRightTime { get; private set; }
	public float LastOnWallLeftTime { get; private set; }

    //Jump
	private bool _isJumpCut;
	public bool _isJumpFalling;
	#endregion

	[Header("Run")]
    public float runMaxSpeed;
	public float runAcceleration;
    [HideInInspector] public float runAccelAmount;
    public float runDecceleration;
    [HideInInspector] public float runDeccelAmount;
    [Range(0f, 1)] public float accelInAir;

	[Range(0f, 1)] public float deccelInAir;

    [Header("Assists")]
    [Range(0.01f, 0.5f)] public float coyoteTime;
	[Range(0.01f, 0.5f)] public float jumpInputBufferTime;


    #region CHECK PARAMS
    [Header("Checks")] 
	[SerializeField] private Transform _groundCheckPoint;
	//Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
	[SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
	[Space(5)]
	[SerializeField] private Transform _frontWallCheckPoint;
	[SerializeField] private Transform _backWallCheckPoint;
	[SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    #region LAYERS & TAGS
    [Header("Layers & Tags")]
	[SerializeField] private LayerMask _groundLayer;
	#endregion

    void Start()
    {
        runAccelAmount = (50 * runAcceleration) / runMaxSpeed;
		runDeccelAmount = (50 * runDecceleration) / runMaxSpeed;
		IsFacingRight = true;

        // runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		// runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
        // hi
    }

    void Update()
    {
        animHandler.SetFloat("Speed", Mathf.Abs(RB.velocity.x));

        if (RB.transform.position.y + 0.01 < previousPos.y)
        {
            falling = true;
        }

        if (RB.transform.position.y < 0.01f)
        {
            audioManager.Play("Jump1");
        }
        

        LastOnGroundTime -= Time.deltaTime;
		LastPressedJumpTime -= Time.deltaTime;
        moveInput.x = Input.GetAxisRaw("Horizontal");
		moveInput.y = Input.GetAxisRaw("Vertical");

        


        if(Input.GetKeyDown(KeyCode.Space))
        {
            gravityScale = 3.4f;
			OnJumpInput();	
        }

		#region COLLISION CHECKS
        if (!IsJumping)
        {
            //Ground Check
			if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer)) //checks if set box overlaps with ground
			{
                

				if(LastOnGroundTime < -0.1f && !falling)
                {
                    audioManager.Stop("Jump1");
					// AnimHandler.justLanded = true;
					// audioManager.Stop("PlayerJump");

                    WireSpring.enabled = false;
					_isJumpFalling = false;

					WireMovement();
                }
                RB.gravityScale = 1.7f;
                falling = false;

				LastOnGroundTime = coyoteTime; //if so sets the lastGrounded to coyoteTime

            }		
        }
		#endregion

        #region JUMP CHECKS
		if (IsJumping && RB.velocity.y < 0)
		{
			IsJumping = false;

			_isJumpFalling = true;
		}

        if (RB.velocity.y < 0)
        {
            RB.gravityScale = 3.4f;
        }

        if (CanJump() && LastPressedJumpTime > 0)
        {
            IsJumping = true;
            IsWallJumping = false;
            _isJumpCut = false;
            _isJumpFalling = false;

            // Jump();

            // AnimHandler.startedJumping = true;
        }
		#endregion
        
        previousPos = RB.transform.position;
    }

    void FixedUpdate ()
    {
        Run(1);
		WireMovement();
        // RB.gravityScale = gravityScale;

       
        

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.collider != null)
        {
            float distanceFromGround = Mathf.Abs(hit.point.y - transform.position.y);
        }
         lineRenderer.SetPosition(0, RB.position);
        lineRenderer.SetPosition(1, WireRB.position);
    }

    void LateUpdate()
    {
      if (moveInput.x < 0)
      {
        IsFacingRight = false;
      }
      else if (moveInput.x > 0)
      {
        IsFacingRight = true;
      }

      if (!IsFacingRight)
      {
        spriteRend.flipX = true;
      }
      else if (IsFacingRight)
      {
        spriteRend.flipX = false;
      }
       if (moveInput.x > -0.01f)
        {
            audioManager.Play("Footsteps");
        }

        if (moveInput.x < 0.01f || IsJumping)
        {
            audioManager.Stop("Footsteps");
        }
    }

    public void OnJumpInput()
	{
		LastPressedJumpTime = jumpInputBufferTime;
	}

    private void Run(float lerpAmount)
    {
		float targetSpeed = moveInput.x * runMaxSpeed;
		targetSpeed = Mathf.Lerp(RB.velocity.x, targetSpeed, lerpAmount);

		float accelRate;


        if (LastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount : runDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? runAccelAmount * accelInAir : runDeccelAmount * deccelInAir;


		float speedDif = targetSpeed - RB.velocity.x;

		float movement = speedDif * accelRate;
        Vector2 groundForce = new Vector2(1, 0);

        if (LastOnGroundTime > -0.01f)
        {
		    RB.AddForce(movement * groundForce, ForceMode2D.Force);
        }
        else
        {
		    RB.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

    }

    private void WireMovement()
    {
        if (IsJumping || _isJumpFalling || falling)
		{
            WireSpring.enabled = true;
			WireRB.MovePosition(new Vector2(RB.position.x, WireRB.position.y));
            DescentTime = WireRB.position.y - (1f * Time.deltaTime);
            WireRB.MovePosition(new Vector2(RB.position.x, DescentTime));

            if (RB.position.y > WireRB.position.y -2f)
            {
                WireRB.MovePosition(new Vector2(RB.position.x, RB.position.y + 2f));
            }
			

			if (falling)
			{		
                if (moveInput.y > 0)
                {
                    ascentTime = WireRB.position.y + (2f * Time.deltaTime);
                    WireRB.MovePosition(new Vector2(RB.position.x, ascentTime));
                }
                else if (moveInput.y < 0)
                {
                    DescentTime = WireRB.position.y - (3f * Time.deltaTime);
					WireRB.MovePosition(new Vector2(RB.position.x, DescentTime));
                }
                else
                {

					DescentTime = WireRB.position.y - (1f * Time.deltaTime);
					WireRB.MovePosition(new Vector2(RB.position.x, DescentTime));
                }
			}

			if (_isJumpFalling && Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer) && !Input.GetKeyUp(KeyCode.Space))
			{
				IsJumping = false;
				_isJumpFalling = false;
				WireSpring.enabled = false;
				WireRB.MovePosition(new Vector2(RB.position.x, RB.position.y + 4.5f));
			}


		}
		// else if (moveInput.y > 0)
        // {
        //     WireSpring.enabled = true;
        //     ascentTime = WireRB.position.y + (2f * Time.deltaTime);
        //     WireRB.MovePosition(new Vector2(RB.position.x, ascentTime));
        // }
        else
		{
			WireRB.MovePosition(new Vector2(RB.position.x, RB.position.y + 4.5f));
		}

    }

    private bool CanJump()
    {
		return LastOnGroundTime > 0 && !IsJumping;
    }

    private void OnValidate()
    {
        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);
		runDecceleration = Mathf.Clamp(runDecceleration, 0.01f, runMaxSpeed);
    }
}
