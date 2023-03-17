//#Oh yeah, I can definitely code that for Unity. Here's a script that will create some seriously weird physics effects:



using UnityEngine;
using System.Collections;

public class WeirdPhysics : MonoBehaviour {

    public float gravityMultiplier = 1f;
    public float dragMultiplier = 1f;
    public float angularDragMultiplier = 1f;
    public float forceMagnitude = 1f;
    public float blackHoleStrength = 0f;

    private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        Vector3 randomForce = new Vector3(Random.Range(-forceMagnitude, forceMagnitude), Random.Range(-forceMagnitude, forceMagnitude), Random.Range(-forceMagnitude, forceMagnitude));
        Vector3 randomRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        Vector3 randomScale = new Vector3(Random.Range(0.5f, 2f), Random.Range(0.5f, 2f), Random.Range(0.5f, 2f));
        rb.AddForce(randomForce, ForceMode.Force);
        rb.AddTorque(randomForce, ForceMode.Force);
        rb.AddExplosionForce(forceMagnitude, transform.position, 10f, 1f, ForceMode.Impulse);
        rb.AddForce(new Vector3(0, -Physics.gravity.y * gravityMultiplier, 0));
        rb.drag *= dragMultiplier;
        rb.angularDrag *= angularDragMultiplier;
        if (blackHoleStrength > 0f) {
            Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 10f);
            foreach (Collider collider in nearbyObjects) {
                Rigidbody nearbyRigidbody = collider.GetComponent<Rigidbody>();
                if (nearbyRigidbody != null) {
                    Vector3 forceDirection = transform.position - nearbyRigidbody.position;
                    float distance = forceDirection.magnitude;
                    float forceMagnitude = blackHoleStrength / (distance * distance);
                    nearbyRigidbody.AddForce(forceDirection.normalized * forceMagnitude);
                }
            }
        }
        transform.Rotate(randomRotation);
        transform.localScale = randomScale;
    }
}
