using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherAmmoCrate : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject container;
    public float rotationSpeed = 90f;

    [Header("Gameplay")]
    public int rocketLauncherAmmo = 5;

    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
