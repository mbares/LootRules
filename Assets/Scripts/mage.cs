using UnityEngine;
using System.Collections;

public class mage : MonoBehaviour {

    float attackCooldown = 1f;
    float timeStampAttack = 0;
    float timeStampTeleport = 0;
    float timeStampStone = 0;
    float teleportCooldown = 5f;
    float stoneCooldown = 7f;    
    public float speed = 10f;
    public Rigidbody2D fireball;
    public GameObject attack;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }  
	void Update () 
    {
        if (Controller.paused == true)
        {
            return;
        }
        MageAbilities();
	}

   
    public void MageAbilities()
    {
        if (this.tag == "player1")
        {
            if (Input.GetKeyDown(KeyCode.Space) && timeStampAttack <= Time.time)
            {
               Fireball();
            }
            if(Input.GetKeyDown(KeyCode.E) && timeStampTeleport <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cdE").GetComponent<Cooldown>().cooldownTime = teleportCooldown;
                GameObject.FindGameObjectWithTag("cdE").GetComponent<Cooldown>().coolingDown = true;
                Teleport();
            }
            if(Input.GetKeyDown(KeyCode.Q) && timeStampStone <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cdQ").GetComponent<Cooldown>().cooldownTime = stoneCooldown;
                GameObject.FindGameObjectWithTag("cdQ").GetComponent<Cooldown>().coolingDown = true;
                Stoneform();                   
            }
        }
        if (this.tag == "player2")
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && timeStampAttack <= Time.time)
            {
                Fireball();
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) && timeStampTeleport <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cd6").GetComponent<Cooldown>().cooldownTime = teleportCooldown;
                GameObject.FindGameObjectWithTag("cd6").GetComponent<Cooldown>().coolingDown = true;
                Teleport();
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) && timeStampStone <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cd4").GetComponent<Cooldown>().cooldownTime = stoneCooldown;
                GameObject.FindGameObjectWithTag("cd4").GetComponent<Cooldown>().coolingDown = true;
                Stoneform();          
            }
        }
    }
    void StoneformDown()
    {
        GetComponent<Rigidbody2D>().mass = 1f;
        animator.SetBool("isStone", false);
    }
    void Fireball()
    {
        this.GetComponent<AudioSource>().Play();
        animator.SetTrigger("attack");
        timeStampAttack = Time.time + attackCooldown;
        Rigidbody2D fireballInstance = Instantiate(fireball, attack.transform.position, attack.transform.rotation) as Rigidbody2D;
        if (attack.transform.rotation == Quaternion.Euler(0, 0, 0))
            fireballInstance.velocity = new Vector2(speed, 0);
        else
            fireballInstance.velocity = new Vector2(-speed, 0);
    }
    void Teleport()
    {
        timeStampTeleport = Time.time + teleportCooldown;
        if (this.transform.rotation == Quaternion.Euler(0, 0, 0))
            this.transform.position = new Vector2(transform.position.x + 3, transform.position.y);
        else
            this.transform.position = new Vector2(transform.position.x - 3, transform.position.y);
    }
    void Stoneform()
    {
        timeStampStone = Time.time + stoneCooldown;
        GetComponent<Rigidbody2D>().mass = 1000f;
        animator.SetBool("isStone", true);
        Invoke("StoneformDown", 2f);  
    }
}
