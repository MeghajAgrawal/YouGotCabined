using UnityEngine;
using System.Collections;

public class MoveObjectController : InteractObjectController 
{
	public string[] dependencies;	//list of items required to interact with object	

	protected Animator anim;

	protected const string animBoolName = "isOpen_Obj_";

	protected override void Start()
	{
		//call base class Start() method
		base.Start();
		
		//create AnimatorOverrideController to re-use animationController for sliding draws.
		anim = GetComponent<Animator>(); 
		anim.enabled = false;  //disable animation states by default.  
	}
		
	override public void Interact(InteractableObject interactableObject)
	{
		if(interactableObject is MoveableObject)
		{
			var movableObject = (MoveableObject) interactableObject;
			string animBoolNameNum = animBoolName + movableObject.objectNumber.ToString();

			bool isOpen = anim.GetBool(animBoolNameNum);	//need current state for message.

			if (isOpen) {
				Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0f));
				LayerMask iRayLM = LayerMask.NameToLayer("CollectRaycast");
				int mask = 1 << iRayLM.value;  
				RaycastHit hit; 
				if(Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, reachRange, mask))
				{
					//if raycast hits a collider on the CollectRaycast layer
					//then the object is already open and the player is looking at a collectable object
					//so we don't need to close the object.
					

					// check if object has highlight script
					// if so, toggle highlight
					// if not, do nothing
					if(raycastedObject == null || raycastedObject != hit.collider.gameObject)
					{
						if(raycastedObject != null && raycastedObject != hit.collider.gameObject)
						{
							raycastedObject.GetComponent<Highlight>()?.ToggleHighlight(false);
						}
						raycastedObject = hit.collider.gameObject;
						raycastedObject.GetComponent<Highlight>()?.ToggleHighlight(true);
					}

					msg = "Press F to collect " + hit.collider.gameObject.name;
					var collectableObject = (CollectableObject) hit.collider.gameObject.GetComponent<CollectableObject>();
					
					collectableObject.Interact();
					return;
				}
				else
				{
					if(raycastedObject != null)
					{
						raycastedObject.GetComponent<Highlight>()?.ToggleHighlight(false);
						raycastedObject = null;
					}
				}
			}
			
			bool unlocked = checkIfUnlocked();
			msg = getGuiMsg(isOpen, unlocked);
			
			ObjectInteract(unlocked, isOpen, animBoolNameNum);
		}
	}

	protected virtual void ObjectInteract(bool unlocked, bool isOpen, string animBoolNameNum)
	{
		if (unlocked && (Input.GetKeyUp(KeyCode.E)))
		{
			print("Interacted with "+gameObject.name+" at "+Time.realtimeSinceStartup);
			anim.enabled = true;
			anim.SetBool(animBoolNameNum,!isOpen);
			msg = getGuiMsg(!isOpen, unlocked);
		}
	}

	protected virtual bool checkIfUnlocked()
	{
		return InventoryItemList.instance.HasItems(dependencies);
	}

	protected virtual string getGuiMsg(bool isOpen, bool unlocked)
	{
		string rtnVal;
		if (unlocked)
		{
			rtnVal = isOpen ? "Press E to Close" : "Press E to Open";
		}
		else
		{
			rtnVal = "You cannot interact with this object yet. Explore more.";
		}
		return rtnVal;
	}

}
