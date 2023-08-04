using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 10;
    private float jumpingPower = 16;
    private bool isFacingRight = true;

    public LogicScript logic;
    public GroundScript ground;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundScript>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded() && ground.playerIsAlive)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        Flip();

        if (!ground.playerIsAlive)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if (ground.playerIsAlive)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0 && ground.playerIsAlive || !isFacingRight && horizontal > 0 && ground.playerIsAlive)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.layer == 3)
        {
            Destroy(collision.gameObject);
            Debug.Log("Enemy Killed");
            logic.addScore(1);
        }
    }
}
