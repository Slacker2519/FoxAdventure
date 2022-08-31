using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
	[SerializeField] float m_JumpForce = 400f;									// Amount of force added when the player jumps.
	[Range(0, 1)][SerializeField] float m_CrouchSpeed = .36f;					// Amount of maxSpeed applied to crouching movement. 1 = 100%
	//[Range(0, .3f)][SerializeField] private float m_MovementSmoothing = 0f;   // How much to smooth out the movement
	[SerializeField] bool m_AirControl = false;									// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask m_WhatIsGround;									// A mask determining what is ground to the character
	[SerializeField] Transform m_GroundCheck;									// A position marking where to check if the player is grounded.
	[SerializeField] Transform m_CeilingCheck;									// A position marking where to check for ceilings
	[SerializeField] Collider2D m_CrouchDisableCollider;						// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f;  // Radius of the overlap circle to determine if the player can stand up
	Rigidbody2D m_Rigidbody2D;
	bool m_FacingRight = true;  // For determining which way the player is currently facing.
	Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	bool m_wasCrouching = false;

	void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        EventsManager();
    }

    void FixedUpdate()
    {
        bool wasGrounded = GroundCheck();
        GroundedEvent(wasGrounded);
    }

    void EventsManager()
    {
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    bool GroundCheck()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        return wasGrounded;
    }

    void GroundedEvent(bool wasGrounded)
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

	public void Move(float move, bool crouch, bool jump)
    {
        crouch = CeilingCheck(crouch);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            move = CrouchingEventHandler(move, crouch);
            ApplyVelocityAndSmoothness(move);

            FlipHandler(move);
        }

        VerticalForce(jump);
    }
    
    bool CeilingCheck(bool crouch)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            crouch = CancelStandup(crouch);
        }

        return crouch;
    }
    
    bool CancelStandup(bool crouch)
    {
        // If the character has a ceiling preventing them from standing up, keep them crouching
        if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
        {
            crouch = true;
        }

        return crouch;
    }
    
    float CrouchingEventHandler(float move, bool crouch)
    {
        // If crouching
        if (crouch)
        {
            CallCrouchingEvent();
            move = CrouchingEvent(move);
        }
        else
        {
            CancelCrouchingEvent();
        }

        return move;
    }
    
    void CallCrouchingEvent()
    {
        if (!m_wasCrouching)
        {
            m_wasCrouching = true;
            OnCrouchEvent.Invoke(true);
        }
    }
    
    float CrouchingEvent(float move)
    {
        // Reduce the speed by the crouchSpeed multiplier
        move *= m_CrouchSpeed;

        // Disable one of the colliders when crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = false;
        return move;
    }
    
    void CancelCrouchingEvent()
    {
        // Enable the collider when not crouching
        if (m_CrouchDisableCollider != null)
            m_CrouchDisableCollider.enabled = true;

        if (m_wasCrouching)
        {
            m_wasCrouching = false;
            OnCrouchEvent.Invoke(false);
        }
    }
    
    void ApplyVelocityAndSmoothness(float move)
    {
        //Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        //And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = targetVelocity;//Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    void VerticalForce(bool jump)
    {
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    void FlipHandler(float move)
    {
        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}