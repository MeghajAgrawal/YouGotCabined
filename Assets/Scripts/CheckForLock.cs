using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForLock : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> UIScreens;

    public void CheckScreens()
    {
        foreach(GameObject UIscreen in UIScreens)
        {
            if(UIscreen.activeInHierarchy)
            {
                PlayerLook.unlockCursor();
            }
        }
    }

    public bool CheckScreensBool()
    {
        foreach(GameObject UIscreen in UIScreens)
        {
            if(UIscreen.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }
}
