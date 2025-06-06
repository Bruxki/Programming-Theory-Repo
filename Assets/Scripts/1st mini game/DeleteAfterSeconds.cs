using System.Collections;
using UnityEngine;

public class DeleteAfterSeconds : MonoBehaviour
{
    public float seconds = 7f;

    IEnumerator Delete(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
