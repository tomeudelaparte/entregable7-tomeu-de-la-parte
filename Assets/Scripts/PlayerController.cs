using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;

    private Rigidbody playerRigidbody;
    private AudioSource playerAudioSource, cameraAudioSource;
    public AudioClip BlipAudio, BoingAudio, BoomAudio;
    public ParticleSystem explosionParticles, fireworksParticles;

    private float forceValue = 10f;
    private float yLimit = 14f;

    private float musicVolume = 0.2f;
    private float effectsVolume = 1f;

    private int totalMoney;

    void Start()
    {
        gameOver = false;

        playerRigidbody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();

        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        explosionParticles = Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
        fireworksParticles = Instantiate(fireworksParticles, transform.position, fireworksParticles.transform.rotation);
    }

    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRigidbody.AddForce(Vector3.up * forceValue, ForceMode.Impulse);

                playerAudioSource.PlayOneShot(BoingAudio, effectsVolume);
            }

            if (transform.position.y > yLimit)
            {
                transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
                playerRigidbody.velocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameOver)
        {
            if (other.gameObject.CompareTag("Money"))
            {
                totalMoney++;
                playerAudioSource.PlayOneShot(BlipAudio, effectsVolume);

                fireworksParticles.transform.position = other.gameObject.transform.position;
                fireworksParticles.Play();

                Destroy(other.gameObject);

                Debug.Log($"Has obtenido " + totalMoney + " monedas en total.");
            }

            if (other.gameObject.CompareTag("Bomb"))
            {
                playerAudioSource.PlayOneShot(BoomAudio, effectsVolume);

                explosionParticles.transform.position = other.gameObject.transform.position;
                explosionParticles.Play();

                Destroy(other.gameObject);

                GameOver();
            }
        }

        // Al tocar el suelo explota (Un tributo a Michael Bay), se destruye el player y termina el juego 
        if (other.gameObject.CompareTag("Ground"))
        {
            explosionParticles.transform.position = other.gameObject.transform.position;
            explosionParticles.Play();

            Destroy(gameObject);

            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;

        cameraAudioSource.volume = musicVolume;

        Debug.Log($"GAME OVER: has conseguido " + totalMoney + " monedas.");
    }
}
