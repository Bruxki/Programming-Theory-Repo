using UnityEngine;

//INHERITANCE & POLYMORPHISM
public class Penguin : AnimalClass
{
    public GameObject deadPenguin;
    public AudioClip penguinSound, deathSound;

    public bool finalWarning;

    protected override void Start()
    {
       base.Start();
        deadAnimal = deadPenguin;
        Debug.Log("initialization");
    }

    protected override void Update()
    {
        if (health < 10 && !finalWarning)
        {
            AudioManager.Instance.PlaySFX(deathSound);
            finalWarning = true;
        }

        base.Update();
    }

    public override void MakeSound()
    {
        AudioManager.Instance.PlaySFX(penguinSound);
    }


    //example of polymorphism
    protected override void Move(Vector3 target)
    {
        if (food <= 30f)
        {
            agent.speed = 2.5f;
        }
        else
        {
            agent.speed = 0.5f;
        }

        base.Move(target);
    }



    protected override void Die()
    {
        base.Die();
        Debug.Log("Your pet has perished");
    }
}
