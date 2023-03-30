using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float inputX;
    float inputZ;
    public float speed = 10f;

    //public ScriptableTest testscript;

    private void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3 (inputX, 0f, inputZ);
        transform.Translate(movement * speed * Time.deltaTime);


    }
}
