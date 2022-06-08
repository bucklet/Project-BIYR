using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSelect : MonoBehaviour
{
    public GameObject ButtonHighlight;

    public void HighlightButton(Transform btn)
    {
        ButtonHighlight.transform.position = btn.position;
    }
    public void SelectButton(Button btn)
    {
        btn.Select();
    }
}
