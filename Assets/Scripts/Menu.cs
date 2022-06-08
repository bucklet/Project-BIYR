using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject RulesPanel;
    public GameObject RulesPanelOne;
    public GameObject RulesPanelTwo;
    public Button PlayButton;
    public Button BackButton;
    public Button NextButton;
    int rulePage;
    public void Play()
    {
        SceneManager.LoadScene("Levels");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Rules()
    {
        RulesPanel.SetActive(true);
        rulePage = 1;
        GoToRulePage(rulePage);
        NextButton.Select();
        MainPanel.SetActive(false);
    }

    public void RuleNext()
    {
        switch (rulePage)
        {
            case 1:
                rulePage = 2;
                GoToRulePage(2);
                NextButton.Select();
                break;
            case 2:
                GoToMainPage();
                PlayButton.Select();
                break;
        }
    }
    public void RulePrevious()
    {
        switch (rulePage)
        {
            case 1:
                GoToMainPage();
                PlayButton.Select();
                break;
            case 2:
                rulePage = 1;
                GoToRulePage(1);
                BackButton.Select();
                break;
        }
    }
    public void GoToRulePage(int page)
    {
        switch (page)
        {
            case 1:
                RulesPanelTwo.SetActive(false);
                RulesPanelOne.SetActive(true);
                break;
            case 2:
                RulesPanelOne.SetActive(false);
                RulesPanelTwo.SetActive(true);
                break;
        }
    }
    public void GoToMainPage()
    {
        MainPanel.SetActive(true);
        RulesPanel.SetActive(false);
    }
}
