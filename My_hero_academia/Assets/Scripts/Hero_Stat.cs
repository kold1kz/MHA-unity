using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hero_stat : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool isGrounded = false;

    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {
        Console.WriteLine(isGrounded);
        CheckGround();
    }

    private void Update(){
        if (isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();
        
        if (Input.GetButtonDown("Jump"))
            Jump();
            // isGrounded && 
    }

    private void Run()
    {   
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        
        sprite.flipX = dir.x < 0.0f;
    }


    private void Jump()
    {   
        
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length >1;
        if (!isGrounded) State = States.jump;
    }

    public enum States{
        idle,
        run,
        jump,
        fall
    }

    private States State{
        get {return (States)anim.GetInteger("state");}
        set {anim.SetInteger("state", (int)value);}
    }
}
