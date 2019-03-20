using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Jelenleg nem használt script.
/// </summary>
public class PlayerController : MonoBehaviour {

    public float Speed;
    public float JumpForce;
    private float MoveInput;

    private Rigidbody2D rb;

    private bool FacingRight = true;

    private bool IsGrounded;
    public Transform GroundCheck;
    public float CheckRadius;
    public LayerMask WhatIsGround;

    private int extraJump;
    public int ExtraJumpNumbers;


    void Start()
    {
        extraJump = ExtraJumpNumbers;
        rb = GetComponent<Rigidbody2D>();
    }

     void FixedUpdate()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, CheckRadius, WhatIsGround);


        MoveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(MoveInput * Speed, rb.velocity.y);

        if (FacingRight == false && MoveInput > 0)
        {
            Flip();
        }
        else if (FacingRight == true && MoveInput < 0)
        {
            Flip();
        }
    }

    void Update()
    {
        if (IsGrounded)
        {
            extraJump = ExtraJumpNumbers;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.velocity = Vector2.up * JumpForce;
            extraJump--;
        } else if (Input.GetKeyDown(KeyCode.Space) && extraJump == 0 && IsGrounded == true)
        {
            rb.velocity = Vector2.up * JumpForce;
        }
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
