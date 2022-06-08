using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnSelect : MonoBehaviour, ISelectHandler
{
    public GameObject ButtonPanel;
    public GameObject ButtonHighlightS;
    public GameObject ButtonHighlightB;
    public bool SmallBtn;

    void Start()
    {
        ButtonHighlightS = GameObject.FindWithTag("ButtonHighlightS");
        ButtonHighlightB = GameObject.FindWithTag("ButtonHighlightB");
    }
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        //ButtonHighlight.transform.position = ButtonPanel.transform.position;
        //this.GetComponentInParent<ChooseLevel>().ChooseLvl();
        //Debug.Log("selected");
        if (SmallBtn!=true)
        {
            ButtonHighlightB.transform.position = ButtonPanel.transform.position;
            ButtonHighlightB.SetActive(true);
            ButtonHighlightS.SetActive(false);
            return;
        }
        ButtonHighlightS.transform.position = ButtonPanel.transform.position;
        ButtonHighlightS.SetActive(true);
        ButtonHighlightB.SetActive(false);
    }
}
