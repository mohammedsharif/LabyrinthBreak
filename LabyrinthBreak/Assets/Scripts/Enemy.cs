using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving,
        Chasing,
    }

    [SerializeField] private LayerMask layerMask;

    private State state;
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float movingSpeed = 2.5f;

    private int health;
    private int maxHealth;
    private float maxAttackingRange = 10f;
    private float maxChasingRange = 15f;
    private int attackPower = 10;
    private float attackTimer;
    private float maxAttackTimer = 1f;

    private void Start() 
    {
        state = State.Idle;
        startingPosition = transform.position;
        attackTimer = maxAttackTimer;
    }

    private void Update() 
    {
        switch(state)
        {
            case State.Idle:
                roamingPosition = GetRoamingPosition();
                state = State.Moving;
                break;
            case State.Moving:
                MoveTo(roamingPosition, movingSpeed);
                if(Vector3.Distance(transform.position, Player.Instance.transform.position) <= maxChasingRange)
                {
                    state = State.Chasing;
                }
                break;
            case State.Chasing:
                attackTimer -= Time.deltaTime;
                MoveTo(Player.Instance.transform.position, movingSpeed, maxChasingRange);
                if(Vector3.Distance(transform.position, Player.Instance.transform.position) <= maxAttackingRange)
                {
                    if(attackTimer <= 0f)
                    {
                        Attack();
                        attackTimer = maxAttackTimer;
                    }

                }
                else
                {
                    state = State.Idle;
                }
                break;
        }   
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

    private Vector3 GetRoamingPosition()
    {
        //generate a random roaming position
        Vector3 roamingPosition;
        roamingPosition = new Vector3(Random.Range(startingPosition.x+5,startingPosition.x-5), startingPosition.y, 
            Random.Range(startingPosition.z+5,startingPosition.z-5));

        return roamingPosition;
    }

    private void MoveTo(Vector3 targetPosition, float speed, float range = 1f, float maxDistance = 1f)
    {   
        transform.LookAt(targetPosition);

        //move to the position if the position and current position are not same and there is no object in the way except player
        if(!Physics.Raycast(transform.position, transform.forward, range, layerMask) && (targetPosition - transform.position).magnitude > maxDistance)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            state = State.Idle;
        }
    }

    private void SetHealth(int health)
    {
        this.health = health;
    }

    private int GetHealth()
    {
        return health;
    }

    private int GetMaxHealth()
    {
        return maxHealth;
    }
}
