using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform hallway, bedroom, diningroom;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3f;

    public AudioClip click;

    enum CamTargetPos
    {
        hallway, bedroom, diningRoom
    }
    CamTargetPos camPos = CamTargetPos.bedroom;


    private void Update()
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

    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(click);
    }


    public void ChangeCammeraPosition(Transform target)
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 0.015f);
    }
}
