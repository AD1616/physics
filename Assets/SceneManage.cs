using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void GravLoad()
    {
        SceneManager.LoadScene("Orbit");
    }

    public void CoulombLoad()
    {
        SceneManager.LoadScene("ElectronFlow");
    }

    public void MainLoad()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
