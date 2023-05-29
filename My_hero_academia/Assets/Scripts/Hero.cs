using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool isGrounded = false;

    private Animator anim;

    public static Hero Instance { get; set; }

     private States State
    {
        get {return (States)anim.GetInteger("state");}
        set {anim.SetInteger("state", (int)value);}
    }

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
        
        if (transform.position.y < -15f){
            HeroDie();
        }

        if (transform.position.x >= 120f){
            HeroWin();
        }
        
             
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
        if (!isGrounded) State = States.jump;
    }

   
    public void GetDamage(){
        lives -=1;
        Debug.Log(lives);

        if (lives < 1){
            Debug.Log("DIE");
            HeroDie();
        }
    }

    // private void Respawn(){
    //     Instantiate(playerPrefab, respawnPoint.position, Quaternion.idemtity); 
    // }

    private void ReStart(){
        SceneManager.LoadScene("GameScene");
    }

    public void HeroDie(){
        Destroy(this.gameObject);
        Invoke("Die", 2f);
        //Hero.speed = 0;
        ReStart();
        // if(!deathScreen.actifeSelf){
        //     deathScreen.SetActivate(true);
        // }
    }

    public void HeroWin(){
        SceneManager.LoadScene("WinScence");
    }
}

public enum States
{
    idle,
    run,
    jump,
    fall
}
