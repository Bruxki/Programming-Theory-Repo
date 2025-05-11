using UnityEngine;
using UnityEngine.AI;

//INHERITANCE + INCAPSULATION
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
    public float search = 8f;
    private float decisionTimer, searchTimer;

    protected float speed = 1f;

    protected Transform playerTarget;

    private GameObject currentTarget;

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

        if ((currentState != AnimalState.SearchingFood && currentState != AnimalState.SearchingFun)
                && (currentState != AnimalState.FollowingPlayer && currentState != AnimalState.MovingToPoint))
        {
            //Change behaviour based on the stats
            if (hunger >= 70f && currentState != AnimalState.SearchingFood)
            {
                currentState = AnimalState.SearchingFood;
                return;
            }
            else if (happiness <= 30f && currentState != AnimalState.SearchingFun)
            {
                currentState = AnimalState.SearchingFun;
                return;
            }
        }

        StateLogic();

    }


    protected virtual void HandleNeeds()
    {
        hunger += Time.deltaTime * 2f;
        happiness -= Time.deltaTime * 2f;


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
                agent.ResetPath();
                decisionTimer -= Time.deltaTime;
                if (decisionTimer < 0f)
                {
                    ChangeState(AnimalState.Wandering);
                }
                break;
            case AnimalState.Wandering:
                decisionTimer -= Time.deltaTime;

                if (decisionTimer <= 0f)
                {
                    ChangeState(AnimalState.Wandering);
                    break;
                }

                if (!agent.pathPending && agent.remainingDistance <= 0.5f)
                {
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

            case AnimalState.SearchingFood:
                if (currentTarget == null)
                {
                    currentTarget = GameObject.FindWithTag("Food");

                    if (currentTarget != null)
                        agent.SetDestination(currentTarget.transform.position);
                }
                break;

            case AnimalState.SearchingFun:
                if (currentTarget == null)
                {

                    currentTarget = GameObject.FindWithTag("Fun");

                    if (currentTarget != null)
                        agent.SetDestination(currentTarget.transform.position);
                }
                break;

        }
    }


    //State Handler
    protected virtual void ChangeState(AnimalState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case AnimalState.Idle:
                decisionTimer = decisionTime;
                break;

            case AnimalState.Wandering:
                decisionTimer = decisionTime;
                Vector3 wanderTarget = transform.position + Random.insideUnitSphere * wanderRadius;
                wanderTarget.y = transform.position.y;
                agent.SetDestination(wanderTarget);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Eat(50f);
            Destroy(collision.gameObject);
            if (currentState == AnimalState.SearchingFood)
            {
                ChangeState(AnimalState.Idle);
            }
        }
        if (collision.gameObject.CompareTag("Fun"))
        {
            Play(50f);
            Destroy(collision.gameObject);
            if (currentState == AnimalState.SearchingFun)
            {
                ChangeState(AnimalState.Idle);
            }
        }
    }



    protected virtual void Die()
    {
        Debug.Log("Animal died");
    }
}
