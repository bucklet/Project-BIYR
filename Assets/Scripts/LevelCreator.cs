using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementTypes
{
    Empty_obj = 0,
    Bob_obj,
    Wall_obj,
    Star_obj,
    Flag_obj,
    Water_obj,
    Lava_obj,
    Skull_obj,

    Is_txt = 99,
    Bob_txt = 100,
    Wall_txt,
    Flag_txt,
    Star_txt,
    Water_txt,
    Lava_txt,
    Skull_txt,



    You_txt = 200,
    Push_txt,
    Win_txt,
    Stop_txt,
    Sink_txt,
    Defeat_txt,
    Hot_txt,
    Melt_txt,

}

[CreateAssetMenu()]
[System.Serializable]
public class LevelCreator : ScriptableObject
{

    [SerializeField]
    public List<ElementTypes> level = new List<ElementTypes>();
    public int lvlColumns;
    public int lvlRows;
    public string lvlName;
    public string lvlHelp;
    public Sprite LevelPreview;

    public LevelCreator()
    {
        level = new List<ElementTypes>();
        level.Capacity = lvlRows * lvlColumns;
    }
}
