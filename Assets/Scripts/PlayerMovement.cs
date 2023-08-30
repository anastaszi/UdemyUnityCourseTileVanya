using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    //Rigidbody2D playerRB;
    // Start is called before the first frame update
    void Start()
    {
        // playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();   
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void Run() {
       // rb.velocity = new Vector2(moveInput.x * 5f, rb.velocity.y);
    //    rb.velocity = moveInput;
    }
}
