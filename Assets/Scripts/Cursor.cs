using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture2D cursorArrow;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        
    }

    void Start()
    {
        // Cursor.visible= false;
       // Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
      
void mouseEnter()
    {
        // Cursor.visible = false;
        // Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }
}
