using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    void OnTriggerEnter2D(Collider2D other) {
      if (other.gameObject.tag == "Player") { 
        Debug.Log("Coin picked up" + GetComponent<AudioSource>());
          GetComponent<AudioSource>().PlayOneShot(coinPickupSFX);
          AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
                    FindObjectOfType<GameSession>().AddToScore(1);
          Destroy(gameObject);
      }    
    }
}
