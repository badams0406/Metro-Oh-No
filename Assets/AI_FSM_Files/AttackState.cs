using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    [SerializeField] GameObject currentWeapon;
    [SerializeField] private Transform bullet;
    float speed = 15f;
    NavMeshAgent agent;
    Animator animator;
    GameObject player;

    List<Collider> zombies;
    private bool isShooting;
    //private bool attackStateWasEnabled;

    public float attackRange = 10f;
    float dangerZone;
    float STAYAWAY = 2f;

    

    public override void EnterState(Companion aiComp)
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5;
        zombies = new List<Collider>();
        //attackStateWasEnabled = true;
        player = GameObject.Find("Player");
    }
    public override void UpdateState(Companion aiComp)
    {
        // animator.SetFloat("speed", 0);
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
        Collider zombie = ClosestZombie();



        if (zombie != null && !ReferenceEquals(zombie, null))
        {

            dangerZone = Vector3.Distance(agent.transform.position, zombie.transform.position);


            if (dangerZone < STAYAWAY)
            {
                Vector3 runAwayPosition = transform.position - (zombie.transform.position - agent.transform.position).normalized * 5f;
                // Debug.Log(dangerZone);

                // Set the destination for the NavMeshAgent to run away
                animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
                agent.SetDestination(runAwayPosition);

                if (Vector3.Distance(agent.transform.position, runAwayPosition) != 0)
                {

                }

            }


            var targetRotation = Quaternion.LookRotation(zombie.gameObject.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            animator.SetTrigger("shoot");
            //Transform bulletTransform = Instantiate(bullet, this.transform.position , Quaternion.identity);
            // Vector3 shootDir = (transform.forward).normalized;

            if (isShooting) return;
            StartCoroutine(Shoot());
            //bulletTransform.GetComponent<BulletScript>().Setup(shootDir);


        }
        else
        {
            agent.SetDestination(player.transform.position);
        }


        
    }

    public IEnumerator Shoot()
   {
      if(isShooting) yield break;

        isShooting = true;
       
        Transform bulletTransform = Instantiate(bullet,  new Vector3(currentWeapon.transform.position.x - .50f, currentWeapon.transform.position.y ,currentWeapon.transform.position.z), Quaternion.identity);
        Vector3 shootDir = (transform.forward);
        bulletTransform.GetComponent<BulletScript>().Setup(shootDir);

        yield return new WaitForSeconds(.55f);

        isShooting = false;
    }

    


   public void GetZombieHitList(List<Collider> listOfZombies)
    {
        if (listOfZombies != null)
            zombies = listOfZombies;
        else
            return;
    }

    private Collider ClosestZombie()
    {
        if (zombies != null)
        {
            Collider closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (Collider enemyCollider in zombies)
            {
                if (enemyCollider != null)
                {
                    float distance = Vector3.Distance(agent.transform.position, enemyCollider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemyCollider;
                    }
                }
            }
            
           return closestEnemy;
        }
        return null;
        
    } 

}
