using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    private Rigidbody2D rigidbody2D;
    // Declare a Move variable;
    Vector2 move;

    //Health system variables
    public int maxHealth = 5;
    int currentHealth = 1;

    //Character speed variable
    public float speed = 3.0f;
    
   // currentHealth = MaxHealth;
    void Start()
    {
        MoveAction.Enable();
        // set rigidbody2d variable equal to rigidbody2d component
        rigidbody2D = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();
        Debug.Log(move);
    }

    void FixedUpdate()
    {
        Vector2 position = (Vector2)rigidbody2D.position + move * speed * Time.deltaTime;
        rigidbody2D.MovePosition(position);
    }

    public void ChangeHealth (int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
