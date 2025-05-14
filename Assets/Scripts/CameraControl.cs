using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//ABSTRACTION
public class CameraControl : MonoBehaviour
{
    GameManager gameManager;


    [Header("Locations")]
    public Transform hallway, bedroom, diningroom;

    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;
    public AudioClip click, errorSound, dingSound;

    [Header("Shop UI")]
    public TMP_Dropdown itemDropdown;
    public GameObject[] toys, foods, meds;

    public int[] itemPrices;

    [Header("Player Money")]
    public int playerMoney = 100;
    public TMP_Text moneyText;



    enum CamTargetPos
    {
        hallway, bedroom, diningRoom
    }
    CamTargetPos camPos = CamTargetPos.bedroom;

    private void Start()
    {
        UpdateMoneyUI();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }



    private void Update()
    {
        HandleCameraMovement();

        if (Input.GetMouseButtonDown(0))
        {
            SpawnItem();
        }
    }





    //Camera movement

    public void HandleCameraMovement()
    {
        switch (camPos)
        {
            case CamTargetPos.hallway:
                ChangeCammeraPosition(hallway);
                break;
            case CamTargetPos.bedroom:
                ChangeCammeraPosition(bedroom);
                break;
            case CamTargetPos.diningRoom:
                ChangeCammeraPosition(diningroom);
                break;
        }
    }

    public void MoveToHallway()
    {
        PlayClick();
        camPos = CamTargetPos.hallway;
    }

    public void MoveToBedroom() 
    {
        PlayClick();
        camPos = CamTargetPos.bedroom;
    }

    public void MoveToDiningroom() 
    {
        PlayClick();
        camPos = CamTargetPos.diningRoom;
    }

    //Sound
    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(click);
    }
    public void PlayError()
    {
        AudioManager.Instance.PlaySFX(errorSound);
    }
    public void PlayDing()
    {
        AudioManager.Instance.PlaySFX(dingSound);
    }

    //MoneyHandler
    public void UpdateMoneyUI()
    {
        moneyText.text = "Balance: $" + playerMoney.ToString();
    }
    
    //spawn item handler
    public void SpawnItem()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (gameManager.state == GameManager.gameStates.paused || gameManager.state == GameManager.gameStates.deadScreen)
            return;

        int selectedItem = itemDropdown.value;
        int itemCost = itemPrices[selectedItem];

        if (playerMoney >= itemCost)
        {
            Vector3 spawnPos = GetWorldClickPosition();
            if (spawnPos != Vector3.zero)
            {
                switch (selectedItem)
                {
                    case 0:
                    Instantiate(toys[Random.Range(0, toys.Length)], spawnPos, Quaternion.identity);
                    break;
                    case 1:
                    Instantiate(foods[Random.Range(0, foods.Length)], spawnPos, Quaternion.identity);
                    break;
                    case 2:
                    Instantiate(meds[Random.Range(0, meds.Length)], spawnPos, Quaternion.identity);
                    break;
                }

                    

                playerMoney -= itemCost;
                UpdateMoneyUI();
                PlayDing();
            }

        }
        else
        {
            PlayError();
        }
    }



    //Abstraction for camera movement
    public void ChangeCammeraPosition(Transform target)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 0.015f);
    }


    private Vector3 GetWorldClickPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit))
        {
            return hit.point;
        }

        return Vector3.zero;
    }
}
