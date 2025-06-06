using UnityEngine;

public class Cat : AnimalClass
{
    public GameObject deadCat;
    public AudioClip catSound;

    protected override void Start()
    {
        base.Start();
        deadAnimal = deadCat;
    }
    public override void MakeSound()
    {
        AudioManager.Instance.PlaySFX(catSound);
    }
}
