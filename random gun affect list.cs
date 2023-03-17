public class Bullet : MonoBehaviour {
public float speed = 10f;
public float damage = 10f;
public float knockback = 10f;
public float explosionForce = 1000f;
public float explosionRadius = 10f;
public float stunDuration = 1f;


private Rigidbody rb;

void Start() {
    rb = GetComponent<Rigidbody>();
    rb.velocity = transform.forward * speed;
}

void OnCollisionEnter(Collision collision) {
    // Apply damage to hit object
    var health = collision.gameObject.GetComponent<Health>();
    if (health != null) {
        health.TakeDamage(damage);
        // Decrease hit object's accuracy temporarily
        var aim = collision.gameObject.GetComponent<Aim>();
        if (aim != null) {
            aim.DecreaseAccuracy(0.5f);
        }
        // Apply a poison effect that gradually decreases the hit object's health over time
        StartCoroutine(ApplyPoisonEffect(5f, health));
    }

    // Apply knockback to hit object
    var hitRb = collision.gameObject.GetComponent<Rigidbody>();
    if (hitRb != null) {
        hitRb.AddForce(rb.velocity.normalized * knockback * 2f, ForceMode.Impulse);
    }

    // Apply explosion force to nearby objects
    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    foreach (Collider col in colliders) {
        var rb = col.GetComponent<Rigidbody>();
        if (rb != null) {
            // Randomize explosion force and direction for more chaotic effects
            rb.AddExplosionForce(explosionForce * Random.Range(0.5f, 1.5f), transform.position + Random.insideUnitSphere * 2f, explosionRadius * Random.Range(0.5f, 1.5f));
        }
    }

    // Stun hit object
    var enemy = collision.gameObject.GetComponent<Enemy>();
    if (enemy != null) {
        enemy.Stun(stunDuration * 2f);
        // Apply a confusion effect that temporarily reverses hit object's movement and aim
        StartCoroutine(ApplyConfusionEffect(3f, enemy));
    }

    // Apply a negative effect to the player that decreases their health and movement speed temporarily
    var player = collision.gameObject.GetComponent<Player>();
    if (player != null) {
        player.TakeDamage(damage * 2f);
        player.DecreaseMovementSpeed(0.5f);
    }

    // Destroy bullet object
    Destroy(gameObject);
}

IEnumerator ApplyPoisonEffect(float duration, Health health) {
    float elapsed = 0f;
    float tickInterval = 0.5f;
    while (elapsed < duration) {
        health.TakeDamage(damage * tickInterval / 2f);
        yield return new WaitForSeconds(tickInterval);
        elapsed += tickInterval;
    }
}

IEnumerator ApplyConfusionEffect(float duration, Enemy enemy) {
    float elapsed = 0f;
    while (elapsed < duration) {
        enemy.ReverseMovementDirection();
        enemy.ReverseAimDirection();
        yield return new WaitForSeconds(0.5f);
        elapsed += 0.5f;
    }
}

}

//Note that this modified script includes various negative effects that apply to the hit objects, including a poison effect that gradually decreases health over time, decreased accuracy, confusion that reverses movement and aim, and increased knockback. The //player also experiences negative effects including decreased health and movement speed. These changes should make the gameplay more chaotic and challenging.
