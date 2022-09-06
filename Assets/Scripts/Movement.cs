using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public MovementController controller;
    public Animator animator;

    float horizontalMove = 0f;
    public float runSpeed = 40f;

    bool jump = false;
    bool crouch = false;

    [HideInInspector] public bool isDashing;
    //public EnemyBehavior enemyBehavior;
    public ColliderHandler colliderHandler;
    bool canDash = true;
    float dashCoolDown = 1f;
    
    [SerializeField] float dashingTime = 0.01f;
    [SerializeField] float dashingPower = 5f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TrailRenderer trailRenderer;

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        WalkHandler();
        //if (!enemyBehavior.canDash)
        //{
        //    return;
        //}
        DashingControl();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Run();
    }

    void WalkHandler()
    {
        JumpControl();
        CrouchControl();
        //running animation control
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    void JumpControl()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

            animator.SetBool("IsJumping", true);
        }

        //OnLanding();
    }

    void CrouchControl()
    {
        OnCrouching(crouch);

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void Run()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    
    //falling animation handler
    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    //crouching animation control
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    IEnumerator Dash()
    {
        canDash = false;
        //enemyBehavior.canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        //enemyBehavior.canDash = true;
        canDash = true;
    }

    void DashingControl()
    {
        //if (enemyBehavior == null)
        //    return;
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash/*enemyBehavior.canDash == true*/)
        {
            StartCoroutine(Dash());
        }
    }
}
