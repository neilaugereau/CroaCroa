using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float dashForce = 10f;
    public float dashCooldown = 5f;
    public LayerMask groundLayer;

    public bool facingRight = true;

    private Rigidbody2D rb;
    private Collider2D collider2d;

    private float dashTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        dashTimer = Math.Max(dashTimer - Time.deltaTime, 0f);

        bool isGrounded = Physics2D.IsTouchingLayers(collider2d, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput >= 0.1f)
            rb.linearVelocityX = Math.Max(rb.linearVelocityX, horizontalInput * speed);
        else if(horizontalInput <= -0.1f)
            rb.linearVelocityX = Math.Min(rb.linearVelocityX, horizontalInput * speed);


        FlipCharacter(horizontalInput);

        if(Input.GetButtonDown("Jump") && isGrounded) {
            rb.linearVelocityY += jumpForce;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashTimer == 0f) {
            dashTimer = dashCooldown;
            if(facingRight)
                rb.linearVelocityX += dashForce;
            else
                rb.linearVelocityX -= dashForce;
        }
    }

    private void FlipCharacter(float horizontalInput)
    {
        // Flip sprite based on movement direction
        if(horizontalInput > 0) {
            facingRight = true;
            Flip();
        }
        else if(horizontalInput < 0) {
            facingRight = false;
            Flip();
        }

    }

    void Flip()
    {
        // Flip the sprite horizontally
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? 1 : -1;
        transform.localScale = scale;
    }
   
}
