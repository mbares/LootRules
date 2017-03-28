using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{  
    float timeStampDamage = 0;
    public float moveSpeed = 5;
    float jumpHeight = 8;    
    public Transform groundCheck;
    float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool grounded;
    public bool isDead=false;
    public bool canJump=true;
    public float HP=100f;
    Animator animator;
    
	void Start ()
    {        
        animator = GetComponent<Animator>();
	}  

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
	
	void Update () {       
        if(Controller.paused==true)
        {
            return;
        }
        animator.SetBool("walk", false);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        if(HP<=0)
        {
            isDead = true;
            gameObject.SetActive(false);
        }
        if (grounded)
        {
            animator.SetBool("jump", false);            
        }            
        
        if (this.tag == "player1")
        {
            if (Input.GetKeyDown(KeyCode.W) && grounded && canJump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                animator.SetBool("jump", true);
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (grounded)
                    animator.SetBool("walk", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (grounded)
                    animator.SetBool("walk", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }            
        }
        if (this.tag == "player2")
        {
            if (Input.GetKeyDown(KeyCode.Keypad5) && grounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
                animator.SetBool("jump", true);
            }
            if (Input.GetKey(KeyCode.Keypad3))
            {
                if (grounded)
                    animator.SetBool("walk", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (Input.GetKey(KeyCode.Keypad1))
            {
                if (grounded)
                    animator.SetBool("walk", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }            
        }
        
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "lava")
        {
            HP -= 5;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 4.5f);
        }
        if (other.tag == "spike" )
        {
            HP -= 5;
            if (GetComponent<Rigidbody2D>().position.x < 0)
                GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 2.5f);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 2.5f);
        }
        if (other.tag == "dragonFireball")
        {
            HP -= 5;         
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {        
        if (other.tag == "fireBreath" && timeStampDamage <= Time.time)
        {
            timeStampDamage = Time.time + 1f;
            HP -= 5;     
        }                   
    }
   
}
