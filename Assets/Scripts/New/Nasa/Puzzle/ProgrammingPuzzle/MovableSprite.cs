using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableSprite : MonoBehaviour
{
    [SerializeField] CodingManager c_manager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        c_manager.hasCollided = true;
    }
}
