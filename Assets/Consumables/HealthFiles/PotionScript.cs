using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    Vector3 rotationDirection = new Vector3();

    public AntitodeScore antidoteScore;


    void Update()
    {
        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attackable") && other is CapsuleCollider)
        {
            antidoteScore.CollectPotion();
            FindObjectOfType<AudioManager>().Play("Potion");

            Destroy(gameObject);

        }
    }


}
