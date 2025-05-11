using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//INHERITANCE + INCAPSULATION
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalClass : MonoBehaviour
{
    [Header("Stats")]
    protected float food = 100f;
    protected float health = 100f;
    protected float happiness = 100f;

    [Header("UI Bars")]
    public Slider healthBar;
    public Slider foodBar;
    public Slider happinessBar;


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

        if (healthBar == null)
            healthBar = GameObject.Find("HealthBar").GetComponent<Slider>();
        if (foodBar == null)
            foodBar = GameObject.Find("FoodBar").GetComponent<Slider>();
        if (happinessBar == null)
            happinessBar = GameObject.Find("HappinessBar").GetComponent<Slider>();



    }

    protected virtual void Update()
    {
        HandleNeeds();

        if ((currentState != AnimalState.SearchingFood && currentState != AnimalState.SearchingFun)
                && (currentState != AnimalState.FollowingPlayer && currentState != AnimalState.MovingToPoint))
        {
            //Change behaviour based on the stats
            if (food <= 30f && currentState != AnimalState.SearchingFood)
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

        UpdateUIBars();

        StateLogic();

    }


    protected virtual void HandleNeeds()
    {
        food -= Time.deltaTime * 5f;
        happiness -= Time.deltaTime * 5f;


        if (food <= 0f)
            food = 0f;

        if (happiness <= 0f)
            happiness = 0f;


        if (happiness <= 0f && food <= 0f)
            health -= Time.deltaTime * 5;


        if (health <= 0f)
            Die();
    }

    protected virtual void UpdateUIBars()
    {
        if (healthBar)
            healthBar.value = health;
        if (foodBar) 
            foodBar.value = food;
        if (happinessBar)
            happinessBar.value = happiness;
    }


    //------[ Pet Behavior ]-----
    protected virtual void Eat(float foodAmount)
    {
        food += foodAmount;
        if (food < 0f) food = 0f;
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
