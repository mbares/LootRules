using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour {

    private List<GameObject> models;
    public int selectionIndex = 0;
    private Text ability1text;
    private Text ability2text;
    public Rigidbody2D knight;
    public Rigidbody2D mage;
    public Rigidbody2D cowboy;
    public bool startGame=false;
	
	void Start () {
        if(this.gameObject.tag=="spCont1")
        {
            ability1text = GameObject.FindGameObjectWithTag("descQ").GetComponent<Text>();
            ability2text = GameObject.FindGameObjectWithTag("descE").GetComponent<Text>();            
        }
        else if (this.gameObject.tag == "spCont2")
        {
            ability1text = GameObject.FindGameObjectWithTag("desc4").GetComponent<Text>();
            ability2text = GameObject.FindGameObjectWithTag("desc6").GetComponent<Text>();            
        }
        models = new List<GameObject>();
        foreach(Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }
        models[selectionIndex].SetActive(true);
	}
	
    public void Select(int index)
    {
        if (index == selectionIndex)
            return;
        if (index < 0 || index >= models.Count)
            return;
        models[selectionIndex].SetActive(false);
        selectionIndex = index;
        models[selectionIndex].SetActive(true);
        if(index ==0)
        {
            ability1text.text="SHIELD (5s cd, 1s dur) \n-blocks damage \n-push enemies with it";
            ability2text.text = "RUSH(8s cd, 3s dur) \n-double movement speed \n-higher pushing power";
        }
         if(index ==1)
        {
            ability1text.text="STONEFORM (7s cd, 2s dur) \n-can't be pushed \n-brutal pushing power";
            ability2text.text="TELEPORT(5s cd) \n-teleports player for a small distance";
        }
        if(index ==2)
        {
            ability1text.text = "HAYBALL (10s cd, 3s dur) \n-roll in a ball with maximum pushing power";
            ability2text.text = "JUMPSHOT(7s cd) \n-propel yourself upwards with your pistols and fire";
        }
    }
    
    public void StartGame()
    {
        startGame = true;
    }
    
}
