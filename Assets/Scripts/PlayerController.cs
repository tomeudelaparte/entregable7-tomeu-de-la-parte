using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variable que indica si el juego ha finalizado
    public bool gameOver;

    // Posición inicial del player
    private Vector3 defaultPosition = new Vector3(0, 8, 0);

    // Variables para almacenar componentes del player u otros
    private Rigidbody playerRigidbody;
    private AudioSource playerAudioSource, cameraAudioSource;

    // Variables para almacenar prefabs de efectos de sonido y partículas
    public AudioClip BlipAudio, BoingAudio, BoomAudio;
    public ParticleSystem explosionParticles, fireworksParticles;

    // Valor de la fuerza de impulso y el límite superior en Y
    private float forceValue = 10f;
    private float yLimit = 14f;

    // Nivel de volúmen de la música y efectos de sonido
    private float musicVolume = 0.2f;
    private float effectsVolume = 1f;

    // Monedas obtenidas en total
    private int totalMoney;

    // Al iniciar el juego
    void Start()
    {
        // Indica que el juego no ha finalizado
        gameOver = false;

        // Posiciona al player en la posición predeterminada
        transform.position = defaultPosition;

        // Obtiene las componentes del Player (Rigidbody) (AudioSource)
        playerRigidbody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();

        // Obtiene la componente de la Main Camera (AudioSource)
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        // Instancia las partículas y las guarda en variables para utilizarlas más adelante
        explosionParticles = Instantiate(explosionParticles, transform.position, explosionParticles.transform.rotation);
        fireworksParticles = Instantiate(fireworksParticles, transform.position, fireworksParticles.transform.rotation);
    }

    // A cada frame del juego
    void Update()
    {
        // Si el juego no ha finalizado
        if (!gameOver)
        {
            // Al presionar la tecla Espacio
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Aplica un impuslo hacia arriba
                playerRigidbody.AddForce(Vector3.up * forceValue, ForceMode.Impulse);

                // Reproduce el efecto de sonido
                playerAudioSource.PlayOneShot(BoingAudio, effectsVolume);
            }

            // Si la altura es mayor al límite superior en Y
            if (transform.position.y > yLimit)
            {
                // Posiciona al player en ese límite y le reduce la velocidad a 0 para que no se quede pegado
                transform.position = new Vector3(transform.position.x, yLimit, transform.position.z);
                playerRigidbody.velocity = Vector3.zero;
            }
        }
    }

    // Al colisionar con un GameObject con triggers
    private void OnTriggerEnter(Collider other)
    {
        // Si el juego no ha finalizado
        if (!gameOver)
        {
            // Si el otro GameObject tiene el tag "Money"
            if (other.gameObject.CompareTag("Money"))
            {
                // Suma +1 al total de monedas obtenidas
                totalMoney++;

                // Reproduce el efecto de sonido
                playerAudioSource.PlayOneShot(BlipAudio, effectsVolume);

                // Posiciona y reproduce las partículas en la posición de ese GameObject
                fireworksParticles.transform.position = other.gameObject.transform.position;
                fireworksParticles.Play();

                // Destruye el otro GameObject
                Destroy(other.gameObject);

                // Muestra por consola el texto con el total de monedas
                Debug.Log($"Has obtenido " + totalMoney + " monedas en total.");
            }

            if (other.gameObject.CompareTag("Bomb"))
            {
                // Reproduce el efecto de sonido
                playerAudioSource.PlayOneShot(BoomAudio, effectsVolume);

                // Posiciona y reproduce las partículas en la posición de ese GameObject
                explosionParticles.transform.position = other.gameObject.transform.position;
                explosionParticles.Play();

                // Destruye el otro GameObject
                Destroy(other.gameObject);

                // Llama a la función GameOver
                GameOver();
            }
        }

        // Al tocar el suelo explota (Un tributo a Michael Bay)
        if (other.gameObject.CompareTag("Ground"))
        {
            // Posiciona y reproduce las partículas en la posición de ese GameObject
            explosionParticles.transform.position = other.gameObject.transform.position;
            explosionParticles.Play();

            // Destruye el otro GameObject
            Destroy(gameObject);

            // Llama a la función GameOver
            GameOver();
        }
    }

    // Función que da por finalizado el juego
    private void GameOver()
    {
        // Indica que el juego ha finalizado
        gameOver = true;

        // Disminuye el volúmen de la música
        cameraAudioSource.volume = musicVolume;

        // Muestra por consola el texto de juego finalizado con el total de monedas
        Debug.Log($"GAME OVER: has conseguido " + totalMoney + " monedas.");
    }
}