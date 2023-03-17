using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public float jumpForce;
    private int extraJumpsValue = 2;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && extraJumpsValue > 0)
        {
            GetComponent<Rigidbody>().velocity = Vector2.up * jumpForce;
            extraJumpsValue--;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && extraJumpsValue == 0 && GetComponent<CharacterController>().isGrounded == true)
        {
            GetComponent<Rigidbody>().velocity = Vector2.up * jumpForce;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        extraJumpsValue = 2;
    }
}
