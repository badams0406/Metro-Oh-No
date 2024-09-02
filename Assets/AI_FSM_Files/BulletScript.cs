using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletScript : MonoBehaviour
{
    float damage = 2f;
    private Vector3 shootDir;
    GameObject zombie;
    [SerializeField] GameObject currentWeapon;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        // transform.eulerAngles = new Vector3(0, , 0);
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        float moveSpeed = 20f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie") && other is CapsuleCollider)
        {
            other.GetComponentInChildren<ZombieAttackPlayer>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (other is MeshCollider)
        {
            Destroy(gameObject);
        }
        
    }
}

