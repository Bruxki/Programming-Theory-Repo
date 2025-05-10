using UnityEngine;
using UnityEngine.AI;

//INHERITANCE
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalClass : MonoBehaviour
{
    [Header("Stats")]
    protected float hunger = 0f;
    protected float health = 100f;
    protected float happiness = 100f;

    [Header("AI")]
    public AnimalState currentState = AnimalState.Idle;
    public float wanderRadius = 5f;
    public float decisionTime = 3f;
    private float decisionTimer;

    protected float speed = 1f;

    protected Transform playerTarget;

    protected UnityEngine.AI.NavMeshAgent agent;

    //using state machine for different animal behaviour
    public enum AnimalState
    {
        Idle,
        Wandering,
        SearchingFood,
        SearchingFun,
        MovingToPoint,
        FollowingPlayer
    }



    protected virtual void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        decisionTimer = decisionTime;
    }

    protected virtual void Update()
    {
        HandleNeeds();
        StateLogic();

    }


    protected virtual void HandleNeeds()
    {
        hunger += Time.deltaTime * 0.5f;
        happiness -= Time.deltaTime * 0.3f;


        if (hunger >= 100f)
            hunger = 100f;

        if (happiness <= 0f)
            happiness = 0f;


        if (health <= 0f)
            Die();
    }


    //------[ Pet Behavior ]-----
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

    protected virtual void Move(Vector3 target)
    {
        agent.SetDestination(target);
        ChangeState(AnimalState.MovingToPoint);
    }

    //state Logic

    protected virtual void StateLogic()
    {
        switch (currentState)
        {
            case AnimalState.Idle:
                decisionTimer -= Time.deltaTime;
                if (decisionTimer < 0f)
                {
                    ChangeState(AnimalState.Wandering);
                }
                break;
            case AnimalState.Wandering:
                if (!agent.pathPending && agent.remainingDistance <= 0.5f)
                {
                    decisionTimer -= Time.deltaTime;
                    ChangeState(AnimalState.Idle);
                }
                break;
            case AnimalState.MovingToPoint:
                if (!agent.pathPending && agent.remainingDistance <= 0.5f)
                {
                    ChangeState(AnimalState.Idle);
                }
                break;
            case AnimalState.FollowingPlayer:
                if (playerTarget!= null)
                {
                    agent.SetDestination(playerTarget.position);
                }
                break;

            //looking for food, fun, etc.
        }
    }


    //State Handler
    protected virtual void ChangeState(AnimalState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case AnimalState.Wandering:
                Vector3 wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
                wanderTarget.y = transform.position.y;
                agent.SetDestination(wanderTarget);
                break;

            //case AnimalState.SearchingFood , fun, etc



        }
    }


    protected virtual void Die()
    {
        Debug.Log("Animal died");
    }
}
