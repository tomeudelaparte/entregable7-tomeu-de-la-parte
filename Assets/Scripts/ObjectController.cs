using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    // Dirección por defecto hacia la derecha
    public float directionSpeed = 10f;

    // Límite por defecto en la parte derecha
    private float xRange = 14f;

    // A cada frame
    void Update()
    {
        // Mueve el objeto en el eje horizontal
        transform.Translate(Vector3.right * directionSpeed * Time.deltaTime);

        // Si la posición es mayor a xRange y la dirección es mayor a 0 o la posición es menor a -xRange y la dirección es menor a 0
        if ((transform.position.x > xRange && directionSpeed > 0)
            || (transform.position.x < -xRange && directionSpeed < 0))
        {
            // Destruye el objeto
            Destroy(gameObject);
        }
    }
}
