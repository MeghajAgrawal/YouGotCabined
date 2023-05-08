using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectController : InteractObjectController
{
    public override void Interact(InteractableObject interactableObject)
    {
        if(interactableObject is PlaceableObject)
        {
            var placeableObject = (PlaceableObject) interactableObject;
            msg = placeableObject.placedObject != null ? "Press F to pick up "+placeableObject.placedObject.gameObject.name : "Press P to place an object";
            placeableObject.Interact();
        }
    }
}
