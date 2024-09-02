using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteHUD : MonoBehaviour
{
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    Vector3 rotationDirection = new Vector3();

    void Update()
    {
        transform.Rotate(rotateSpeed * rotationDirection * Time.deltaTime);
    }
}
