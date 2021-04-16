using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
public void Setup()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false; //for Unity Editor
        Application.Quit();
    }
}
