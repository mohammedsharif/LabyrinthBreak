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

    private State state;
    private Vector3 startingPosition;
    private Vector3 roamingPosition;
    private float movingSpeed = 2.5f;

    private void Start() 
    {
        state = State.Idle;
        startingPosition = transform.position;
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
                MoveTo(roamingPosition);
                break;
            case State.Chasing:
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

    private void MoveTo(Vector3 position)
    {   
        float maxDistance = 0.5f;
        transform.LookAt(position);

        //move to the position if the position and current position are not same and there is no object in the way
        if(!Physics.Raycast(transform.position, transform.forward, maxDistance) && transform.position != roamingPosition)
        {
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
        else
        {
            state = State.Idle;
        }
    }
}
