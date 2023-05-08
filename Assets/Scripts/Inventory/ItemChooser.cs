using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemChooser : MonoBehaviour
{
    [SerializeField]
    private ItemChangeEvent itemChangeEvent = default;
    // Start is called before the first frame update
    public InventoryItem item;
    public void ChooseItem()
    {
        itemChangeEvent.Invoke(item);
    }
}
