using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
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

    private States State
    {
        get {return (States)anim.GetInteger("state");}
        set {anim.SetInteger("state", (int)value);}
    }
    
    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update(){
        // CheckGround();

        if (isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal"))
            Run();
        
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
             
    }

    private void Run()
    {   
        if (isGrounded) State = States.run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        
        sprite.flipX = dir.x < 0.1f;
    }


    private void Jump()
    {   
        
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {   
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1);
        isGrounded = collider.Length > 1;
        // Debug.Log("colider.Length = " + collider.Length);
        if (!isGrounded) State = States.jump;
    }

    public static Hero Instance {get; set;}
    

    public void GetDamage(){
        lives -=1;
        Debug.Log(lives);

        if (lives < 1){
            Debug.Log("DIE");
            //Die();
        }
    }
}

public enum States
{
    idle,
    run,
    jump,
    fall
}
