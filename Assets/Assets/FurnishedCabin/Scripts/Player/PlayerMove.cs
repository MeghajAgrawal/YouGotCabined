using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";
    public static float movementSpeed = 2f;

    [Range(0,0.5f)]
    [SerializeField] private float crouchSpeed = 0.3f; 
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private Vector3 standcenter = new Vector3(0,0,0);
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private Vector3 crouchcenter = new Vector3(0,0.5f,0);
    private bool crouching;
    private CharacterController charController;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        crouching = Input.GetKey(KeyCode.LeftControl);
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        var desiredheight = crouching ? crouchHeight : standHeight;
        Vector3 desiredcenter = crouching ? crouchcenter : standcenter;
        if(charController.height != desiredheight && charController.center != desiredcenter)
        {
            AdjustHeight(desiredheight,desiredcenter);
        }
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;     //CharacterController.SimpleMove() applies deltaTime
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        //simple move applies delta time automatically
        charController.SimpleMove(forwardMovement + rightMovement);
    }
    private void AdjustHeight(float height,Vector3 center)
    {
        charController.height = Mathf.Lerp(charController.height,height,crouchSpeed);
        charController.center = Vector3.Lerp(charController.center,center,crouchSpeed);
    }
}
