using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelNoAssign : MonoBehaviour
{
    // Start is called before the first frame update
    public Text LevelNo;
    void Start()
    {
        LevelNo = GetComponent<Text>();

        LevelNo.text = SceneManager.GetActiveScene().buildIndex.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        LevelNo.text = SceneManager.GetActiveScene().buildIndex.ToString();
    }
}
