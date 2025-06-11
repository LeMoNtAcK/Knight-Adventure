using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using KnightAdventures;
using KnightAdventures.Utils;

public class EnemyAI : MonoBehaviour


{
    private enum State
    {
        Idle,
        Chasing,
        Roaming
    }

    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;

   

    private NavMeshAgent navMeshAgent;




    private State state;
    private float roamingTime;
    private Vector3 roamPosition;
    private Vector3 startingPosition;


    private void Awake()
    {
        startingPosition = transform.position;
        state = State.Roaming;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        // Каждый враг случайно решает, Idle он или Roaming на старте
        //state = (Random.value > 0.5f) ? State.Roaming : State.Idle;
        roamingTime = Random.Range(0f, roamingTimerMax);
        navMeshAgent.speed = Random.Range(1.6f, 2.4f);

    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    // 3. Каждый раз новое время до следующей смены
                    roamingTime = Random.Range(roamingTimerMax * 0.7f, roamingTimerMax * 1.3f);
                }
                break;
        }
    }

    //private void LateUpdate()
    //{
    //    Vector3 pos = transform.position;
    //    pos.z = 0f;
    //    transform.position = pos;

    //    // Сброс вращения по X и Z — чтобы Slime не "ложился" и не вращался:      ВРОДЕ И ТАК РАБОТАЕТ!
    //    Vector3 euler = transform.eulerAngles;
    //    euler.x = 0f;
    //    euler.z = 0f;
    //    transform.eulerAngles = euler;
    //}

    private void Roaming()
    {
        
        roamPosition = GetRoamingPosition();
        NavMeshHit hit;
        if (NavMesh.SamplePosition(roamPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            navMeshAgent.SetDestination(hit.position);
        }
        else
        {
            //Roaming(); // не нашли подходящую точку → остаёмся на месте или пробуем заново
        }
        UpdateFacingDirection();
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void UpdateFacingDirection()
    {
        Vector3 dir = navMeshAgent.destination - transform.position;

        if (dir.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        else if (dir.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
    }
} 
