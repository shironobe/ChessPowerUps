using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ban : MonoBehaviour
{
    public bool isRock;
    SpriteRenderer sr;

    public Sprite on, off;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pushable"))
        {
            isRock = true;
            sr.sprite = on;
            BanBlock.instance.CheckBans();
        }
    }
    //private void OnCollisionStay2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Pushable") || other.gameObject.CompareTag("Robo2"))
    //    {
    //        isRock = true;
    //        sr.sprite = on;
    //    }
    //}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pushable") )
        {

            if (sr.sprite == off)
            {
               // AudioManager.instance.PlaySfx(6);
            }
            BanBlock.instance.CheckBans();
        }
    }
    //private void OnCollisionEnter2D(Collision2D other)
    //{

    //    if (other.gameObject.CompareTag("Pushable") || other.gameObject.CompareTag("Robo2"))
    //    {
    //        // AudioManager.instance.PlaySfx(5);
    //    }
    //}


    private void OnTriggerExit2D(Collider2D other)
    {

       // if (BanBlock.instance.block2d.enabled == true)
        //{
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Pushable"))
            {
                isRock = false;
                sr.sprite = off;
              //  BanBlock.instance.CheckBans();
            }
        BanBlock.instance.CheckBans();
        // }
    }
    //private void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Pushable") || other.gameObject.CompareTag("Robo2"))
    //    {
    //        isRock = false;
    //        sr.sprite = off;
    //    }
    //}
    



}
