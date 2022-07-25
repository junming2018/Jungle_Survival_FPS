using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmoCrate : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject container;
    public float rotationSpeed = 180f;

    [Header("Gameplay")]
    public int rifleAmmo = 12;

    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
