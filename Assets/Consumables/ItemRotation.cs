using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    Vector3 rotationDirection = new Vector3();

    public HealthSystem healthBarPlayer;
    public HealthSystem healthBarAIC;

    void Update()
    { 
        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
    }
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attackable") && other is CapsuleCollider)
        {
            healthBarPlayer.Medkit();
            healthBarAIC.Medkit();
            FindObjectOfType<AudioManager>().Play("Heal");
            Destroy(gameObject);

        }
    }
}
