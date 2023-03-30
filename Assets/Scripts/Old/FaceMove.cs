using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMove : MonoBehaviour
{
    float mouseX;
    float mouseY;
    [SerializeField]float rotationSpeed = 5f;

    private void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(-mouseY, mouseX, Time.deltaTime);
        
    }
}
