using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int LevelNo;

    Animator SceneTransition;

#if UNITY_WEBGL
    [DllImport("__Internal")]
    private static extern void StartLevelEvent(int level);


#endif
    public Text Levelno;

    int level;
    void Start()
    {
        SceneTransition = GameObject.FindGameObjectWithTag("FadePanel").GetComponent<Animator>();

        LevelNo = transform.GetSiblingIndex() + 1;

        Levelno = transform.gameObject.GetComponentInChildren<Text>();

        level = transform.GetSiblingIndex() + 1;
        Levelno.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GotoLevel()
    {

        StartCoroutine(LoadLevel());
      startlevel();
    }

    private void startlevel()
    {

        StartLevelEvent(level);

    }

    IEnumerator LoadLevel()
    {

        SceneTransition.SetTrigger("end");
        AudioManager.instance.PlaySfx(1);
        yield return new WaitForSeconds(0.10f);
        SceneManager.LoadScene(LevelNo);
    }



    
}
