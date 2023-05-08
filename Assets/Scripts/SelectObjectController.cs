using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectObjectController : MonoBehaviour
{
    public static SelectObjectController instance;

    public GameObject panel;

    public PlaceableObject placeableObject;

    public GameObject objectToPlace;

    [SerializeField]
    private GameObject content = default;
    [SerializeField]
    private GameObject MenuItemTemplate = default;

    Dictionary<string, GameObject> objectList = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> buttonList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(instance != null){
            Debug.LogError("Attempt to create multiple SelectObjectController");
            return;
        }
        instance = this;
    }

    public void AddItem(GameObject item)
    {
        
        string label = $"{item.name}";
            if(buttonList.ContainsKey(label))
            {
                buttonList[label].SetActive(true);
            }
            else
            {
                GameObject newMenuItem = Instantiate(MenuItemTemplate,transform.position,transform.rotation);
                newMenuItem.name = label;
                newMenuItem.transform.SetParent(content.transform,true);
                newMenuItem.SetActive(true);
                newMenuItem.GetComponentInChildren<TextMeshProUGUI>().text = label;
                // newMenuItem.GetComponent<ItemChooser>().item = item;
                objectList.Add(label, item);
                buttonList.Add(label, newMenuItem);
            }
    }
    
    public void Activate()
    {
        foreach(GameObject button in buttonList.Values)
        {
            if(button.activeSelf)
            {
                panel.SetActive(true);
                PlayerLook.unlockCursor();
            }
        }
    }

    public void Deactivate()
    {
        panel.SetActive(false);
        PlayerLook.lockCursor();
    }

    public void PlaceSelectedObject(Button button)
    {
        placeableObject.PlaceObject(objectList[button.name]);
        buttonList[button.name].SetActive(false);
        Deactivate();
    }
}
