using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuDuringPlay, pausedMenu, deadScreen;

    public AudioClip click;

    private bool paused = false;
    
    gameStates state = gameStates.playing;
    private enum gameStates
    {
        playing, paused, deadScreen
    }


    private void Update()
    {
        

    //Pause Menu
        if (!paused && Input.GetKeyDown(KeyCode.P) )
        { 
            state = gameStates.paused; 
            paused = true;
            PlayClick();
            Time.timeScale = 0f;

        }
        else if (paused && Input.GetKeyDown(KeyCode.P) )
        {
            state = gameStates.playing;
            paused = false;
            PlayClick();
            Time.timeScale = 1f;
        }

    //Game states managing part
        switch (state)
        {
            case gameStates.playing:
                menuDuringPlay.SetActive(true);
                pausedMenu.SetActive(false);
                deadScreen.SetActive(false);
                break;
            case gameStates.paused:
                menuDuringPlay.SetActive(false);
                pausedMenu.SetActive(true);
                deadScreen.SetActive(false);
                break;
            case gameStates.deadScreen:
                menuDuringPlay.SetActive(false);
                pausedMenu.SetActive(false);
                deadScreen.SetActive(true);
                break;
        }
    }
    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(click);
    }


    public void QuitToTheMenu()
    {
        SceneManager.LoadScene(0);
    }
}
