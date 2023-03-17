public class Jetpack : MonoBehaviour
{
    public float thrust = 10f;
    public float fuel = 100f;
    public float maxFuel = 100f;
    public float fuelConsumptionRate = 10f;
    public ParticleSystem jetpackParticles;
    public AudioSource jetpackAudio;
    
    private bool isJetpacking = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            StartJetpacking();
        }
        else
        {
            StopJetpacking();
        }
    }

    private void FixedUpdate()
    {
        if (isJetpacking)
        {
            ConsumeFuel();
            ApplyThrust();
        }
    }

    private void ConsumeFuel()
    {
        fuel -= fuelConsumptionRate * Time.fixedDeltaTime;
        if (fuel < 0)
        {
            fuel = 0;
            StopJetpacking();
        }
    }

    private void ApplyThrust()
    {
        rb.AddForce(Vector2.up * thrust, ForceMode2D.Force);
        if (!jetpackAudio.isPlaying)
        {
            jetpackAudio.Play();
            jetpackParticles.Play();
        }
    }

    private void StartJetpacking()
    {
        isJetpacking = true;
    }

    private void StopJetpacking()
    {
        isJetpacking = false;
        jetpackAudio.Stop();
        jetpackParticles.Stop();
    }
}
