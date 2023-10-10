using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f, PowerUpSpeed;
    public LayerMask BlockMask = 0;
    public float detectionRadius = 2f, detectionRadiusRook;

    public Vector3 Destination;
    public static PlayerController instance;

    public bool canMove;

   
    Animator SceneTransition;

    #if UNITY_WEBGL
    //[DllImport("__Internal")]
    //private static extern void StartLevelEvent(int level);

    //[DllImport("__Internal")]
    //private static extern void ReplayEvent(int Level);

#endif

    public bool inContactWithPlayer;



    public bool isMoving;

    bool blockPushed;
    public List<Vector3> lastPos = new List<Vector3>();

    // public int moveplayerNo = 0;
    public SpriteRenderer sr;



   public bool isDead;

  //  public Transform N, S;
    public bool PlayerMoved;

    public Vector3 CurrentPos, LastPos;

     public bool DoorOpen;

    bool Won;


  
    [SerializeField] GameObject Door;

    [SerializeField] Text MovesLeft;

    bool sfxPlayed;

    public GameObject RestartPopup;

    public GameObject[] PowerUps;

    public Transform[] PowerUpPosition;

    public int PowerUpCount;

    bool usePowerUp;

    public enum moveStates {None, RightRook,LeftRook,DownRook, UpRook, RightUpBishop,RightDownBishop,LeftUpBishop,LeftDownBishop}

    public moveStates currentmoveState;


    Animator anim;
    private void Awake()
    {
        instance = this;
    }

   
    void Start()
    {
        anim = GetComponent<Animator>();
      
        Destination = transform.position;
        BoxCollider2D box2d = GetComponent<BoxCollider2D>();
    
        canMove = true;
     
        sr = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame

   
    void Update()

    {





        MovesLeft.text = Manager.instance.MovesLeft.ToString();



        if (!isDead)
        {
            isMoving = false;
          
            if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon )
            {


               // anim.SetBool("isMoving", false);

                // SnakeMove.instance.snakeMove();
                //Debug.Log(Input.GetAxisRaw("Horizontal"));
                //Debug.Log(Input.GetAxisRaw("Vertical"));
                //Debug.Log(CheckDirection(Vector3.right));

                //if (Input.GetAxisRaw("Horizontal") > 0)
                //{
                if (Manager.instance.MovesLeft > 0)
                {
                    CurrentPos = transform.position;
                    if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                    {

                        if (CheckDirection(Vector3.right))
                        {


                               sr.flipX = false;

                            PlayerMoved = true;
                            Destination = transform.position + Vector3.right;

                            //     Icommand command = new PlayerMoveCommand(this.gameObject, Vector3.right);
                            // CommandInvoker.instance.AddCommand(command);
                            Manager.instance.MovesLeft--;


                            AudioManager.instance.PlaySfx(3);
                            //

                        }
                        //}
                    }


                    else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                    {
                        if (CheckDirection(Vector3.left))
                        {
                            AudioManager.instance.PlaySfx(3);
                            PlayerMoved = true;
                            Destination = transform.position + Vector3.left;
                            Manager.instance.MovesLeft--;
                            sr.flipX = true;
                            // Destination = mPlayer.transform.position;

                            //     Icommand command = new PlayerMoveCommand(this.gameObject, Vector3.left);
                            //  CommandInvoker.instance.AddCommand(command);






                        }
                    }


                    else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                    {

                        if (CheckDirection(Vector3.up))
                        {
                            PlayerMoved = true;
                            AudioManager.instance.PlaySfx(3);
                            Destination = transform.position + Vector3.up;
                            Manager.instance.MovesLeft--;

                            // Icommand command = new PlayerMoveCommand(this.gameObject, Vector3.up);
                            //  CommandInvoker.instance.AddCommand(command);



                        }
                        //}
                    }


                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                    {
                        if (CheckDirection(Vector3.down))
                        {
                            AudioManager.instance.PlaySfx(3);
                            PlayerMoved = true;
                            Destination = transform.position + Vector3.down;
                            Manager.instance.MovesLeft--;



                        }
                        // }
                    }
                }
                    //}

                
            }
            else
            {
                //   SnakeMove.instance.snakeMove();
                if (!usePowerUp)
                {
                  //  anim.SetBool("isMoving", true);
                    isMoving = true;
                    transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
                }
                else
                {
                   // anim.SetBool("isMoving", true);
                    isMoving = true;
                    transform.position = Vector3.MoveTowards(transform.position, Destination, PowerUpSpeed * Time.deltaTime);

                }





                //  lastPos.Add(transform.position);


            }

        }
        //}
        if (Input.GetKey(KeyCode.R))
        {
            ReplayLevel();
        }

        if (usePowerUp)
        {

            switch (currentmoveState)
            {
                case moveStates.RightRook:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(Vector3.right))
                        {
                           
                            sr.flipX = false;
                            Destination = transform.position + Vector3.right;
                        }
                    }
                    break;

                case moveStates.LeftRook:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(Vector3.left))
                        {
                            sr.flipX = true;
                            Destination = transform.position + Vector3.left;
                        }
                    }
                    break;

                case moveStates.UpRook:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(Vector3.up))
                        {
                            sr.flipX = true;
                            Destination = transform.position + Vector3.up;
                        }
                    }
                    break;

                case moveStates.DownRook:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(Vector3.down))
                        {
                            sr.flipX = true;
                            Destination = transform.position + Vector3.down;
                        }
                    }
                    break;
                case moveStates.RightUpBishop:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(new Vector3(1f, 1f, 0)))
                        {
                            sr.flipX = false;
                            Destination = transform.position + new Vector3(1f, 1f, 0);
                        }
                    }
                    break;

                case moveStates.RightDownBishop:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(new Vector3(1f, -1f, 0)))
                        {
                            sr.flipX = false;
                            Destination = transform.position + new Vector3(1f, -1f, 0);
                        }
                    }
                    break;
                case moveStates.LeftDownBishop:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(new Vector3(-1f, -1f, 0)))
                        {
                            sr.flipX = true;
                            Destination = transform.position + new Vector3(-1f, -1f, 0);
                        }
                    }
                    break;
                case moveStates.LeftUpBishop:
                    if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
                    {
                        if (CheckDirectionRook(new Vector3(-1f, 1f, 0)))
                        {
                            sr.flipX = true;
                            Destination = transform.position + new Vector3(-1f, 1f, 0);
                        }
                    }
                    break;

            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

          //  UsePowerUp();
        }

        if (Input.GetKey(KeyCode.P))
        {

            if (Vector3.Distance(transform.position, Destination) < Mathf.Epsilon)
            {
                if (CheckDirectionRook(new Vector3(1f, 1f, 0)))
                {
                    Destination = transform.position + new Vector3(1f, 1f,0);
                }
            }
        }

    }

    void moveRookRight()
    {
        if (CheckDirection(Vector3.right))
        {
            Destination = transform.position + Vector3.right * 20;
        }
    }
   public void UsePowerUp(string PowerUpName)
    {
        if (PowerUpCount > 0 && !usePowerUp)
        {
            AudioManager.instance.PlaySfx(1);
            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "RightRook")
                {
                    currentmoveState = moveStates.RightRook;
                  //  Destroy(PowerUps[PowerUpCount - 1], 0);
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }

            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "LeftRook")
                {
                    currentmoveState = moveStates.LeftRook;
                  
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }

            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "UpRook")
                {
                    currentmoveState = moveStates.UpRook;
                  
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }

            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "DownRook")
                {
                    currentmoveState = moveStates.DownRook;
                  
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }



            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "RightUpBishop")
                {
                    currentmoveState = moveStates.RightUpBishop;
                   
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }

            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "RightDownBishop")
                {
                    currentmoveState = moveStates.RightDownBishop;
                   
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }

            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "LeftUpBishop")
                {
                    currentmoveState = moveStates.LeftUpBishop;
                  
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }

            if (PowerUpCount > 0 && !usePowerUp)
            {
                if (PowerUpName == "LeftDownBishop")
                {
                    currentmoveState = moveStates.LeftDownBishop;
                   
                    usePowerUp = true;
                    PowerUpCount--;
                }
            }
        }
             //   Destination = transform.position + new Vector3(1f, 1f, 0);
            
        

    }

    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


    }

    public void MovePlayer(Vector3 direction)
    {

       


        // Icommand command = new PlayerMoveCommand(this.gameObject, direction);
        //  CommandInvoker.AddCommand(command);

        Destination = transform.position + direction;
       // AudioManager.instance.PlaySfx(1);


    }

    public void UnMovePlayer(Vector3 direction)
    {




        // Icommand command = new PlayerMoveCommand(this.gameObject, direction);
        //  CommandInvoker.AddCommand(command);

        Destination = transform.position - direction;
        // AudioManager.instance.PlaySfx(1);


    }


   

    private bool CheckDirection(Vector3 direction) { 
  //  
  //  RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);

       
        Debug.DrawRay(transform.position, direction);

       // RaycastHit2D hit1 = Physics2D.Raycast(hit1pos.transform.position, direction, detectionRadius, BlockMask);
      //  Debug.DrawRay(hit1pos.transform.position, direction);

        if ( hit)
        {
          
            if (hit.collider.gameObject.CompareTag("Pushable") )
            {

                

                PushableBlock pushableBlock = hit.collider.GetComponent<PushableBlock>();
            


                if (!pushableBlock)
                    return false;


              pushableBlock.Push(direction, speed);
             
                // Destination = transform.position + direction;

                return false;
            }



          //  Debug.Log(hit.collider.gameObject.name);
            // Debug.Log("Here");

            return false;
        }
       
     
        return true;
       
    }

    private bool CheckDistance(Vector3 direction)
    {
        //  
        //  RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadiusRook, BlockMask);


        Debug.DrawRay(transform.position, direction);

        // RaycastHit2D hit1 = Physics2D.Raycast(hit1pos.transform.position, direction, detectionRadius, BlockMask);
        //  Debug.DrawRay(hit1pos.transform.position, direction);

        if (hit)
        {

            Debug.Log(Vector3.Distance(transform.position, hit.transform.position));


            //  Debug.Log(hit.collider.gameObject.name);
            // Debug.Log("Here");

            return false;
        }


        return true;

    }
    private bool CheckDirectionRook(Vector3 direction)
    {
        //  
        //  RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadiusRook, BlockMask);


        Debug.DrawRay(transform.position, direction);

        // RaycastHit2D hit1 = Physics2D.Raycast(hit1pos.transform.position, direction, detectionRadius, BlockMask);
        //  Debug.DrawRay(hit1pos.transform.position, direction);

        if (hit)
        {

            if (hit.collider.gameObject.CompareTag("Pushable"))
            {



                PushableBlock pushableBlock = hit.collider.GetComponent<PushableBlock>();

                AudioManager.instance.PlaySfx(4);

                if (!pushableBlock)
                    return false;

                if (usePowerUp)
                {
                    pushableBlock.PushForce(direction);
                    usePowerUp = false;
                    return false;

                }
                pushableBlock.Push(direction, speed);
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
    private bool CheckDirectionWon(Vector3 direction)
    {
       

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, BlockMask);


        Debug.DrawRay(transform.position, direction);


        if (hit)
        {
           
            if (hit.collider.gameObject.CompareTag("Won"))
            {
                Won = true;
                if (Manager.instance.MovesLeft <= 0 && !Won)
                {
                    RestartPopup.SetActive(true);
                }
                return true;
            }

            // Debug.Log("Here");

            return false;
        }
        //if (hit1)
        //{
        //    string tag = hit1.transform.tag;

        //    if (hit1.collider.gameObject.CompareTag("Pushable"))
        //    {
        //        PushableBlock pushableBlock = hit1.collider.GetComponent<PushableBlock>();


        //        if (!pushableBlock)
        //            return false;


        //        pushableBlock.Push(direction, speed);



        //    }
        //    return false;
        //}

        return false;

    }

    public void Right()
    {
        if (CheckDirection(Vector3.right))
        {
            Destination = transform.position + Vector3.right;
          //  AudioManager.instance.PlaySfx(1);
        }
    }
    public void Left()
    {
        if (CheckDirection(Vector3.left))
        {
            Destination = transform.position + Vector3.left;
          //  AudioManager.instance.PlaySfx(1);
        }
    }


    public void Up()
    {
        if (CheckDirection(Vector3.up))
        {
            Destination = transform.position + Vector3.up;
           // AudioManager.instance.PlaySfx(1);
        }
    }

    public void Down()
    {

        if (CheckDirection(Vector3.down))
        {
            Destination = transform.position + Vector3.down;
            //AudioManager.instance.PlaySfx(1);
        }
    }

   


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("PowerUp"))
        {
           // PowerUps[PowerUpCount] = other.gameObject;

            AddItem(other.gameObject);
        

            other.gameObject.GetComponent<PowerUp>().isCollected = true;
             PowerUpCount++;
        }
        if (other.gameObject.CompareTag("ExitDoor"))
        {


            anim.SetTrigger("Won");
            StartCoroutine(NextLevel());
        }





    }
    public void AddItem(GameObject item)
    {
        bool ticker = true;
        for (int i = 0; i < PowerUps.Length; i++)
        {
            if (ticker && PowerUps[i] == null) {
                {
                    PowerUps[i] = item;
                    item.GetComponent<Transform>().position  = PowerUpPosition[i].position;
                    ticker = false;
                }
            }
        }
    }

    IEnumerator NextLevel()
    {

        // SceneTransition.SetTrigger("end");
        // AudioManager.instance.PlaySfx(5);
        if (PlayerPrefs.GetInt("LevelUnlock") < SceneManager.GetActiveScene().buildIndex + 1)
        {
           // PlayerPrefs.SetInt("LevelUnlock", SceneManager.GetActiveScene().buildIndex + 1);
        }
        startlevel();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
    private void startlevel()
    {

      //  StartLevelEvent(SceneManager.GetActiveScene().buildIndex + 1);

    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    
  

}
