using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateObjectValidator : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider zSlider1;
    public Slider zSlider2;
    public Slider zSlider3;
    
    public string enteredCode;
    public bool locked = true;
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(nameof(CodeChecker));
    }
    void Update()
    {
        if(locked)
            StartCoroutine(nameof(CodeChecker));
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        PlayerLook.lockCursor();
    }

    IEnumerator CodeChecker()
    {
        enteredCode = zSlider1.value.ToString()+zSlider2.value.ToString()+zSlider3.value.ToString();
        gameObject.GetComponentInParent<KeyPadObjectController>().enteredCode = enteredCode;
        if(gameObject.GetComponentInParent<KeyPadObjectController>().code.Equals(gameObject.GetComponentInParent<KeyPadObjectController>().enteredCode))
        {
            print("Code is correct " + enteredCode);
            AudioSource audio = GameObject.FindGameObjectWithTag("UnlockSound").GetComponent<AudioSource>();
            audio.Play();
            yield return new WaitForSeconds(0.5f);
            gameObject.SetActive(false);
            PlayerLook.lockCursor();
            locked = false;
        }
    }
}
