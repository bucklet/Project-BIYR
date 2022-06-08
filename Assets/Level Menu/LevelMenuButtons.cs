using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuButtons : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void setLvlComplete(int number)
    {
        PlayerPrefs.SetInt("lvlComplete", number);
        Debug.Log("set lvlComplete to " + number);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
