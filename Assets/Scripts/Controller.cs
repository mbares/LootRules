using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour {
    private static Controller controller;
    [SerializeField]
    private int player1pick=0;
    [SerializeField]
    private int player2pick=0;    
    bool done = false;    
    public static bool paused = false;
    public bool endGame = false;
    public Rigidbody2D knight;
    public Rigidbody2D mage;
    public Rigidbody2D cowboy;
    [SerializeField]
    GameObject lootChest;
    [SerializeField]
    public GameObject player1Object;
    [SerializeField]
    GameObject player2Object;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Text announcement;
    [SerializeField]
    AudioClip characterSelectMusic;
    [SerializeField]
    AudioClip arenaMusic;       
    public static Controller controllerInstance;    
    void Awake()
    {            
        if(!controllerInstance)
        {
            controllerInstance = this;
        }
        else if(controllerInstance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
    }

   void Update()
    {
        Camera.main.aspect = 16f / 9f;           
        if (SceneManager.GetActiveScene().buildIndex == 2 && (player1Object == null || player2Object == null || boss==null || announcement==null))
        {
            boss = GameObject.FindGameObjectWithTag("boss");
            player1Object = GameObject.FindGameObjectWithTag("player1");
            player2Object = GameObject.FindGameObjectWithTag("player2");
            announcement = GameObject.Find("announcement").GetComponent<Text>();
        }
        if (player1Object != null && player2Object != null && boss != null)
        {
            if (player1Object.GetComponent<Player>().isDead && boss.GetComponent<Boss>().isDead && !done)
            {
                endGame = true;
                paused = true;
                announcement.text = "PLAYER 2 WINS";                
                GameObject lootChestInstance = Instantiate(lootChest, new Vector3(0f, -4.1f, 0), Quaternion.identity) as GameObject;
                lootChestInstance.GetComponent<Animator>().SetInteger("chestReward", player2pick);
                done = true;
            }
            else if (player2Object.GetComponent<Player>().isDead && boss.GetComponent<Boss>().isDead && !done)
            {
                endGame = true;
                paused = true;
                announcement.text = "PLAYER 1 WINS";
                GameObject lootChestInstance = Instantiate(lootChest, new Vector3(0f, -4.1f, 0), Quaternion.identity) as GameObject;
                lootChestInstance.GetComponent<Animator>().SetInteger("chestReward", player1pick);
                done = true;
            }
            else if (player1Object.GetComponent<Player>().isDead && player2Object.GetComponent<Player>().isDead && !done)
            {
                endGame = true;
                Time.timeScale = 0;
                paused = true;
                announcement.text = "BOSS WINS";
                done = true;
            }
        }
       if(SceneManager.GetActiveScene().buildIndex == 2 && this.GetComponent<AudioSource>().clip != arenaMusic)
       {
           this.GetComponent<AudioSource>().clip = arenaMusic;
           this.GetComponent<AudioSource>().Play();
       }
       if((SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1) && this.GetComponent<AudioSource>().clip != characterSelectMusic)
       {
           this.GetComponent<AudioSource>().clip = characterSelectMusic;
           this.GetComponent<AudioSource>().Play();
       }
       if( SceneManager.GetActiveScene().buildIndex == 1 && GameObject.FindGameObjectWithTag("spCont1").GetComponent<CharacterSelect>().startGame)
       {
           player1pick = GameObject.FindGameObjectWithTag("spCont1").GetComponent<CharacterSelect>().selectionIndex;
           player2pick = GameObject.FindGameObjectWithTag("spCont2").GetComponent<CharacterSelect>().selectionIndex;
           ChangeScene(2);          
       }
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
        endGame = false;
        paused = false;
        done = false;
        if (scene == 2)
        {
            
            Time.timeScale = 1;            
            Invoke("InstantiateP1", 0.1f);
            Invoke("InstantiateP2", 0.1f);           
        }      
        if(scene == 0 || scene == 1)
        {
            player1pick = 0;
            player2pick = 0;                       
        }        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
  
   
    
    private void InstantiateP1()
    {
        if (player1pick == 0)
        {
            knight.tag = "player1";
            Instantiate(knight, new Vector3(-5.7f, -6.1f, 0), Quaternion.identity);            
        }
        else if (player1pick == 1)
        {
            mage.tag = "player1";
            Instantiate(mage, new Vector3(-5.7f, -6.1f, 0), Quaternion.identity);            
        }
        else if (player1pick == 2)
        {
            cowboy.tag = "player1";
            Instantiate(cowboy, new Vector3(-5.7f, -6.1f, 0), Quaternion.identity);
        }      
    }
    private void InstantiateP2()
    {
		Quaternion leftQ = new Quaternion(this.transform.rotation.x, 180, this.transform.rotation.z, this.transform.rotation.w);
        if (player2pick == 0)
        {
            knight.tag = "player2";
            Instantiate(knight, new Vector3(4.45f, -6.1f, 0), leftQ);           
        }
        else if (player2pick == 1)
        {
            mage.tag = "player2";
            Instantiate(mage, new Vector3(4.45f, -6.1f, 0), leftQ);          
        }
        else if (player2pick == 2)
        {
            cowboy.tag = "player2";
            Instantiate(cowboy, new Vector3(4.45f, -6.1f, 0), leftQ);
        } 
       
    }
}
