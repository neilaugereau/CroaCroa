using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D collider2d;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
    }

    private void Update()
    {
        bool isGrounded = Physics2D.IsTouchingLayers(collider2d, groundLayer);

        float horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        FlipCharacter(horizontalInput);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocityY += jumpForce;
        }
    }

    private void FlipCharacter(float horizontalInput)
    {
        // Flip sprite based on movement direction
        if (horizontalInput > 0)
            Flip(true);
        else if (horizontalInput < 0)
            Flip(false);

    }

    void Flip(bool facingRight)
    {
        // Flip the sprite horizontally
        Vector3 scale = transform.localScale;
        scale.x = facingRight ? 1 : -1;
        transform.localScale = scale;
    }
   
}
