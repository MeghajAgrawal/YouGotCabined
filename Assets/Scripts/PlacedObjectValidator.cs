using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObjectValidator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    string name1;
    string name2;
    string name3;
    bool isadded = true;
    public InventoryItem item;
    public List<string> listtocheck;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(
            object1.GetComponent<PlaceableObject>().placedObject != null && 
            object2.GetComponent<PlaceableObject>().placedObject != null &&
            object3.GetComponent<PlaceableObject>().placedObject != null
        )
        {
        name1 = object1.GetComponent<PlaceableObject>().placedObject.name;
        name2 = object2.GetComponent<PlaceableObject>().placedObject.name;
        name3 = object3.GetComponent<PlaceableObject>().placedObject.name;
        CheckPlacedObject();
        }
    }
    void CheckPlacedObject()
    {
        if(isadded)
        {
            if(name1.Equals(listtocheck[0]) && name2.Equals(listtocheck[1])&& name3.Equals(listtocheck[2]))
            {
                isadded = false;
                InventoryItemList.instance.AddMenuItem(item);
                gameObject.GetComponent<PlaceObjectController>().enabled = false;
                GameObject PoemCanvas = GameObject.FindGameObjectWithTag("PoemUI").transform.GetChild(0).gameObject;
                PoemCanvas.SetActive(true);
                PoemCanvas.GetComponent<PoemUIManager>().SetContent("You have found the "+item.Title+"!<br>"+item.Description+"<br>You can check it out in the inventory","");
                PlayerLook.unlockCursor();
            }
        }
    }
}
