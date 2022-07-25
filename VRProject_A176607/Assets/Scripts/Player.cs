using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Visuals")]
    public Camera playerCamera;

    [Header("GamePlay")]
    public int initialHealth = 100;
    public int initialRifleAmmo = 12;
    public int initialRocketLauncherAmmo = 5;
    public float knockbackForce = 10;
    public float hurtDuration = 0.5f;

    private int health;
    public int Health { get { return health; } }

    private int rifleAmmo;
    public int RifleAmmo { get { return rifleAmmo; } }

    private int rocketLauncherAmmo;
    public int RocketLauncherAmmo { get { return rocketLauncherAmmo; } }

    private bool killed;
    public bool Killed { get { return killed; } }

    private bool isHurt;

    [SerializeField] GameObject rifleObject;
    [SerializeField] GameObject rocketLauncherObject;
    [SerializeField] GameObject rifleAim1;
    [SerializeField] GameObject rifleAim2;
    [SerializeField] GameObject rocketLauncherAim1;
    [SerializeField] GameObject rocketLauncherAim2;
    [SerializeField] GameObject rocketLauncherAim3;
    [SerializeField] GameObject rocketLauncherAim4;

    public AudioSource noAmmo;
    public AudioSource injure;
    public AudioSource rifleAmmoReload;
    public AudioSource rocketLauncherReload;
    public AudioSource heal;

    public Transform rifleAim;
    public Transform rifleNotAim;
    public Transform rocketLauncherAim;
    public Transform rocketLauncherNotAim;
    public bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        rifleAmmo = initialRifleAmmo;
        rocketLauncherAmmo = initialRocketLauncherAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (rifleObject.activeSelf == true)
            {
                if (rifleAmmo > 0 && Killed == false)
                {
                    rifleAmmo--;
                    GameObject rifleBulletObject = ObjectPoolingManager.Instance.GetRifleBullet(true);
                    rifleBulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                    rifleBulletObject.transform.forward = playerCamera.transform.forward;
                } else
                {
                    noAmmo.Play();
                }
            } else if (rocketLauncherObject.activeSelf == true)
            {
                if (rocketLauncherAmmo > 0 && Killed == false)
                {
                    rocketLauncherAmmo--;
                    GameObject rocketLauncherBulletObject = RL_ObejctPoolingManager.Instance.GetRocketLauncherBullet(true);
                    rocketLauncherBulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward;
                    rocketLauncherBulletObject.transform.forward = playerCamera.transform.forward;
                } else
                {
                    noAmmo.Play();
                }
            }
        } else if (Input.GetButtonDown("Fire2"))
        {
            if (rifleObject.activeSelf == true)
            {
                rifleObject.SetActive(false);
                rocketLauncherObject.SetActive(true);
                rifleAim1.SetActive(false);
                rifleAim2.SetActive(false);
                rocketLauncherAim1.SetActive(true);
                rocketLauncherAim2.SetActive(true);
                rocketLauncherAim3.SetActive(true);
                rocketLauncherAim4.SetActive(true);
                isAiming = false;
                rifleObject.transform.position = rifleNotAim.position;

            } else if (rocketLauncherObject.activeSelf == true)
            {
                rocketLauncherObject.SetActive(false);
                rifleObject.SetActive(true);
                rocketLauncherAim1.SetActive(false);
                rocketLauncherAim2.SetActive(false);
                rocketLauncherAim3.SetActive(false);
                rocketLauncherAim4.SetActive(false);
                rifleAim1.SetActive(true);
                rifleAim2.SetActive(true);
                isAiming = false;
                rocketLauncherObject.transform.position = rocketLauncherNotAim.position;
            }
        } else if (Input.GetButton("Fire3"))
        {
            if (rifleObject.activeSelf == true)
            {
                if (isAiming == false)
                {
                    isAiming = true;
                    rifleObject.transform.position = rifleAim.position;
                } else if (isAiming == true)
                {
                    isAiming = false;
                    rifleObject.transform.position = rifleNotAim.position;
                }
            } else if (rocketLauncherObject.activeSelf == true)
            {
                if (isAiming == false)
                {
                    isAiming = true;
                    rocketLauncherObject.transform.position = rocketLauncherAim.position;
                }
                else if (isAiming == true)
                {
                    isAiming = false;
                    rocketLauncherObject.transform.position = rocketLauncherNotAim.position;
                }
            }
        }
    }

    private void OnTriggerEnter (Collider otherCollider)
    {
         if (otherCollider.GetComponent<RifleAmmoCrate>() != null)
        {
            RifleAmmoCrate rifleAmmoCrate = otherCollider.GetComponent<RifleAmmoCrate>();
            rifleAmmo += rifleAmmoCrate.rifleAmmo;
            rifleAmmoReload.Play();
            Destroy(rifleAmmoCrate.gameObject);
        } else if (otherCollider .GetComponent<RocketLauncherAmmoCrate>() != null)
        {
            RocketLauncherAmmoCrate rocketLauncherAmmoCrate = otherCollider.GetComponent<RocketLauncherAmmoCrate>();
            rocketLauncherAmmo += rocketLauncherAmmoCrate.rocketLauncherAmmo;
            rocketLauncherReload.Play();
            Destroy(rocketLauncherAmmoCrate.gameObject);
        } else if (otherCollider.GetComponent<HealthCrate>() != null)
        {
            HealthCrate healthCrate = otherCollider.GetComponent<HealthCrate>();
            health += healthCrate.health;
            heal.Play();
            Destroy(healthCrate.gameObject);
        }
        if (isHurt == false)
        {
            GameObject hazard = null;
            if (otherCollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = otherCollider.GetComponent<Enemy>();

                if (enemy.Killed == false)
                {
                    hazard = enemy.gameObject;
                    health -= enemy.damage;
                    injure.Play();
                }
            } else if (otherCollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = otherCollider.GetComponent<Bullet> ();
                if (bullet.ShotByPlayer == false)
                {
                    hazard = bullet.gameObject;
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                    injure.Play();
                }
            }
            if (hazard != null)
            {
                isHurt = true;

                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce);

                StartCoroutine(HurtRoutine());
            }
            if (health <= 0)
            {
                if (killed == false)
                {
                    killed = true;

                    OnKill ();
                }
            }
        }
    }

    IEnumerator HurtRoutine ()
    {
        yield return new WaitForSeconds (hurtDuration);

        isHurt = false;
    }

    private void OnKill ()
    {
        GetComponent<JMPlayerMovements>().enabled = false;
    }
}