using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPadInteraction : MonoBehaviour
{
    public TextMeshProUGUI codetext;
    public string enteredCode;
    void Start()
    {
        enteredCode = "";
    }
    void Update()
    {
        codetext.text = enteredCode;
    }
    // Update is called once per frame
    public void enterednumber(string value)
    {
        enteredCode += value;
    }
    public void SubmitCode()
    {
        gameObject.GetComponentInParent<KeyPadObjectController>().enteredCode = enteredCode;
        if(gameObject.GetComponentInParent<KeyPadObjectController>().code.Equals(gameObject.GetComponentInParent<KeyPadObjectController>().enteredCode))
        {
            AudioSource audio = GameObject.FindGameObjectWithTag("UnlockSound").GetComponent<AudioSource>();
            audio.Play();
        }
        else
        {
            AudioSource audio = GameObject.FindGameObjectWithTag("ErrorSound").GetComponent<AudioSource>();
            audio.Play();
        }
        Close();
    }
    public void Clear()
    {
        enteredCode = "";
    }
    public void Close()
    {
        Clear();
        PlayerLook.lockCursor();
        gameObject.SetActive(false);
    }

}
