using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JMPlayerMovements : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    [SerializeField] GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed, 0, Input.GetAxis("Vertical") * moveSpeed);
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotateSpeed);

        if (Input.GetButton("Jump"))
        {
            player.transform.Rotate(new Vector3(0,0,90));
        }
    }
}