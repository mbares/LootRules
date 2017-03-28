using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour {    
      
    [SerializeField]
    private Image content;
    public bool coolingDown=false;
    public float cooldownTime;
    bool done=false;	
	void Update () 
    {        
	    CoolingDown();       
	}    
	
	void CoolingDown()
	{
		if(coolingDown)
        {
            if (done == false)
            {
                content.fillAmount = 1.0f;
                done = true;
            }            
            content.fillAmount -= 1.0f / cooldownTime * Time.deltaTime;
            if(content.fillAmount==0)
            {
                done=false;
                coolingDown=false;
            }
        }
	}
}
