using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Companion: MonoBehaviour
{
    [Header("Controls")]
    public float aiSpeed = 5.0f;
    public float aiRotationSpeed = 5f;
    public float aiAttackCD = 2f;
    public float aiAttackRange = 2f;
    public float aiAggroRange = 8f;

    public List<Collider> zombies;


    BaseState currentState;
    FollowState followState;
    AttackState attackState;
    RoamState roamState;
  

   

    private void Start()
    {
      
        zombies = new List<Collider>();
        
        followState = gameObject.AddComponent<FollowState>();
        attackState = gameObject.GetComponent<AttackState>();
        roamState = gameObject.GetComponent<RoamState>();

        currentState = followState;
        currentState.EnterState(this);
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            // Debug.Log("Right mouse button was clicked");
            
            currentState = attackState;
            
            currentState.EnterState(this);
         
            //attackState.GetZombieHitList(zombies);
            
            
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
           // Debug.Log("F was pressed");
                currentState = followState;
                currentState.EnterState(this);
          
            
        }
        attackState.GetZombieHitList(zombies);
        currentState.UpdateState(this);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie") && other is CapsuleCollider) 
        {
           // Debug.Log("Zombie in RANGE");
            zombies.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie") && other is CapsuleCollider) 
        {
            zombies.Remove(other);
        }
    }

    public void BackToFollow()
    {
        currentState = followState;
        currentState.EnterState(this);
    }



}
