using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField]
    GameObject escMenu;
    [SerializeField]
    GameObject howToPlayPanel;
    [SerializeField]
    GameObject endMenu;
    bool endGame=false;
    bool done = false;

	void Update () 
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            endGame = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().endGame;
            EscMenu();
            if (endGame && !done)
            {
                done = true;
                Invoke("EndMenu", 3f);                
            }
        }
	}
    public void EndMenu()
    {
        endMenu.SetActive(true);
    }

 
    public void EscMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escMenu.activeInHierarchy && !endGame)
        {
            escMenu.SetActive(false);
            Time.timeScale = 1;
            Controller.paused = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && escMenu.activeInHierarchy == false && !endGame)
        {
            escMenu.SetActive(true);
            Time.timeScale = 0;
            Controller.paused = true;
        }
    }
    public void Resume()
    {
        escMenu.SetActive(false);
        Time.timeScale = 1;
        Controller.paused = false;
    }
    public void QuitToMainMenu()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().ChangeScene(0);
        if(endMenu.activeInHierarchy)
        {
            endMenu.SetActive(false);
            done = false;
        }
    }
	 public void BackToSelection()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().ChangeScene(1);
        if(endMenu.activeInHierarchy)
        {
            endMenu.SetActive(false);
            done = false;
        }
    }
   
    public void StartGame()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>().ChangeScene(1);
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }
    public void HowToPlay()
    {
        if (howToPlayPanel.activeInHierarchy)
        {
            howToPlayPanel.SetActive(false);
        }
        else
        {
            howToPlayPanel.SetActive(true);
        }
    }
}
