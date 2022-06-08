using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public int lvlInt;
    public LevelCreator Level;
    public GameObject lvlPreview;
    public GameObject lvlName;
    public GameObject lvlCompleteText;
    public GameObject lvlButton;
    private GameObject LevelHighlight;
    private GameObject PlayButton;
    public Sprite NoPreview;
    private void Start()
    {
        LevelHighlight = GameObject.FindWithTag("Highlight");
        PlayButton = GameObject.FindWithTag("PlayButton");
        ChangeName("Уровень " + (lvlInt + 1));
        if (Level.LevelPreview)
        {
            ChangePreview(Level.LevelPreview);
        }
        else
        {
            ChangePreview(NoPreview);
        }
        if (PlayerPrefs.GetInt("lvlComplete") < lvlInt)
            lvlButton.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("lvlComplete") > lvlInt)
            lvlCompleteText.SetActive(true);
    }
    public void ChooseLvl()
    {
        PlayerPrefs.SetInt("Level", lvlInt);
        PlayButton.GetComponent<Button>().interactable = true;
        LevelHighlight.transform.position = this.transform.position;
        LevelHighlight.SetActive(true);
        Debug.Log("lvl chosen");
    }
    public void ChangePreview(Sprite preview)
    {
        lvlPreview.GetComponent<Image>().sprite = preview;
    }
    public void ChangeName(string newText)
    {
        lvlName.GetComponent<Text>().text = newText;
    }
}
