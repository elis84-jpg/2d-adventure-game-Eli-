using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{
    // Creating a Rigidbody2D variable
    Rigidbody2D rigidbody2d;

   void Awake()
    {
       rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // A custom method that accepts a Vector2 for direction and a decimal number for force
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
