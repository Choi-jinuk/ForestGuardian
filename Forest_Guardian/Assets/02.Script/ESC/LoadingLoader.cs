using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingLoader : MonoBehaviour
{
    public void StartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
}
