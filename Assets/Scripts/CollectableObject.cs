using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableObject : InteractableObject
{
    // Start is called before the first frame update
    
    public string ItemTitle;
    public string ItemDescription;
    public string Itemtag;
    public Sprite image;
    public bool placeable;
    
    //Function is only for Poem UI
    void Show()
    {
        GameObject PoemCanvas = GameObject.FindGameObjectWithTag("PoemUI").transform.GetChild(0).gameObject;
        PoemCanvas.SetActive(true);
        PoemCanvas.GetComponent<PoemUIManager>().SetContent(ItemDescription,ItemTitle);
        PlayerLook.unlockCursor();
    }

    public void Interact()
    {
        if(Input.GetKeyUp(KeyCode.F))
        {
            if(gameObject.tag == "Poem")
            {	
                Show();
            }
            //add to inventory
            InventoryItem inventoryItem = (InventoryItem)ScriptableObject.CreateInstance(typeof(InventoryItem));
            inventoryItem.Title = ItemTitle;
            inventoryItem.Description = ItemDescription;
            inventoryItem.tag = Itemtag;
            inventoryItem.image = image;
            InventoryItemList.instance.AddMenuItem(inventoryItem);

            if(placeable)
            {
                SelectObjectController.instance.AddItem(gameObject);
            }
            //destroy object
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
