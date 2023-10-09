using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    public string Name;

    public bool isCollected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        if (isCollected && !PlayerController.instance.isMoving)
        {
            PlayerController.instance.UsePowerUp(Name);

            Destroy(this.gameObject);
        }
    }
}
