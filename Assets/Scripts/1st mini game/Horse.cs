using UnityEngine;

public class Horse : AnimalClass
{
    public GameObject deadHorse;
    public AudioClip horseSound;

    protected override void Start()
    {
        base.Start();
        deadAnimal = deadHorse;
    }

    public override void MakeSound()
    {
        AudioManager.Instance.PlaySFX(horseSound);
    }
}
