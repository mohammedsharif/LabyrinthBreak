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
    
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float movingSpeed = 2.5f;
    private float stoppingDistance = 1f;
    private float maxAttackingRange = 10f;
    private float maxChasingRange = 15f;

    private int health;
    private int attackPower = 10;
    private float attackTimer;
    private float maxAttackTimer = 1f;

    private void Start() 
    {
        state = State.Idle;
        startingPosition = transform.position;
        agent.stoppingDistance = stoppingDistance;
        agent.speed = movingSpeed;
        attackTimer = maxAttackTimer;
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
        Player.Instance.SetHealth(Player.Instance.GetHealth() - attackPower);
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, maxAttackingRange))
        {
            if(raycastHit.transform.TryGetComponent(out Player player))
            {
                Player.Instance.SetHealth(player.GetHealth() - attackPower);
            }
        }
    }
}
