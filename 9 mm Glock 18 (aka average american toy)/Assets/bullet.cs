using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 1400f; // Speed at which the object moves forward
   
    private void Start()
    {
      
    }

    private void Update()
    {
        // Move the object forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
