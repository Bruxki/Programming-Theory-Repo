using System.Collections;
using UnityEngine;

//INHERITANCE & POLYMORPHISM
public class Penguin : AnimalClass
{
    public GameObject deadPenguin;
    public AudioClip penguinSound, deathSound, beeping, explosion;

    public Transform escapePoint, bombSite;
    public GameObject bombObject;

    public bool finalWarning;
    public bool onMission;

    private bool immortality;


    protected override void Start()
    {
        escapePoint = GameObject.Find("EscapeRoute").transform;
        bombObject = GameObject.Find("Bomb");
        bombSite = bombObject.transform;
        bombObject.SetActive(false);

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

        if (happiness <= 0 && !onMission)
        {
            onMission = true;
            StartCoroutine(ExecuteBombMission());
        }

        if (onMission)
        {
            float speed = agent.velocity.magnitude;
            immortality = true;
            return;
        }

        if (immortality)
        {
            base.health = 100f;
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

    private IEnumerator ExecuteBombMission()
    {
        agent.SetDestination(bombSite.position);
        currentState = AnimalState.MovingToPoint;

        while (agent.pathPending || agent.remainingDistance > 0.5)
        {
            yield return null;
        }

        //wait for 3 seconds
        yield return new WaitForSeconds(3f);

        bombObject.SetActive(true);

        //go to escape route
        agent.SetDestination(escapePoint.position);

        while (agent.pathPending || agent.remainingDistance > 0.5f)
        {
            yield return null;
        }

        gameManager.StopAllMusic();
        gameManager.penguinRevolt = true;

        PlantingBombSound();
        yield return new WaitForSeconds(1.8f);
        gameManager.state = GameManager.gameStates.explodedApt;
        ExplosionSound();
        yield return new WaitForSeconds(2f);
        gameManager.PenguinRevoltScene();
    }

    private void PlantingBombSound()
    {
        AudioManager.Instance.PlaySFX(beeping);
    }
    private void ExplosionSound()
    {
        AudioManager.Instance.PlaySFX(explosion);
    }



    protected override void Die()
    {
        base.Die();
        Debug.Log("Your pet has perished");
    }
}
