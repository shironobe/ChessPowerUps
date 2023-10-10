using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{

    public static PushableBlock instance;
    public bool isPushed;
    public bool isHooked;

    public LayerMask BlockMask = 0;
    public float detectionRadius = 1f;
    public float detectionRadiusForHook = 1f;
    public Vector3 destination;
    public float speed ;
    float speedMultiplyer = 1.5f;

    public bool shouldHook;

    public bool isDestroyable;

    bool usePowerUp;

    public Vector3 direction;
    private void Awake()
    {
        instance = this;
       
    }

  
    
    void Start()
    {
        destination = transform.position;
        
        shouldHook = false;
      

       
       
    }

    // Update is called once per frame
    void Update()
    {
        // if (Vector3.Distance(PlayerController.instance.transform.position, PlayerController.instance.Destination) < Mathf.Epsilon)







        if (Vector3.Distance(transform.position, destination) < Mathf.Epsilon)
        {


            transform.position = destination;

            isPushed = false;

            isHooked = false;


            PlayerController.instance.canMove = true;
            //  RoboController.instance.isDead = true ;

        }
        else
        {


            PlayerController.instance.isDead = false;
            //  RoboController.instance.isDead = false;
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);


        }


        if (usePowerUp)
        {


            if (Vector3.Distance(transform.position, destination) < Mathf.Epsilon)
            {
                if (CheckDirectionPushedWithForce(direction))
                {

                    destination = transform.position + direction;
                }
            }



            // anim.SetBool("isMoving", isPushed);
        }

    }
    public void Push(Vector3 direction, float speed)
    {
      
        if (!isPushed)
        {
            if (CheckDirection(direction))
            {
               // Debug.Log("Here");
               // if (Vector3.Distance(RoboController.instance.transform.position, RoboController.instance.Destination) < Mathf.Epsilon)
                {
                  
                   

                    destination = transform.position + direction;

                    AudioManager.instance.PlaySfx(3);
                    isPushed = true;
                    Manager.instance.MovesLeft--;
                    PlayerController.instance.MovePlayer(direction);


                    //  Invoke("Grablastpos", 0f);
                }
            }
            // anim.SetBool("isMoving", isPushed);

            else
            {
                // transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
            }


        }
    }

    public void PushForce(Vector3 dir)
    {
        usePowerUp = true;
        direction = dir;
    }


    private bool CheckDirectionPushedWithForce(Vector3 direction)
    {
        //  
        //  RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);


        Debug.DrawRay(transform.position, direction);

        // RaycastHit2D hit1 = Physics2D.Raycast(hit1pos.transform.position, direction, detectionRadius, BlockMask);
        //  Debug.DrawRay(hit1pos.transform.position, direction);

        if (hit)
        {

            if (hit.collider.gameObject.CompareTag("Pushable"))
            {



                PushableBlock pushableBlock = hit.collider.GetComponent<PushableBlock>();



                if (!pushableBlock)
                    return false;
                if (usePowerUp)
                {
                    pushableBlock.PushForce(direction);
                    usePowerUp = false;
                    return false;

                }

                pushableBlock.Push(direction, speedMultiplyer);
                AudioManager.instance.PlaySfx(3);
                //Destination = transform.position + direction;
                usePowerUp = false;
                return false;
            }



            //  Debug.Log(hit.collider.gameObject.name);
            // Debug.Log("Here");
            usePowerUp = false;
            return false;
        }


        return true;

    }










    private bool CheckDirection(Vector3 direction)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);
        // Debug.Log(hit);


        // Debug.DrawRay(transform.position, direction);
        if (hit.collider != null)
        {
            return false;
            // string tag = hit.transform.tag;
            //if (hit.collider.gameObject.CompareTag("Pushable"))
            //{
            //    Debug.Log(tag);
            //    Debug.Log(hit);
            //    return false;
            //}

        }
        return true;
    }

    public void MoveBlock(Vector3 direction)
    {

        if(CheckDirectionAgain(direction)){

           // Debug.Log("Running");
            destination = transform.position + direction;
        }


    }
    public void PushAgain(Vector3 direction, float speed)
    {

        if (!isPushed)
        {
            if (CheckDirectionAgain(direction))
            {
                if (Vector3.Distance(PlayerController.instance.transform.position, PlayerController.instance.Destination) < Mathf.Epsilon)
                {
                    // Debug.Log("ImPushed");

                    //  ICommand move = new CommandMove(mPlayer, direction);
                    //   mInvoker.Execute(move);
                    //destination = mPlayer.transform.position;
                   
                    destination = transform.position + direction;

                 
                    isPushed = true;

                   // PlayerController.instance.MovePlayer(direction);
                    

                    //  Invoke("Grablastpos", 0f);
                }

                // anim.SetBool("isMoving", isPushed);
            }
            else
            {
                // transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.deltaTime);
            }
        }
    }
    private bool CheckDirectionAgain(Vector3 direction)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);
        // Debug.Log(hit);


        // Debug.DrawRay(transform.position, direction);

        // RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider != null)
        {


                    return false;
                

            
        }
        return true;

    }

    public void Hook(Vector3 direction, float speed)
    {

        if (!isHooked)
        {
            if (CheckDirectionForHook(direction))
            {
                if (Vector3.Distance(PlayerController.instance.transform.position, PlayerController.instance.Destination) < Mathf.Epsilon)
                {

                    
                    PlayerController.instance.isDead = true;

                    destination = transform.position  - direction;  //  Without Command Line 


                   
                    isHooked = true;

                   
                }
            }
        }
    }
    public bool CheckDirectionForHook(Vector3 direction)
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -direction, detectionRadiusForHook, BlockMask);
        // Debug.Log(hit);

      
        //  Debug.DrawRay(transform.position, -direction);
        if (hit.collider != null)
        {
            return false;
          
        }
        return true;
    }

    






}
