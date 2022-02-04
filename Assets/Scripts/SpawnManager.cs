using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControllerScript;

    public GameObject[] objectsPrefab;

    private int randomObject;
    private Vector3 randomPosition;
    private float[] spawnSide = { -14f, 14f };
    private float[] spawnDirection = { 1f, -1f};

    private int direction;

    void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController>();

        InvokeRepeating("spawnRandomObject", 0f, 1f);
    }

    private void spawnRandomObject()
    {
        if (!playerControllerScript.gameOver)
        {
            randomObject = Random.Range(0, 2);
            direction = Random.Range(0, 2);
            randomPosition.y = Random.Range(2, 14);

            randomPosition.x = spawnSide[direction];            

            GameObject spawnedObject = Instantiate(objectsPrefab[randomObject], randomPosition, objectsPrefab[randomObject].transform.rotation);

            spawnedObject.GetComponent<ObjectController>().directionSpeed *= spawnDirection[direction];
        }
    }
}
