using UnityEngine;
using System.Collections;

public class Cowboy : MonoBehaviour {

    float attackCooldown = 2f;
    float timeStampAttack = 0;
    float timeStampHayball = 0;
    float timeStampJumpshot = 0;
    float hayballCooldown = 10f;
    float jumpshotCooldown = 7f;
    bool canAttack = true;
    public float speed = 15f;
    public Rigidbody2D bullet;
    public GameObject attack;
    Animator animator;
    [SerializeField]
    AudioClip jumpshotSound;

	
	void Start () 
    {
        animator = GetComponent<Animator>();
	}
	
	
	void Update () 
    {
        if (Controller.paused == true)
        {
            return;
        }
        CowboyAbilities();
	}

    void CowboyAbilities()
    {
        if (this.tag == "player1")
        {
            if (Input.GetKeyDown(KeyCode.Space) && timeStampAttack <= Time.time && canAttack)
            {
                animator.SetTrigger("attack");
                timeStampAttack = Time.time + attackCooldown;
                Fire();
                Invoke("Fire", 0.3f);
            }
            if (Input.GetKeyDown(KeyCode.E) && timeStampJumpshot <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cdE").GetComponent<Cooldown>().cooldownTime = jumpshotCooldown;
                GameObject.FindGameObjectWithTag("cdE").GetComponent<Cooldown>().coolingDown = true;
                Jumpshot();    
            }
            if (Input.GetKeyDown(KeyCode.Q) && timeStampHayball <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cdQ").GetComponent<Cooldown>().cooldownTime = hayballCooldown;
                GameObject.FindGameObjectWithTag("cdQ").GetComponent<Cooldown>().coolingDown = true;
                Hayball();
            }
        }
        if (this.tag == "player2")
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && timeStampAttack <= Time.time)
            {
                animator.SetTrigger("attack");
                timeStampAttack = Time.time + attackCooldown;
                Fire();
                Invoke("Fire", 0.3f);
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) && timeStampJumpshot <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cd6").GetComponent<Cooldown>().cooldownTime = jumpshotCooldown;
                GameObject.FindGameObjectWithTag("cd6").GetComponent<Cooldown>().coolingDown = true;
                timeStampJumpshot = Time.time + jumpshotCooldown;
                Jumpshot();
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) && timeStampHayball <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cd4").GetComponent<Cooldown>().cooldownTime = hayballCooldown;
                GameObject.FindGameObjectWithTag("cd4").GetComponent<Cooldown>().coolingDown = true;
                Hayball();
            }
        }
    }
    void Fire()
    {
        Rigidbody2D bulletInstance = Instantiate(bullet, attack.transform.position, attack.transform.rotation) as Rigidbody2D;
        this.GetComponent<AudioSource>().Play();
        if (attack.transform.rotation == Quaternion.Euler(0, 0, 0))
            bulletInstance.velocity = new Vector2(speed, 0);
        else
            bulletInstance.velocity = new Vector2(-speed, 0);
    }
    void Jumpshot()
    {
        this.GetComponent<AudioSource>().PlayOneShot(jumpshotSound);
        timeStampJumpshot = Time.time + jumpshotCooldown;
        animator.SetTrigger("jumpshot");
        Vector2 leftBullet = new Vector2(this.transform.position.x - 1f, this.transform.position.y);
        Vector2 rightBullet = new Vector2(this.transform.position.x + 1f, this.transform.position.y);
        Rigidbody2D bulletInstance = Instantiate(bullet, rightBullet, Quaternion.identity) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(speed, 0);
        Rigidbody2D bulletInstance2 = Instantiate(bullet, leftBullet, Quaternion.identity) as Rigidbody2D;
        bulletInstance2.velocity = new Vector2(-speed, 0);
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10);        
    }
    void HayballDown()
    {
        GetComponent<Rigidbody2D>().mass = 1.8f;
        this.gameObject.GetComponent<Player>().canJump = true;
        canAttack = true;
        animator.SetBool("hayball", false);
    }
    
    void Hayball()
    {
        timeStampHayball = Time.time + hayballCooldown;
        animator.SetBool("hayball", true);
        canAttack = false;
        this.gameObject.GetComponent<Player>().canJump = false;
        GetComponent<Rigidbody2D>().mass = 3000f;
        Invoke("HayballDown", 3f); 
    }
}
