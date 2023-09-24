using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CoolMathAds : MonoBehaviour
{

	public static CoolMathAds instance;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}

	public void PauseGame()
	{
		//All the code inside PauseGame will be called when the Ad event will begin.

		//Below line will pause the game excluding the IEnumerator with WaitForSecondsRealtime and the animators with Update Mode set to Unscalled Time.
		Time.timeScale = 0f;
		if (!AudioManager.instance.muted)
		{
			AudioManager.instance.Sfx[0].volume = 0;
		}

		//If you do not want to pause the game, call your custom code to mute or stop the music of the game.
	}

	public void ResumeGame()
	{
		Time.timeScale = 1.0f;
		if (!AudioManager.instance.muted)
		{
			AudioManager.instance.Sfx[0].volume = 0.7f;
		}
		//If you used custom code to mute music in PauseGame function, call the code here to unmute or play the music.
	}

	//Below code call the cmgAdBreak event.
	public void InitiateAds()
	{
		Application.ExternalCall("triggerAdBreak");
	}

}
