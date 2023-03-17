using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 2f;
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;
    public float knockbackForce = 500f;
    public float knockbackDuration = 1f;
    public float damage = 10f;
    public float burnDuration = 5f;
    public float slowDuration = 5f;
    public float slowAmount = 0.5f;
    public float confusionDuration = 5f;
    public float spinDuration = 5f;
    public float spinForce = 500f;

    public GameObject explosionEffect;
    public GameObject impactEffect;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
        rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to player
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);

            // Apply knockback to player
            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * knockbackForce, ForceMode.Impulse);
            StartCoroutine(ApplyNegativeEffect(collision.gameObject, knockbackDuration, "Knockback"));

            // Apply burn to player
            StartCoroutine(ApplyNegativeEffect(collision.gameObject, burnDuration, "Burn"));

            // Apply slow to player
            collision.gameObject.GetComponent<PlayerMovement>().speed *= slowAmount;
            StartCoroutine(ApplyNegativeEffect(collision.gameObject, slowDuration, "Slow"));

            // Apply confusion to player
            StartCoroutine(ApplyNegativeEffect(collision.gameObject, confusionDuration, "Confusion"));

            // Apply spin to player
            collision.gameObject.GetComponent<Rigidbody>().AddTorque(transform.forward * spinForce, ForceMode.Impulse);
            StartCoroutine(ApplyNegativeEffect(collision.gameObject, spinDuration, "Spin"));
        }
        else
        {
            // Apply explosion force to surrounding objects
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider col in colliders)
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 1f, ForceMode.Impulse);
                }
            }

            // Spawn explosion effect
            Instantiate(explosionEffect, transform.position, transform.rotation);

            // Spawn impact effect
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        // Destroy bullet
        Destroy(gameObject);
    }

    IEnumerator ApplyNegativeEffect(GameObject target, float duration, string effectName)
    {
        // Apply negative effect to target
        target.GetComponent<PlayerMovement>().enabled = false;
        target.GetComponent<Shooter>().enabled = false;
        target.GetComponentInChildren<MeshRenderer>().enabled = false;

        // Wait for duration of effect
        yield return new WaitForSeconds(duration);

        // Remove negative effect from target
        target.GetComponent<PlayerMovement>().enabled = true;
        target.GetComponent<Shooter>().enabled = true;
        target.GetComponentInChildren<MeshRenderer>().enabled = true;

        // Show effect ended message
        Debug.Log(target.name + " " + effectName + " ended.");
    }
}
