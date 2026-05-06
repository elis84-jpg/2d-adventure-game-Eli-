using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    // Talk action variable
    public InputAction talkAction;

  // Variables related to player character movement
  public InputAction MoveAction;
  Rigidbody2D rigidbody2d;
  Vector2 move;
  public float speed = 3.0f;


  // Variables related to the health system
  public int maxHealth = 5;
  int currentHealth;
  public int health { get { return currentHealth; }}


  // Variables related to temporary invincibility
  public float timeInvincible = 2.0f;
  bool isInvincible;
  float damageCooldown;

  // Variables related to animation
  Animator animator;
  Vector2 moveDirection = new Vector2(1,0);

  // Audio Source Variable declaration
  AudioSource audioSource;




  void Start()
  {
    //Storing Audio Source component referece in the variable
    audioSource = GetComponent<AudioSource>();

    talkAction.Enable();
  }

  // New Public function called PlaySound
  public void PlaySound(AudioClip clip)
  {
    audioSource.PlayOneShot(clip);
  }

  void FindFriend()
  {
    RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));

    if(hit.collider != null)
    {
        Debug.Log("Raycast has hit the object" + hit.collider.gameObject);
    }
  }


  // Start is called before the first frame update
  void Awake()
  {
    MoveAction.Enable();
    rigidbody2d = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    currentHealth = maxHealth;
  }
 
  // Update is called once per frame
  void Update()
  {
     move = MoveAction.ReadValue<Vector2>();

    if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y,0.0f))
    {
        moveDirection.Set(move.x, move.y);
        moveDirection.Normalize();
    }

    Debug.Log(move.x);

    if(move.x < 0)
    {
      gameObject.GetComponent<SpriteRenderer>().flipX = true; 
    }else if(move.x > 0)
    {
      gameObject.GetComponent<SpriteRenderer>().flipX = false; 
    }

    
    animator.SetBool("Grounded", true);
    animator.SetFloat("Speed", move.magnitude);

    if(move.magnitude > 0)
    {
      animator.SetInteger("AnimState", 1);
    }
    else
    {
      animator.SetInteger("AnimState", 0);
    }



     if (isInvincible)
       {
           damageCooldown -= Time.deltaTime;
           if (damageCooldown < 0)
           {
            isInvincible = false;
           }
               
       }

       if(Input.GetKeyDown(KeyCode.C))
       {
        Launch();
       }

       // call "FrindFriend" when correct input.
       if (Input.GetKeyDown(KeyCode.X))
       {
            FindFriend();
       }
   }

   public void Launch(Vector2 direction, float force)
   {
    rigidbody2d.AddForce(direction * force);
   }




 // FixedUpdate has the same call rate as the physics system
  void FixedUpdate()
  {
     Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
     rigidbody2d.MovePosition(position);
  }


  public void ChangeHealth (int amount)
  {
     if (amount < 0)
       {
           if (isInvincible)
           {
            return;
           }
           isInvincible = true;
           damageCooldown = timeInvincible;
           animator.SetTrigger("Hit");
       }


     currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
     UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
  }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.75f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(moveDirection, 300);

        animator.SetTrigger("Launch");
    }

}