using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlay ()
    {
        SceneManager.LoadScene("Scene 1");
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Scene 1");
        } else if (Input.GetButtonDown("Fire2"))
        {
            Application.Quit();
        }
    }
}
