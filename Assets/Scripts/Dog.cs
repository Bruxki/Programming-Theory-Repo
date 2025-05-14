using UnityEngine;

public class Dog : AnimalClass
{
    public GameObject deadDog;
    public AudioClip dogSound;

    protected override void Start()
    {
        base.Start();
        deadAnimal = deadDog;
    }

    public override void MakeSound()
    {
        AudioManager.Instance.PlaySFX(dogSound);
    }

}
