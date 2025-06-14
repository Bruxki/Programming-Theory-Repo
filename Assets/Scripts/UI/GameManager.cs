using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuDuringPlay, pausedMenu, deadScreen, condition, quote, hint, explodedApt;

    public AudioClip click;

    public AudioClip endingMusic;

    private bool paused = false;

    public bool penguinRevolt;
    
    public gameStates state = gameStates.playing;
    public enum gameStates
    {
        playing, paused, deadScreen, explodedApt
    }
    private void Start()
    {
        quote.SetActive(false);
        explodedApt.SetActive(false);
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
            case gameStates.explodedApt:
                menuDuringPlay.SetActive(false);
                pausedMenu.SetActive(false);
                deadScreen.SetActive(false);
                condition.SetActive(false);
                explodedApt.SetActive(true);
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
    public void DisableHint()
    {
        PlaySound(click);
        hint.SetActive(false);
    }


    public void PenguinRevoltScene()
    {
        SceneManager.LoadScene("HideAndSeek");
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
