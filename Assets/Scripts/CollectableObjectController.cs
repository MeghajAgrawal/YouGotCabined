using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObjectController: InteractObjectController
{
    protected override void Start()
	{
		//call base class Start() method
		base.Start();
		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer("CollectRaycast");
		rayLayerMask = 1 << iRayLM.value;
	}

    public override void Interact(InteractableObject interactableObject)
    {
        msg = "Press F to collect " + interactableObject.name;
        
        var collectableObject = (CollectableObject) interactableObject;
            
        collectableObject.Interact();
        
    }
}
