using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float directionSpeed = 10f;
    private float xRange = 14f;

    void Update()
    {
        transform.Translate(Vector3.right * directionSpeed * Time.deltaTime);

        if ((transform.position.x > xRange && directionSpeed > 0)
            || (transform.position.x < -xRange && directionSpeed < 0))
        {
            Destroy(gameObject);
        }
    }
}
