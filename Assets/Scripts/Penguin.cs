using UnityEngine;

//INHERITANCE
public class Penguin : AnimalClass
{

    protected override void Start()
    {
       base.Start();
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
