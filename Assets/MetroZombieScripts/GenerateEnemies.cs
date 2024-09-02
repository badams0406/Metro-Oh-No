using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    public int xMin, xMax; 
    public int zMin, zMax;
    public GameObject zombees;
    public int xPos;
    public int zPos;
    public int enemyCount;
    public int totalEnemyCount;
    // bool spawn = false;


    IEnumerator EnemyDrop()
    {
        while (enemyCount < totalEnemyCount)
        {
            // xPos = Random.Range(90, 100);
            //zPos = Random.Range(-25, -4);

            xPos = Random.Range(xMin, xMax);
            zPos = Random.Range(zMin, zMax);

            Instantiate(zombees, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(.1f);
            enemyCount++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attackable") && other is CapsuleCollider)
        {
            StartCoroutine(EnemyDrop());
            Destroy(GetComponent<BoxCollider>());
            
        }
    }


}
