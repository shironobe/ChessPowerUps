using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class BanBlock : MonoBehaviour
{

    public static BanBlock instance;
    public  BoxCollider2D block2d;

    public GameObject[] Bans;

    SpriteRenderer sr;

    public Sprite on, off;

    //[DllImport("__Internal")]
    //private static extern void StartLevelEvent(int level);
    bool won;
    Animator SceneTransition;


    bool open;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
       sr=GetComponent<SpriteRenderer>();
        // SceneTransition = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
        open = true;
    }

    // Update is called once per frame
    void Update()
    {
        Bans = GameObject.FindGameObjectsWithTag("Ban");

        if (sr.sprite == off && !won)
        {
           // StartCoroutine(NextLevel());
            if (PlayerPrefs.GetInt("LevelUnlock") < SceneManager.GetActiveScene().buildIndex + 1)
            {
                PlayerPrefs.SetInt("LevelUnlock", SceneManager.GetActiveScene().buildIndex + 1);
            }
         //   startlevel();
           
            won = true;

        }

        if (sr.sprite == off && open)
        {

         
            open = false;
        }

       // CheckBans();
    }


   

    
    public void CheckBans()
    {

        for (int i = 0; i < Bans.Length; i++)
        {
          if( Bans[i].GetComponent<Ban>().isRock)
            {
              //  AudioManager.instance.PlaySfx(5);



            }




        }

        foreach (GameObject Ban in Bans)
        {

            if (Ban.GetComponent<Ban>().isRock)
            {

                block2d.enabled = false;
                sr.sprite = off;
              
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


            }
            else
            {
                block2d.enabled = true;
                sr.sprite = on;
                break;
            }
        }

    }





}
