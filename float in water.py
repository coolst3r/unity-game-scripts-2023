#Here is a script that will make an object float when touched by the player in an FPS game:

#c#

using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatHeight = 1f; // the height at which the object will float
    public float liftForce = 1f; // the force applied to the object to make it float
    public float damping = 0.1f; // the rate at which the object's velocity is dampened
    private bool isFloating; // whether or not the player is touching the object

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFloating = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFloating = false;
        }
    }

    private void FixedUpdate()
    {
        if (isFloating)
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            // calculate the distance between the object and the floatHeight
            float distance = transform.position.y - floatHeight;

            // apply an upward force proportional to the distance
            Vector3 lift = -Physics.gravity * (distance / liftForce);

            // dampen the object's velocity to prevent it from bouncing
            rb.velocity -= rb.velocity * damping * Time.fixedDeltaTime;

            // apply the lift force to the object
            rb.AddForce(lift, ForceMode.Acceleration);
        }
    }
}

#Attach this script to any object that you want to float when touched by the player. Create a new Trigger Collider object that covers the area you want to be considered "touchable". Assign the "Player" tag to this collider object. When the player touches the collider, the object will start floating.
