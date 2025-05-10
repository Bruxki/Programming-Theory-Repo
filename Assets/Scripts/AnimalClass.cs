using UnityEngine;

public class AnimalClass : MonoBehaviour
{
    protected float hunger = 0f;
    protected float health = 100f;
    protected float happiness = 100f;

    protected float speed = 1f;

    protected UnityEngine.AI.NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    protected virtual void Update()
    {
        hunger += Time.deltaTime * 0.5f;
        happiness -= Time.deltaTime * 0.2f;


        if (hunger >= 100f)
            hunger = 100f;

        if (happiness <= 0f)
            happiness = 0f;


        if (health <= 0f)
            Die();
    }

    protected virtual void Eat(float foodAmount)
    {
        hunger -= foodAmount;
        if (hunger < 0f) hunger = 0f;
    }

    protected virtual void Play(float funAmount)
    {
        happiness += funAmount;
        if (happiness > 100f) happiness = 100f;
    }




    protected virtual void Die()
    {
        Debug.Log("Animal died");
    }
}
