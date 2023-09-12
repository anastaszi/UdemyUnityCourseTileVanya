using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Player" && !wasCollected) { 
            wasCollected = true;
          GetComponent<AudioSource>().PlayOneShot(coinPickupSFX);
          AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
          FindObjectOfType<GameSession>().AddToScore(1);
          Destroy(gameObject);
      }    
    }
}
