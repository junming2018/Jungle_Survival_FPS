using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HXPlayerMovements : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        transform.position += movementDirection * speed * Time.deltaTime;
    }
}