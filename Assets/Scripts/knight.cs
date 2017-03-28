using UnityEngine;
using System.Collections;

public class knight : MonoBehaviour {

    float attackCooldown = 1f;
    float rushCooldown = 8f;
    float shieldCooldown = 5f;
    float timeStampAttack = 0;
    float timeStampRush = 0;
    float timeStampShield = 0;
    bool attackEnabled = true;
    public float speed = 10f;
    public GameObject attack;
    public Rigidbody2D sword_slash;
    [SerializeField]
    GameObject shield;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Controller.paused == true)
        {
            return;
        }
        KnightAbilities();
    }

    public void KnightAbilities()
    {
        if (this.tag == "player1")
        {
            if (Input.GetKeyDown(KeyCode.Space) && timeStampAttack <= Time.time && attackEnabled)
            {
                SwordSlash();
            }
            if (Input.GetKeyDown(KeyCode.E) && timeStampRush <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cdE").GetComponent<Cooldown>().cooldownTime = rushCooldown;
                GameObject.FindGameObjectWithTag("cdE").GetComponent<Cooldown>().coolingDown = true;
                Rush();
            }
            if (Input.GetKeyDown(KeyCode.Q) && timeStampShield <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cdQ").GetComponent<Cooldown>().cooldownTime = shieldCooldown;
                GameObject.FindGameObjectWithTag("cdQ").GetComponent<Cooldown>().coolingDown = true;
                Shield();
            }
        }
        if (this.tag == "player2")
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && timeStampAttack <= Time.time && attackEnabled)
            {
                SwordSlash();
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) && timeStampRush <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cd6").GetComponent<Cooldown>().cooldownTime = rushCooldown;
                GameObject.FindGameObjectWithTag("cd6").GetComponent<Cooldown>().coolingDown = true;
                Rush();
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) && timeStampShield <= Time.time)
            {
                GameObject.FindGameObjectWithTag("cd4").GetComponent<Cooldown>().cooldownTime = shieldCooldown;
                GameObject.FindGameObjectWithTag("cd4").GetComponent<Cooldown>().coolingDown = true;
                Shield();
            }
        }
    }
    void ShieldDown()
    {
        shield.SetActive(false);
        attackEnabled = true;
    }
    void RushDown()
    {
        gameObject.GetComponent<Player>().moveSpeed = 5;
        gameObject.GetComponent<Rigidbody2D>().mass = 1.4f;
    }
    void SwordSlash()
    {
        this.GetComponent<AudioSource>().Play();
        animator.SetTrigger("attack");
        timeStampAttack = Time.time + attackCooldown;
        Rigidbody2D sword_slashInstance = Instantiate(sword_slash, attack.transform.position, attack.transform.rotation) as Rigidbody2D;
        if (attack.transform.rotation == Quaternion.Euler(0, 0, 0))
            sword_slashInstance.velocity = new Vector2(speed, 0);
        else
            sword_slashInstance.velocity = new Vector2(-speed, 0);
    }
    void Rush()
    {
        timeStampRush = Time.time + rushCooldown;
        gameObject.GetComponent<Player>().moveSpeed = 10;
        gameObject.GetComponent<Rigidbody2D>().mass = 10;
        Invoke("RushDown", 3f);
    }
    void Shield()
    {
        timeStampShield = Time.time + shieldCooldown;
        shield.SetActive(true);
        attackEnabled = false;
        Invoke("ShieldDown", 2f);
    }
}