using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraController : MonoBehaviour
{
    public PixelPerfectCamera PixelCamera;

    bool cameraSet;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cameraSet)
        {
            if (Screen.width > 900 && !cameraSet)
            {
             //   PixelCamera.assetsPPU = 22;
                cameraSet = true;
            }
            else
            {
               // PixelCamera.assetsPPU = 26;
                cameraSet = true;
            }
        }
    }
}
