using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObjectController : MonoBehaviour
{
    public float reachRange = 3f;	

    protected Camera fpsCam;
	protected GameObject player;

	protected bool playerEntered;
	protected bool showInteractMsg;
	protected GUIStyle guiStyle;
	protected string msg;

	protected int rayLayerMask;

	protected GameObject raycastedObject;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

		fpsCam = Camera.main;
		if (fpsCam == null)	//a reference to Camera is required for rayasts
		{
			Debug.LogError("A camera tagged 'MainCamera' is missing.");
		}

		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer("InteractRaycast");
		rayLayerMask = 1 << iRayLM.value;  

		//setup GUI style settings for user prompts
		setupGui();
    }

    void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject == player)		//player has collided with trigger
		{			
			playerEntered = true;
			// gameObject.GetComponent<Highlight>()?.ToggleHighlight(true);
		}
	}

	void OnTriggerExit(Collider other)
	{		
		if (other.gameObject == player)		//player has exited trigger
		{			
			playerEntered = false;
			//hide interact message as player may not have been looking at object when they left
			showInteractMsg = false;		
			// gameObject.GetComponent<Highlight>()?.ToggleHighlight(false);
		}
	}

    public abstract void Interact(InteractableObject moveableObject);

    // Update is called once per frame
    void Update()
    {
		msg = "";
        if (playerEntered)
		{	
			//center point of viewport in World space.
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f,0.5f,0f));
			RaycastHit hit;

			//if raycast hits a collider on the rayLayerMask
			if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit,reachRange,rayLayerMask))
			{
				InteractableObject interactableObject = null;
				//is the object of the collider player is looking at the same as me?
				if (!isEqualToParent(hit.collider, out interactableObject))
				{	//it's not so return;
					return;
				}

				if (interactableObject != null)		//hit object must have InteractableObject script attached
				{
					if(raycastedObject == null || raycastedObject != interactableObject.gameObject)
					{
						if(raycastedObject != null && raycastedObject != interactableObject.gameObject)
						{
							raycastedObject.GetComponent<Highlight>()?.ToggleHighlight(false);
						}
						raycastedObject = interactableObject.gameObject;
						raycastedObject.GetComponent<Highlight>()?.ToggleHighlight(true);
					}
					showInteractMsg = true;
					raycastedObject = interactableObject.gameObject;
					Interact(interactableObject);
				}
			}
			else
			{
				showInteractMsg = false;
				if(raycastedObject != null)
				{
					raycastedObject.GetComponent<Highlight>()?.ToggleHighlight(false);
					raycastedObject = null;
				}
			}
		}
    }

	//is current gameObject equal to the gameObject of other.  check its parents
	private bool isEqualToParent(Collider other, out InteractableObject draw)
	{
		draw = null;
		bool rtnVal = false;
		try
		{
			int maxWalk = 6;
			draw = other.GetComponent<InteractableObject>();

			GameObject currentGO = other.gameObject;
			for(int i=0;i<maxWalk;i++)
			{
				if (currentGO.Equals(this.gameObject))
				{
					rtnVal = true;	
					if (draw== null) draw = currentGO.GetComponentInParent<MoveableObject>();
					break;			//exit loop early.
				}

				//not equal to if reached this far in loop. move to parent if exists.
				if (currentGO.transform.parent != null)		//is there a parent
				{
					currentGO = currentGO.transform.parent.gameObject;
				}
			}
		} 
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
			
		return rtnVal;

	}

	#region GUI Config

	//configure the style of the GUI
	private void setupGui()
	{
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 36;
		guiStyle.fontStyle = FontStyle.Bold;
		guiStyle.normal.textColor = Color.white;
		msg = "Press F to take Item";
	}

	void OnGUI()
	{
		if (showInteractMsg)  //show on-screen prompts to user for guide.
		{
			GUI.Label(new Rect (50,Screen.height - 50,200,50), msg,guiStyle);
		}
	}		
	//End of GUI Config --------------
	#endregion
}
