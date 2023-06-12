using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Interface : MonoBehaviour
{
    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }
}
