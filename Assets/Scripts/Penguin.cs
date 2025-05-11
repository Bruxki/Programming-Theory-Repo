using UnityEngine;

//INHERITANCE
public class Penguin : AnimalClass
{
    public GameObject deadPenguin;

    protected override void Start()
    {
       base.Start();
        deadAnimal = deadPenguin;
        Debug.Log("initialization");
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("Your pet has perished");
    }
}
