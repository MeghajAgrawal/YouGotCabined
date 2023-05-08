using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadObjectController : MoveObjectController
{
	public string code;
    public string enteredCode;	
    public GameObject Keypad;

	protected override string getGuiMsg(bool isOpen, bool unlocked)
	{
		string rtnVal;
		if (unlocked)
		{
			rtnVal = isOpen ? "Press E to Close" : "Press E to Open";
		}
		else
		{
            if(Keypad.activeInHierarchy)
            {
                return "";
            }
			rtnVal = "Press E to Enter Code";
		}
		return rtnVal;
	}

	protected override void ObjectInteract(bool unlocked, bool isOpen, string animBoolNameNum)
	{
		if((Input.GetKeyUp(KeyCode.E)))
		{
			if(unlocked)
			{
				anim.enabled = true;
				anim.SetBool(animBoolNameNum,!isOpen);
				msg = getGuiMsg(!isOpen, unlocked);
			}
			else
			{
				PlayerLook.unlockCursor();
				Keypad.SetActive(true);
			}
		}
		
	}

	protected override bool checkIfUnlocked()
	{
		return code.Equals(enteredCode);
	}
}
