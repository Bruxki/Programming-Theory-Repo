using System;
using UnityEngine;

public class SpawnPet : MonoBehaviour
{
    public GameObject[] pets;

    int pet;

    private void Start()
    {
        
        pet = PlayerPrefs.GetInt("pet_index", 0);
        Instantiate(pets[pet]);
    }
}
