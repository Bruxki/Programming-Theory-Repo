using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menuDuringPlay, pausedMenu, deadScreen;


    private bool paused = false;
    
    gameStates state = gameStates.playing;
    private enum gameStates
    {
        playing, paused, deadScreen
    }


    private void Update()
    {
        

    //Pause Menu
        if (!paused && Input.GetKeyDown(KeyCode.P))
        { 
            state = gameStates.paused; 
            paused = true;
        }
        else
        {
            state = gameStates.playing;
            paused = false;
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
}
