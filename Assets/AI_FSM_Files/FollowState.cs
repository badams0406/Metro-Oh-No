using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : BaseState
{
    float distanceFromPlayer;
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    public override void EnterState(Companion aiComp)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 7.5f;
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        
    }
    public override void UpdateState(Companion aiComp)
    {
        if (player != null)
        {
            //Debug.Log(distanceFromPlayer);
            distanceFromPlayer = Vector3.Distance(aiComp.transform.position, player.transform.position);

            animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

            if (distanceFromPlayer > 5)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
            }
        }
    }


}
