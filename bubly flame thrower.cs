#a unity script it should on button f shoot bubble particle that act like a flame thrower that makes things float and play a song and then make you fly ina weird floaty way in game


using UnityEngine;
using System.Collections;

public class BubbleParticleScript : MonoBehaviour {

    public ParticleSystem bubbleParticles;
    public AudioClip song;
    public float bubbleForce = 10.0f;
    public float playerFlyForce = 100.0f;

    private bool isPlayingSong = false;

    void Update () {
        if (Input.GetKeyDown(KeyCode.F)) {
            bubbleParticles.Play();
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bubbleForce, ForceMode.Impulse);
            if (!isPlayingSong) {
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = song;
                audio.Play();
                isPlayingSong = true;
            }
            rb.AddForce(Vector3.up * playerFlyForce, ForceMode.Impulse);
        }
    }
}
