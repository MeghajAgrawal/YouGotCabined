using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class InventoryItemList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject content = default;
    [SerializeField]
    private GameObject MenuItemTemplate = default;
    [SerializeField]
    private List<InventoryItem> inventorylist = default;
    
    [SerializeField]
    private ItemChangeEvent itemChangeEvent = default;
    public static InventoryItemList instance;
    
    void Awake()
    {
        if(instance != null){
            Debug.LogError("Attempt to create multiple InventoryController");
            return;
        }
        instance = this;
    }

    public void AddMenuItem(InventoryItem item)
    {
        inventorylist.Add(item);
        GameObject newMenuItem;
        string label = $"{item.Title}";
        newMenuItem = Instantiate(MenuItemTemplate,transform.position,transform.rotation);
        newMenuItem.name = label;
        newMenuItem.transform.SetParent(content.transform,true);
        newMenuItem.SetActive(true);
        newMenuItem.GetComponentInChildren<TextMeshProUGUI>().text = label;
        newMenuItem.GetComponent<ItemChooser>().item = item;
    }
    public void ActivateFirstItem()
    {
        if(inventorylist.Count > 0)
        {
            InventoryItem activeItem = inventorylist[0];
            if(activeItem != null)
            {
                itemChangeEvent.Invoke(activeItem);
            }
        }
        
    }
    public bool HasItems(string[] items)
    {
        int count = 0;
        foreach(string item in items)
        {
            foreach(InventoryItem inventoryItem in inventorylist)
            {
                if(inventoryItem.Title.Equals(item)){
                    count++;
                }
            }
        }
        return count == items.Length;
    }
}
