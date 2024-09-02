using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.AI;


public class ZombieAttackPlayer : MonoBehaviour
{
  
    [SerializeField] float health = 3;

    [Header("Combat")]
    //[SerializeField] float attackCD = 3f;
    // [SerializeField] float attackRange = 1f;
    // [SerializeField] float aggroRange = 4f;

    GameObject randomTarget;
    List<GameObject> listOfAttackable;
  
    NavMeshAgent agent;
    Animator animator;
    // float timePassed;
    // float newDestinationCD = 0.5f;
    float speed = 5f;

    AttackState attackState;
    public KillCounter killCounter;
    
    
    bool destinationReached;

    private Vector3 startPosition;
    private Vector3 roamPosition;
    SphereCollider aggrooRange;
    bool attackPlayer = false;

    bool isRoaming;
    



    private void Start()
    {
        //player = GameObject.FindWithTag("Attackable");
        listOfAttackable = new List<GameObject>();
        listOfAttackable.Add(GameObject.Find("Player"));
        listOfAttackable.Add(GameObject.Find("AI Companion"));
        //closestAttackable = GameObject.Find("Player");
       


        animator = GetComponent<Animator>();   
        agent = GetComponent<NavMeshAgent>();

        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
       
        randomTarget = listOfAttackable[Random.Range(0, 2)];
    }

    private void Update()
    {
        if (health > 0)
        {
            if (!attackPlayer)
            {
                RoamingAround();
            }
            else
            {
                if (!ReferenceEquals(randomTarget, null)) 
                {
                    AttackMode();
                    
                }
            }

            //float reachedPosition = 2f;
            /* if (attackPlayer)
             {
                 agent.speed = 8;
                 agent.SetDestination(closestAttackable.transform.position);
             }*/


        }
    }

    private void AttackMode()
    {
        //Debug.Log(randomTarget.name);
        agent.speed = 5.5f;
        if (!ReferenceEquals(randomTarget, null) && randomTarget != null)
        {
            agent.SetDestination(randomTarget.transform.position);

           

            // if  (Vector3.Distance(randomTarget.transform.position, transform.position) <= attackRange)
            // {
            var targetRotation = Quaternion.LookRotation(randomTarget.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            animator.SetTrigger("attack");
            //}
        }
        else {
            RoamingAround();
        }
    }

    

    public void ChangeToAttackMode()
    {
        attackPlayer = true;
    }




    private void RoamingAround()
    {
        agent.SetDestination(roamPosition);

        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        /* if ((agent.velocity.magnitude / agent.speed) == 0)
         {
             roamPosition = GetRoamingPosition();
         }*/
        if (isRoaming) return;
        StartCoroutine(GetRoamPos());

    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + new Vector3(Random.Range(-1f, 1f), 0f ,Random.Range(-1f, 1f)).normalized * Random.Range(1f, 10f);
    }

    public IEnumerator GetRoamPos()
    {
        if (isRoaming) yield break;

        isRoaming = true;


        roamPosition = GetRoamingPosition();

        yield return new WaitForSeconds(3f);

        isRoaming = false;
    }

    /*
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
     */


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attackable") && other is CapsuleCollider)
        {
            Destroy(GetComponent<BoxCollider>());
            attackPlayer = true;
        }
    }

   /* private void OnTriggerExit(Collider other)
    {
        // roamPosition = // GetRoamingPosition();
    } */




    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        animator.SetTrigger("damage");

        FindObjectOfType<AudioManager>().PlayZombie();

        if (health <= 0)
        {
            killCounter.addKill();
            Destroy(GetComponent<CapsuleCollider>());
            agent.velocity = Vector3.zero;
            agent.isStopped = true;
            animator.SetTrigger("death");
            

            StartCoroutine(Die());
        }
    }

    public IEnumerator Die() 
    {
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);

    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }*/

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
       GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }

}

/*
 * if (health > 0)
        {
            foreach (var item in listOfAttackable)
            {
                if (timePassed >= attackCD)
            {
                if (Vector3.Distance(item.transform.position, transform.position) <= attackRange)
                {
                    animator.SetTrigger("attack");
                    timePassed = 0;
                }
            }

            timePassed += Time.deltaTime;
            

                if (newDestinationCD <= 0 && Vector3.Distance(item.transform.position, transform.position) <= aggroRange && Vector3.Distance(item.transform.position, transform.position) <= Vector3.Distance(closestAttackable.transform.position, transform.position))
                {
                    newDestinationCD = 0.5f;

                    agent.SetDestination(item.transform.position);
                    closestAttackable = item;
                }

                newDestinationCD -= Time.deltaTime;
               
                
                
                var targetRotation = Quaternion.LookRotation(closestAttackable.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime );
            }
           
        }
 */
