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

    // Update is called once per frame
    void Update()
    {
        WalkHandler();
    }

    void FixedUpdate()
    {
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
}
