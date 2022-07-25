using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    private static ObjectPoolingManager instance;
    public static ObjectPoolingManager Instance { get { return instance; } }

    public GameObject rifleBulletPrefab;
    public int rifleBulletAmount = 20;

    private List<GameObject> rifleBullets;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        rifleBullets = new List<GameObject>(rifleBulletAmount);

        for (int i = 0; i < rifleBulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate (rifleBulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive (false);

            rifleBullets.Add(prefabInstance);
        }
    }

    public GameObject GetRifleBullet(bool shotByPlayer)
    {
        foreach (GameObject rifleBullet in rifleBullets)
        {
            if (!rifleBullet.activeInHierarchy)
            {
                rifleBullet.SetActive(true);
                rifleBullet.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
                return rifleBullet;
            }
        }
        GameObject prefabInstance = Instantiate(rifleBulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
        rifleBullets.Add(prefabInstance);
        return prefabInstance;
    }
}