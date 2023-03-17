using UnityEngine;
using System.Collections;

public class WeirdPhysics : MonoBehaviour {


public float gravityMultiplier = 1.5f;
public float dragMultiplier = 2f;
public float angularDragMultiplier = 0.5f;
public float forceMagnitude = 10f;
public float blackHoleStrength = 100f;

private Rigidbody rb;

void Start () {
    rb = GetComponent<Rigidbody>();
}

void FixedUpdate () {
    Vector3 randomForce = new Vector3(Random.Range(-2f*forceMagnitude, 2f*forceMagnitude), Random.Range(-2f*forceMagnitude, 2f*forceMagnitude), Random.Range(-2f*forceMagnitude, 2f*forceMagnitude));
    Vector3 randomRotation = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f), Random.Range(-360f, 360f));
    Vector3 randomScale = new Vector3(Random.Range(0.1f, 5f), Random.Range(0.1f, 5f), Random.Range(0.1f, 5f));
    rb.AddForce(randomForce, ForceMode.Force);
    rb.AddTorque(randomForce, ForceMode.Force);
    rb.AddExplosionForce(forceMagnitude, transform.position, 10f, 0.5f, ForceMode.Impulse);
    rb.AddForce(new Vector3(0, -Physics.gravity.y * gravityMultiplier, 0));
    rb.drag *= dragMultiplier;
    rb.angularDrag *= angularDragMultiplier;
    if (blackHoleStrength > 0f) {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 50f);
        foreach (Collider collider in nearbyObjects) {
            Rigidbody nearbyRigidbody = collider.GetComponent<Rigidbody>();
            if (nearbyRigidbody != null) {
                Vector3 forceDirection = transform.position - nearbyRigidbody.position;
                float distance = forceDirection.magnitude;
                float forceMagnitude = blackHoleStrength / (Mathf.Sqrt(distance) * Mathf.Sqrt(distance));
                nearbyRigidbody.AddForce(forceDirection.normalized * forceMagnitude);
            }
        }
    }
    transform.Rotate(randomRotation);
    transform.localScale = randomScale;
}

}

