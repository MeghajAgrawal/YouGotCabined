using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory", order = 0)]
public class InventoryItem : ScriptableObject
{
    // Start is called before the first frame update
    public string Title = "Poem";
    public string Description = "ITEM IS DESCRIBED AS";
    public string tag = "";
    public Sprite image;

}
