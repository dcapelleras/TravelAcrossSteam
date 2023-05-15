using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    Renderer rend;
    [SerializeField] Material normalMaterial;
    [SerializeField] Material invisMaterial;
    [SerializeField] int canClickMask;
    [SerializeField] int cannotClickMask;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        gameObject.layer= 0;
    }

    public void BlockVision()
    {
        rend.material = invisMaterial;
        gameObject.layer = canClickMask;
    }

    public void UnblockVision()
    {
        gameObject.layer = cannotClickMask;
        rend.material = normalMaterial;
    }
}
