using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 10;
    
    public HealthBar healthBar;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(health);
        
    }

    // Update is called once per frame
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.SetHealth(health);
        animator.SetTrigger("damage");

        if (health <= 0)
        {
            Die();
        }
    }

    public void Medkit()
    {
        health = 10f;
        healthBar.SetHealth(health);
    }

    void Die() 
    {
        Destroy(this.gameObject);
    }
}
