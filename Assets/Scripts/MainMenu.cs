using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject initialMenu, optionsMenu, choiceMenu;


    enum MenuOptions
    {
        Initial, Options, Choice
    }

    MenuOptions menu;







    private void Start()
    {
        menu = MenuOptions.Initial;
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

}
