using UnityEngine;
using TMPro;

public class DropdownMenu : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.onValueChanged.AddListener(ValueChanged);

        int pet = PlayerPrefs.GetInt("pet_index", 0);
        dropdown.value = pet;

    }

    private void ValueChanged(int index)
    {
        PlayerPrefs.SetInt("pet_index", index);
    }
}
