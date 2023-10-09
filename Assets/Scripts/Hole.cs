using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public static Hole instance;

    public GameObject HoleObject;
   

    public BoxCollider2D Box2D;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Box2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Pushable") )
        {


            other.gameObject.transform.position = transform.position;

            Animator anim = other.gameObject.GetComponent<Animator>();
             if (Vector3.Distance(other.gameObject.transform.position, transform.position) < Mathf.Epsilon)
            {
                other.gameObject.GetComponent<PushableBlock>().enabled = false;
                other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                other.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;

                anim.SetBool("Fall", true);

                // other.gameObject.GetComponent<PushableBlock>().BlockMask =
                HoleObject.SetActive(false);
               // AudioManager.instance.PlaySfx(2);
            }


        }
    }

    public void OffCollider()
    {
        Box2D.enabled = false;
    }
    public void OnCollider()
    {
        Box2D.enabled = true;
    }
}
