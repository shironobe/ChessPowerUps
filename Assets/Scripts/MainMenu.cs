using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.InteropServices;
public class MainMenu : MonoBehaviour
{
    public int SceneNo;

    public bool muted;

    public Image Audio;
    public Sprite On, Off;

    public Animator SceneTransition;

    public int LevelScreen;

//#if UNITY_WEBGL


//    [DllImport("__Internal")]
//    private static extern void StartGameEvent();


//#endif

    public Text Screenwidth;
    void Start()
    {
        SceneTransition = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //  PlayerPrefs.SetInt("LevelUnlock",24);

          //  Debug.Log(Screen.width);
          //  Screenwidth.text = Screen.width.ToString();
        }
        

    }


    public void PlayButton()
    {
        StartCoroutine(LoadLevelScene());
       
        startevent();

        

    }
    private void startevent()
    {

     // StartGameEvent();

    }


    
    public void GotoScene()
    {
        StartCoroutine(LoadScene());
    }
   // private static extern void StartGameEvent();
    public void SoundOnOff()
    {

        if (!muted)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            if (muted)
            {
                muted = false;
                AudioListener.pause = false;
            }
        }
      //save();
     updateIcon();
    }
    private void updateIcon()
    {
        if (!muted)
        {
            Audio.sprite = On;

        }
        else
        {
            if (muted)
            {
                Audio.sprite = Off;
            }

        }
    }

    IEnumerator LoadScene()
    {
        SceneTransition.SetTrigger("end");
        AudioManager.instance.PlaySfx(5);
        yield return new WaitForSeconds(0.10f);
        SceneManager.LoadScene(SceneNo);
    }

    IEnumerator LoadLevelScene()
    {
        SceneTransition.SetTrigger("end");
       AudioManager.instance.PlaySfx(5);
        yield return new WaitForSeconds(0.10f);
        SceneManager.LoadScene(LevelScreen);
    }
}
