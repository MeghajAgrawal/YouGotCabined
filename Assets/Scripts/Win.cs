using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject WinScreen;
    void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject.tag == "Player")		//player has collided with trigger
		{			
			WinScreen.SetActive(true);
            PlayerLook.unlockCursor();
		}
	}

    public void Quit()
    {
        Application.Quit();
    }
}
