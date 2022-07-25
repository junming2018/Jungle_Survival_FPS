using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;
    public GameObject enenyContainer;

    [Header("UI")]
    public Text healthText;
    public Text ammoText;
    public Text enemyText;
    public Text infoText;

    private bool gameOver = false;
    private float resetTimer = 7f;

    [SerializeField] GameObject rifleObject;
    [SerializeField] GameObject rocketLauncherObject;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject winAudio;
    [SerializeField] GameObject loseAudio;
    [SerializeField] GameObject rifleAim;
    [SerializeField] GameObject rocketLauncherAim;

    public AudioSource background;

    private void Start()
    {
        infoText.gameObject.SetActive (false);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + player.Health;

        if (rifleObject.activeSelf == true)
        {
            ammoText.text = "Rifle Ammo: " + player.RifleAmmo;
        } else if (rocketLauncherObject.activeSelf == true)
        {
            ammoText.text = "Rocket Launcher Ammo: " + player.RocketLauncherAmmo;
        }

        int aliveEnemies = 0;
        foreach (Enemy enemy in enenyContainer.GetComponentsInChildren<Enemy>())
        {
            if (enemy.Killed == false)
            {
                aliveEnemies++;
            }
        }
        enemyText.text = "Enemies: " + aliveEnemies;

        if (aliveEnemies == 0)
        {
            gameOver = true;
            rifleAim.SetActive(false);
            rocketLauncherAim.SetActive(false);
            infoText.gameObject.SetActive(true);
            infoText.text = "VICTORY!";
            background = backgroundMusic.GetComponent<AudioSource>();
            background.Stop();
            winAudio.SetActive(true);
        }
        if (player.Killed == true && aliveEnemies > 0)
        {
            gameOver = true;
            rifleAim.SetActive(false);
            rocketLauncherAim.SetActive(false);
            infoText.gameObject.SetActive(true);
            infoText.text = "GAME OVER!";
            background = backgroundMusic.GetComponent<AudioSource>();
            background.Stop();
            loseAudio.SetActive(true);
        }
        if (gameOver == true)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer <= 0)
            {
                SceneManager.LoadScene("Menu Scene");
            }
        }
    }
}
