using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float RunSpeed = 40f;
    private float horizontalMove = 0f;
    [HideInInspector]
    public bool CanMove = true;
    bool jump = false;
    bool crouch = false;

    [Header(" ")]
    [SerializeField]
    private CharacterController2D controller;
    [SerializeField]
    private Animator animator;


    /// <summary>
    /// Játékos karakter mozgását irányítja a "CharacterController2D" script segítségével.
    /// </summary>
    void Update () {
        if (CanMove)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * RunSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
                animator.SetBool("Jumping", true);
            }
            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
                animator.SetBool("IsCrouching", true);
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
                animator.SetBool("IsCrouching", false);
            }
        } else
        {
            horizontalMove = 0;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
