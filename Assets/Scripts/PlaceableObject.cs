using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : InteractableObject
{
    public GameObject placedObject;
    public void Interact()
    {
        if(placedObject == null && Input.GetKeyUp(KeyCode.P))
        {
            SelectObjectController.instance.Activate();
            SelectObjectController.instance.placeableObject = this;
            Debug.Log("PlaceableObject Interact");
        }
        else if(placedObject != null && Input.GetKeyUp(KeyCode.F))
        {
            SelectObjectController.instance.AddItem(placedObject);
            Destroy(placedObject);
            placedObject = null;
        }
    }

    public void PlaceObject(GameObject objectToPlace)
    {
        // objectToPlace.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        placedObject = Instantiate(objectToPlace, transform.position + new Vector3(0, 0.03f, 0), objectToPlace.transform.rotation);
        placedObject.name = objectToPlace.name;
        placedObject.SetActive(true);
    }
}
