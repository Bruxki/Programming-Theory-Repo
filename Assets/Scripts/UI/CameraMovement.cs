using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    private int petNumber;
    bool isChoosing = false;

    public Transform doggie, kitty, pengwin, horsie, menu, options;
    
    public enum CamPosition
    {
        kitty, horsie, pengwin, doggie, menu, options
    }

  
    CamPosition camPos = CamPosition.menu;

    private void Start()
    {
        Time.timeScale = 1f;
        camPos = CamPosition.menu;
    }


    void Update()
    {
        //checking the pet index
        petNumber = PlayerPrefs.GetInt("pet_index");

        if (isChoosing)
        {
            switch (petNumber)
            {
                case 0:
                    camPos = CamPosition.kitty;
                    break;
                case 1:
                    camPos = CamPosition.horsie;
                    break;
                case 2:
                    camPos = CamPosition.pengwin;
                    break;
                case 3:
                    camPos = CamPosition.doggie;
                    break;
            }
        }
        //changing camera position
        if (camPos == CamPosition.doggie)
        {
            ChangeCameraPosition(doggie);
        }
        else if (camPos == CamPosition.kitty)
        {
            ChangeCameraPosition(kitty);
        }
        else if (camPos == CamPosition.pengwin)
        {
            ChangeCameraPosition(pengwin);
        }
        else if (camPos == CamPosition.horsie)
        {
            ChangeCameraPosition(horsie);
        }
        else if (camPos == CamPosition.menu)
        {
            ChangeCameraPosition(menu);
        }
        else if (camPos == CamPosition.options)
        {
            ChangeCameraPosition(options);
        }
    }

    public void ReturnToTheMenu()
    {
        camPos = CamPosition.menu;
        isChoosing = false;
    }
    public void GoToOptions()
    {
        camPos = CamPosition.options;
        isChoosing = false;
    }

    //ABSTRACTION
    public void ChangeCameraPosition(Transform target)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 0.05f);
    }

    public void ChoosingFighter()
    {
        isChoosing = true;
    }
}
