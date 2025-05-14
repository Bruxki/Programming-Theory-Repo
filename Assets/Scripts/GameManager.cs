using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuDuringPlay, pausedMenu, deadScreen, condition, quote;

    public AudioClip click;

    public AudioClip endingMusic;

    private bool paused = false;
    
    public gameStates state = gameStates.playing;
    public enum gameStates
    {
        playing, paused, deadScreen
    }
    private void Start()
    {
        quote.SetActive(false);
    }

    private void Update()
    {
        

    //Pause Menu
        if (!paused && Input.GetKeyDown(KeyCode.P) )
        { 
            state = gameStates.paused; 
            paused = true;
            PlaySound(click);
            Time.timeScale = 0f;

        }
        else if (paused && Input.GetKeyDown(KeyCode.P) )
        {
            state = gameStates.playing;
            paused = false;
            PlaySound(click);
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
                condition.SetActive(false);
                break;
        }
    }

    public void Die()
    {
        AudioManager.Instance.StopMusic();
        PlaySound(endingMusic);
        StartCoroutine(WaitingTillScreen(4f));
        StartCoroutine(WaitingTillQuote(13.2f));
    }

    public void PlaySound(AudioClip sound)
    {
        AudioManager.Instance.PlaySFX(sound);
    }
    public void StopAllMusic()
    {
        AudioManager.Instance.StopMusic();
    }



    public void QuitToTheMenu()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator WaitingTillScreen(float sec)
    {
        yield return new WaitForSeconds(sec);
        state = gameStates.deadScreen;

    }
    public IEnumerator WaitingTillQuote(float sec)
    {
        yield return new WaitForSeconds(sec);
        quote.SetActive(true);
    }
}
