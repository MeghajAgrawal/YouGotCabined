using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateObject : MonoBehaviour // IBeginDragHandler, IDragHandler
{
    // Start is called before the first frame update
    public Slider zSlider; //set in editor
    public GameObject objectToRotate; //set in editor
    private readonly float rotationLimit = 15f;
    private float previousValue;
    public float Rotationz;
    public void Start()
    {
        Rotationz = objectToRotate.transform.rotation.eulerAngles.z;
        previousValue = zSlider.value;
    }
    public void Rotate(float value)
    {
        float sign = 1f;
        sign = previousValue > value ? -1 : 1;
        Rotationz += rotationLimit * sign;
        objectToRotate.transform.eulerAngles = new Vector3(0,0,Rotationz);
        previousValue = value;
    }

}
