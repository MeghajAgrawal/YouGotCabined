using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName = "Mouse X";
    [SerializeField] private string mouseYInputName = "Mouse Y";
    public static float mouseSensitivity = 300f;

    [SerializeField] private Transform playerBody;
    private float xAxisClamp;
    private bool m_cursorIsLocked = true;

    public GameObject Inventory;
    public GameObject PauseScreen;

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }

    private void LockCursor()
    {
       
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            lockCursor();
        }
        else if (!m_cursorIsLocked)
        {
            unlockCursor();    
        }
        
    }
    public static void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseSensitivity = 300;
        PlayerMove.movementSpeed = 2;
    }
    public static void unlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseSensitivity = 0;
        PlayerMove.movementSpeed = 0;
    }

    public void UnPause()
    {
        PauseScreen.SetActive(false);
        lockCursor();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        CameraRotation();
        if(Input.GetKeyUp(KeyCode.Escape)){
            if(Inventory.activeSelf)
            {
                Inventory.SetActive(false);
                lockCursor();
                Inventory.GetComponent<CheckForLock>().CheckScreens(); 
            }
            else if (!Inventory.GetComponent<CheckForLock>().CheckScreensBool()){
                if(PauseScreen.activeSelf)
                {
                    PauseScreen.SetActive(false);
                    lockCursor();
                }
                else{
                    PauseScreen.SetActive(true);
                    unlockCursor();
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.I))
        {
            if(Inventory.activeSelf)
            {
                Inventory.SetActive(false);
                lockCursor();
                Inventory.GetComponent<CheckForLock>().CheckScreens();    
            }
            else
            {
                InventoryItemList.instance.ActivateFirstItem();
                Inventory.SetActive(true);
                unlockCursor();
            }
        }
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
