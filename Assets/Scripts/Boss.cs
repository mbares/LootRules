using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {
    public float HP = 100f;
    public bool isDead=false;
    Animator animator;
    [SerializeField]
    GameObject dragonFireball;
    [SerializeField]
    GameObject flameBreath;
    Object flame;
    [SerializeField]
    Sprite deadDragon;
    [SerializeField]
    GameObject dragonBg;

    void Start()
    {
        animator = GetComponent<Animator>();
        this.gameObject.SetActive(false);
        Invoke("BossAppear", 4f);
        InvokeRepeating("DragonFireball", 3f, Random.Range(0.5f, 2f));
    }
    void Update()
    {
        if (HP <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
            dragonBg.GetComponent<SpriteRenderer>().sprite = deadDragon;
            Destroy(flame);

        }
    }
   
    void BossAppear()
    {
        this.transform.position = new Vector2(Random.Range(-11.24f, 11.3f), Random.Range(-6.12f, 4.48f));
        this.gameObject.SetActive(true);
        this.GetComponent<Collider2D>().offset = new Vector2(0, 0);
        FireBreath();
        Invoke("BossDisappear", Random.Range(4f, 7f));
    }
    void BossDisappear()
    {
        DestroyObject(flame);
        this.gameObject.SetActive(false);
        Invoke("BossAppear", Random.Range(3f, 5f));
    }
    
    void FireBreath()
    {
        int rand = Random.Range(0, 4);
        Vector2 left = new Vector2(this.transform.position.x-0.56f, this.transform.position.y-0.93f);
        Vector2 right= new Vector2(this.transform.position.x+0.32f, this.transform.position.y-0.98f);
        Vector2 up = new Vector2(this.transform.position.x+0.41f, this.transform.position.y + 2.33f);
        Vector2 down = new Vector2(this.transform.position.x-0.43f, this.transform.position.y - 0.5f);
        Quaternion leftQ = new Quaternion(this.transform.rotation.x, 180, this.transform.rotation.z, this.transform.rotation.w);        
        if (rand == 0)
        {
            animator.SetBool("headLeft", true);
            flame = Instantiate(flameBreath, left, leftQ);
        }
        else if(rand == 1)
        {
            animator.SetBool("headRight", true);
            flame = Instantiate(flameBreath, right, Quaternion.identity);
        }
        else if(rand ==2)
        {
            animator.SetBool("headUp", true);
            this.GetComponent<Collider2D>().offset=new Vector2(0, 3f);
            flame = Instantiate(flameBreath, up, Quaternion.identity*Quaternion.Euler(0,0,90));
        }
        else 
        {
            animator.SetBool("headDown", true);
            flame = Instantiate(flameBreath, down, Quaternion.identity*Quaternion.Euler(0,0,-90));
        }
    }
    void DragonFireball()
    {
        if(Controller.paused)
        {
            return;
        }
        Instantiate(dragonFireball, new Vector2(Random.Range(-13.05f, 13.12f), 8.66f),dragonFireball.transform.rotation);
    }
}
