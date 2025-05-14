using UnityEngine;

public class PetClickController : MonoBehaviour
{
    public AnimalClass pet;

    private bool assigned = false;

    private void Update()
    {
        if (pet == null && !assigned)
        { 
            pet = GameObject.FindWithTag("Pet").GetComponent<AnimalClass>(); 
            assigned = true;
        
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                pet.SendToPoint(hit.point);
            }
        }
    }
}
