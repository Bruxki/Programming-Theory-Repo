using UnityEngine;

public class PetClickController : MonoBehaviour
{
    public AnimalClass pet;
    public GameObject indicator;

    private Vector3 currentTarget;
    private bool movingToTarget = false;

    private bool assigned = false;

    private bool revolt;

    

    private void Start()
    {
        indicator.SetActive(false);
    }

    private void Update()
    {
        if (pet == null && !assigned)
        { 
            pet = GameObject.FindWithTag("Pet").GetComponent<AnimalClass>(); 
            assigned = true;
        
        }

        Penguin penguin = pet as Penguin;

        if (penguin.onMission && !revolt)
        {
            revolt = true;
        }

        //Pet-Click-Control. stops working when the revolt has begun (only for penguin tho)
        if (!revolt)
        {
            if (Input.GetMouseButtonDown(1) && pet != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    currentTarget = hit.point;
                    movingToTarget = true;

                    indicator.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
                    indicator.SetActive(true);

                    pet.SendToPoint(hit.point);
                }
            }
            if (movingToTarget && pet != null)
            {
                float distance = Vector3.Distance(pet.transform.position, currentTarget);

                if (distance < 0.2f)
                {
                    indicator.SetActive(false);
                    movingToTarget = false;
                }
            }
        }
    }
}
