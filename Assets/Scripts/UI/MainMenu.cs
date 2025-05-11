using UnityEngine;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
    public GameObject initialMenu, optionsMenu, choiceMenu;

    public AudioClip music;
    public AudioClip[] click;

    enum MenuOptions
    {
        Initial, Options, Choice
    }

    MenuOptions menu;


    private void Start()
    {
        menu = MenuOptions.Initial;
        PlayMusic();
    }

    private void Update()
    {
        //state machine
        if (menu == MenuOptions.Initial)
        {
            initialMenu.SetActive(true);
            optionsMenu.SetActive(false);
            choiceMenu.SetActive(false);
        }
        else if (menu == MenuOptions.Options)
        {
            initialMenu.SetActive(false);
            optionsMenu.SetActive(true);
            choiceMenu.SetActive(false);
        }
        else if (menu == MenuOptions.Choice)
        {
            initialMenu.SetActive(false);
            optionsMenu.SetActive(false);
            choiceMenu.SetActive(true);
        }
    }


    public void BackToTheMenu()
    {
        menu = MenuOptions.Initial;
    }
    public void GoToChoosing()
    {
        menu = MenuOptions.Choice;
    }
    public void GoToOptions()
    {
        menu = MenuOptions.Options;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void PlayMusic()
    {
        AudioManager.Instance.PlayMusic(music);
    }
    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(click[Random.Range(0,click.Length)]);
    }


    public void QuitGame()
    {
        //save score

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }
}
