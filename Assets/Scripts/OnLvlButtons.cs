using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnLvlButtons : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PPanelOne;
    public GameObject PPanelTwo;
    public Text HelpText;
    public Text NameText;
    public Button ResumeButton;
    GameObject EventSystem;
    bool isPaused, rulePanel;
    private void Start()
    {
        PausePanel.SetActive(false);
        isPaused = false;
        rulePanel = false;
        EventSystem = GameObject.FindWithTag("EventSystem");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
            EventSystem.GetComponent<ButtonSelect>().SelectButton(ResumeButton);
        }
    }
    public bool IsPaused
    {
        get { return isPaused; }
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Escape()
    {
        if (!IsPaused)
        {
            PausePanel.SetActive(true);
            PPanelTwo.SetActive(false);
            PPanelOne.SetActive(true);
            isPaused = true;
            return;
        }
        if (!rulePanel)
        {
            PausePanel.SetActive(false);
            isPaused = false;
            return;
        }
        changePanel();
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void changePanel()
    {
        if (!rulePanel)
        {
            PPanelTwo.SetActive(true);
            PPanelOne.SetActive(false);
            rulePanel = true;
            return;
        }
        PPanelTwo.SetActive(false);
        PPanelOne.SetActive(true);
        rulePanel = false;
    }
    public void setHelpText(string text)
    {
        if (text != "")
        {
            HelpText.GetComponent<Text>().text = text;
            return;
        }
        HelpText.GetComponent<Text>().text = "ERROR";
    }
    public void setNameText(string text)
    {
        if (text != "")
        {
            NameText.GetComponent<Text>().text = text;
            return;
        }
        NameText.GetComponent<Text>().text = "ERROR";
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GridMaker.instance.NextLevel();
    }
}
