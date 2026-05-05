using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : MonoBehaviour
{
   // Declaring new public AudioClip variable
   public AudioClip collectedClip;

   void OnTriggerEnter2D(Collider2D other)
   {
      PlayerController controller = other.GetComponent<PlayerController>();

      if(controller != null && controller.health < controller.maxHealth)
      {
         // Calling the PlaySound Function onthe PlayerController to play sound
         controller.PlaySound(collectedClip);
         
         controller.ChangeHealth(1);
         Destroy(gameObject);
      }
   }
}