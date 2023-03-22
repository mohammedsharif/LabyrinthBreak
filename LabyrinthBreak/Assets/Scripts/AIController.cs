using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField]private NavMeshAgent agent;

    public enum State
    {
        Idle,
        Moving,
        Chasing,
    }

    private State state;
    
    [SerializeField] private LayerMask playerMask;

    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float movingSpeed = 2.5f;
    private float stoppingDistance = 1f;
    private float maxAttackingRange = 10f;
    private float maxChasingRange = 15f;

    private int health = 100;
    private int maxHealth;
    private int attackPower = 10;
    private float attackTimer;
    private float maxAttackTimer = 1f;

    private void Player_OnAttackTouched(object sender, Player.EventArgsOnAttackTouched e)
    {
        SetHealth(health - e.attackPower);   
    }

    private void Start() 
    {
        state = State.Idle;
        startingPosition = transform.position;
        agent.stoppingDistance = stoppingDistance;
        agent.speed = movingSpeed;
        attackTimer = maxAttackTimer;
        maxHealth = health;

        Player.Instance.OnAttackTouched += Player_OnAttackTouched; 
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Idle:
                roamingPosition = GetRoamingPosition();
                state = State.Moving;
                break;
            case State.Moving:
                agent.destination = roamingPosition;
                if(agent.remainingDistance <= agent.stoppingDistance)
                {
                    state = State.Idle;
                }
                if(Vector3.Distance(transform.position, Player.Instance.transform.position) <= maxChasingRange)
                {
                    state = State.Chasing;
                }
                break;
            case State.Chasing:
                agent.destination = Player.Instance.transform.position;
                if(agent.remainingDistance > maxChasingRange)
                {
                    state = State.Idle;
                }
                if(agent.remainingDistance <= maxAttackingRange)
                {
                    attackTimer -= Time.deltaTime;
                    if(attackTimer <= 0f)
                    {
                        Attack();
                        attackTimer = maxAttackTimer;
                    }
                }
                break;
        }

        if(health <= 0)
            Destroy(gameObject);
    }

    private Vector3 GetRoamingPosition()
    {
        //generate a random roaming position
        Vector3 roamingPosition;
        roamingPosition = new Vector3(Random.Range(startingPosition.x+5,startingPosition.x-5), startingPosition.y, 
            Random.Range(startingPosition.z+5,startingPosition.z-5));

        return roamingPosition;
    }

    private void Attack()
    {
        if(Physics.Raycast(transform.position, transform.forward, maxAttackingRange, playerMask))
        {
            Player.Instance.SetHealth(Player.Instance.GetHealth() - attackPower);
        }
    }

    public void SetHealth(int health)
    {
        if(health < 0)
        {
            this.health =  0;
        }
        else
        {
            this.health = health;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
