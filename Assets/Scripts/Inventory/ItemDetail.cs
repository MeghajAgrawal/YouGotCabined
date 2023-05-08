using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemDetail : MonoBehaviour
{
    [SerializeField]
    private InventoryItem item = default;
    [SerializeField]
    private TextMeshProUGUI Titletext = default;
    [SerializeField]
    private TextMeshProUGUI Descriptiontext = default;
    public TextMeshProUGUI PoemText = default;
    public Image image;
    public GameObject sideimage;
    public Image taggedImage;
    public GameObject mainImage;

    public void SetItem(InventoryItem recItem)
    {
        item = recItem;
        Titletext.text = item.Title;
        if(item.tag == "Poem"){
            mainImage.SetActive(false);
            PoemText.text = item.Description;
            Descriptiontext.text = "";
            sideimage.SetActive(true);
        }
        else if(item.tag == "Object")
        {
            mainImage.SetActive(true);
            Descriptiontext.text = "";
            taggedImage.sprite  = item.image;
            sideimage.SetActive(false);
        }
        else{
            mainImage.SetActive(false);
            PoemText.text = "";
            Descriptiontext.text = item.Description;
            sideimage.SetActive(true);
        }
        image.sprite = item.image;
    }
}
