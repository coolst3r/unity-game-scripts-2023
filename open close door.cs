using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90.0f; // How far the door should rotate when opened
    public float openSpeed = 2.0f; // How quickly the door should open
    public AudioClip openSound; // Sound to play when door opens
    public AudioClip closeSound; // Sound to play when door closes

    private bool isOpen = false; // Whether or not the door is currently open
    private Quaternion startRotation; // Starting rotation of the door
    private Quaternion endRotation; // Ending rotation of the door
    private AudioSource audioSource; // Audio source for playing sounds

    void Start()
    {
        startRotation = transform.rotation; // Store starting rotation of the door
        endRotation = startRotation * Quaternion.Euler(0.0f, openAngle, 0.0f); // Calculate the ending rotation of the door
        audioSource = GetComponent<AudioSource>(); // Get the audio source component attached to the door
    }

    void Update()
    {
        if (isOpen) // If the door is open, rotate it towards the end rotation
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, Time.deltaTime * openSpeed);
        }
        else // If the door is closed, rotate it back towards the starting rotation
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.deltaTime * openSpeed);
        }
    }

    void Interact()
    {
        isOpen = !isOpen; // Toggle the open state of the door
        if (isOpen) // If the door is now open, play the open sound
        {
            audioSource.PlayOneShot(openSound);
        }
        else // If the door is now closed, play the close sound
        {
            audioSource.PlayOneShot(closeSound);
        }
    }
}
