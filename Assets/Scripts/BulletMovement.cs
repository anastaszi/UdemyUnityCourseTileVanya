using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Rigidbody2D bulletRB;
    PlayerMovement player;
    float xSpeed;
    [SerializeField] float bulletSpeed = 10f;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other) {
       if (other.gameObject.tag == "Enemy") {
           Destroy(other.gameObject);
           Destroy(gameObject);
       }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (gameObject == null) {return;}
        Destroy(gameObject);
    }
}
