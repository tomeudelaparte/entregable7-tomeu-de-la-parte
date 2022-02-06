using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Variable para guardar el script controller del Player
    private PlayerController playerControllerScript;

    // Array de objetos prefab
    public GameObject[] objectsPrefab;

    // Variables para guardar el objeto, direcci�n y posici�n random
    private int randomObject;
    private int direction;
    private Vector3 randomPosition;

    // Variable para guardar el objeto spawneado
    private GameObject spawnedObject;

    // Rango para definir el intervalo de spawn
    private float[] rangeInvoke = { 0, 1 };

    // Posiciones para definir la posici�n y direcci�n del objeto
    private float[] spawnSide = { -14f, 14f };
    private float[] spawnDirection = { 1f, -1f };

    // Rangos para generar los valores aleatorios
    private int[] rangeRandomObject = { 0, 2 };
    private int[] rangeDirection = { 0, 2 };
    private int[] rangeRandomPosition = { 2, 14 };

    // Al iniciar el juego
    void Start()
    {
        // Obtiene la componente del Player (PlayerController)
        playerControllerScript = FindObjectOfType<PlayerController>();

        // Llama a la funci�n cada cierto tiempo
        InvokeRepeating("spawnRandomObject", rangeInvoke[0], rangeInvoke[1]);
    }

    // Spawnea un objeto aleatorio en una posici�n y direcci�n aleatoria
    private void spawnRandomObject()
    {
        // Si el juego no ha finalizado
        if (!playerControllerScript.gameOver)
        {
            // Objeto aleatorio
            randomObject = Random.Range(rangeRandomObject[0], rangeRandomObject[1]);

            // Direcci�n aleatoria
            direction = Random.Range(rangeDirection[0], rangeDirection[1]);

            // Altura aleatoria
            randomPosition.y = Random.Range(rangeRandomPosition[0], rangeRandomPosition[1]);

            // Posici�n seg�n la direcci�n aleatoria
            randomPosition.x = spawnSide[direction];

            // Spawnea el objeto
            spawnedObject = Instantiate(objectsPrefab[randomObject], randomPosition, objectsPrefab[randomObject].transform.rotation);

            // Cambia la direcci�n del objeto spawneado
            spawnedObject.GetComponent<ObjectController>().directionSpeed *= spawnDirection[direction];
        }
    }
}