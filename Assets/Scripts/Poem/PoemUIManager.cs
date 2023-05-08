using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PoemUIManager : MonoBehaviour
{
    public TextMeshProUGUI poemContent;
    public TextMeshProUGUI poemTitle;
    // Start is called before the first frame update
    public void SetContent(string content, string title)
    {
        poemTitle.text = title;
        poemContent.text = content;
    }    
    public void Close()
    {
        gameObject.SetActive(false);  
        PlayerLook.lockCursor();
    }
}
