using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MakeTransparent : MonoBehaviour
{
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.color = new Color32(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
