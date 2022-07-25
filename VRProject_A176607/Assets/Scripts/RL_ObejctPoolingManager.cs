using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL_ObejctPoolingManager : MonoBehaviour
{
    private static RL_ObejctPoolingManager instance;
    public static RL_ObejctPoolingManager Instance { get { return instance; } }

    public GameObject rocketLauncherBulletPrefab;
    public int rocketLauncherBulletAmount = 5;

    private List<GameObject> rocketLauncherBullets;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        rocketLauncherBullets = new List<GameObject>(rocketLauncherBulletAmount);

        for (int i = 0; i < rocketLauncherBulletAmount; i++)
        {
            GameObject prefabInstance = Instantiate(rocketLauncherBulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);

            rocketLauncherBullets.Add(prefabInstance);
        }
    }

    public GameObject GetRocketLauncherBullet(bool shotByPlayer)
    {
        foreach (GameObject rifleBullet in rocketLauncherBullets)
        {
            if (!rifleBullet.activeInHierarchy)
            {
                rifleBullet.SetActive(true);
                rifleBullet.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
                return rifleBullet;
            }
        }
        GameObject prefabInstance = Instantiate(rocketLauncherBulletPrefab);
        prefabInstance.transform.SetParent(transform);
        prefabInstance.GetComponent<Bullet>().ShotByPlayer = shotByPlayer;
        rocketLauncherBullets.Add(prefabInstance);
        return prefabInstance;
    }
}