using UnityEngine;
using System.Collections;

public class DestroyProjectile : MonoBehaviour {


	void Start () {
        if(gameObject.tag=="fireball")
            GameObject.Destroy(this.gameObject, 3f);
        else if(gameObject.tag=="sword_slash")
            GameObject.Destroy(this.gameObject, 0.2f);
        else if(gameObject.tag=="dragonFireball")
            GameObject.Destroy(this.gameObject, 2f);
        else if (gameObject.tag == "bullet")
            GameObject.Destroy(this.gameObject, 0.6f);
	}
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        coll.gameObject.GetComponent<Player>().HP = coll.gameObject.GetComponent<Player>().HP - 5;      
        GameObject.Destroy(this.gameObject, 0.1f);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "boss" && this.tag !="dragonFireball")
        {
            coll.gameObject.GetComponent<Boss>().HP = coll.gameObject.GetComponent<Boss>().HP - 5;
            GameObject.Destroy(this.gameObject, 0.1f);
        }
    }
}
