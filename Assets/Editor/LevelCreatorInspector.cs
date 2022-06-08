using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))]
[ExecuteInEditMode]
public class LevelCreatorInspector : Editor
{
    Dictionary<ElementTypes, Texture> textureHolder = new Dictionary<ElementTypes, Texture>();
    
    private void OnEnable()
    {
        textureHolder.Add(ElementTypes.Empty_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/empty_obj.png"));
        textureHolder.Add(ElementTypes.Is_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/is_txt.png"));
        textureHolder.Add(ElementTypes.You_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/you_txt.png"));
        textureHolder.Add(ElementTypes.Win_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/win_txt.png"));
        textureHolder.Add(ElementTypes.Push_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/push_txt.png"));
        textureHolder.Add(ElementTypes.Stop_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/stop_txt.png"));
        textureHolder.Add(ElementTypes.Sink_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/sink_txt.png"));
        textureHolder.Add(ElementTypes.Defeat_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/defeat_txt.png"));
        textureHolder.Add(ElementTypes.Hot_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/hot_txt.png"));
        textureHolder.Add(ElementTypes.Melt_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/melt_txt.png"));
        textureHolder.Add(ElementTypes.Bob_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/bob_obj.png"));
        textureHolder.Add(ElementTypes.Bob_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/bob_txt.png"));
        textureHolder.Add(ElementTypes.Wall_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/wall_obj.png"));
        textureHolder.Add(ElementTypes.Wall_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/wall_txt.png"));
        textureHolder.Add(ElementTypes.Star_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/star_obj.png"));
        textureHolder.Add(ElementTypes.Star_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/star_txt.png"));
        textureHolder.Add(ElementTypes.Flag_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/flag_obj.png"));
        textureHolder.Add(ElementTypes.Flag_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/flag_txt.png"));
        textureHolder.Add(ElementTypes.Water_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/water_obj.png"));
        textureHolder.Add(ElementTypes.Water_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/water_txt.png"));
        textureHolder.Add(ElementTypes.Lava_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/lava_obj.png"));
        textureHolder.Add(ElementTypes.Lava_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/lava_txt.png"));
        textureHolder.Add(ElementTypes.Skull_obj, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/skull_obj.png"));
        textureHolder.Add(ElementTypes.Skull_txt, (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/skull_txt.png"));

    }
    ElementTypes currentSelected = ElementTypes.Empty_obj;
    public override void OnInspectorGUI()
    {
        // emptyTexture = (Texture)EditorGUIUtility.Load("Assets/EditorDefaultResources/empty.png");

        base.OnInspectorGUI();
        GUILayout.Label("Current Selected : " + currentSelected.ToString());

        LevelCreator levelCreator = (LevelCreator)target;
        //int rows = (int)Mathf.Sqrt(levelCreator.level.Count);
        int columns = levelCreator.lvlColumns;
        int rows = levelCreator.lvlRows;
        //int currentI = levelCreator.level.Count-1;
        GUILayout.BeginVertical();
        for (int r = rows - 1; r >= 0; r--)
        {

            GUILayout.BeginHorizontal();
            for (int c = 0; c < columns; c++)
            {
                if (GUILayout.Button(textureHolder[levelCreator.level[c + ((columns) * r)]], GUILayout.Width(25), GUILayout.Height(25)))
                {
                    levelCreator.level[c + ((columns) * r)] = currentSelected;
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
        
        GUILayout.Space(20);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        int count = 0;
        foreach (KeyValuePair<ElementTypes, Texture> e in textureHolder)
        {
            count++;
            if (GUILayout.Button(e.Value, GUILayout.Width(50), GUILayout.Height(50)))
            {
                currentSelected = e.Key;
            }
            if (count % 6 == 0)
            {
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        
        /* if (GUILayout.Button(textureHolder[ElementTypes.Empty],GUILayout.Width(50),GUILayout.Height(50))){
             currentSelected = ElementTypes.Empty;
         }
         else if (GUILayout.Button(textureHolder[ElementTypes.Baba], GUILayout.Width(50), GUILayout.Height(50)))
         {
             currentSelected = ElementTypes.Baba;
         }
         else if (GUILayout.Button(textureHolder[ElementTypes.Wall], GUILayout.Width(50), GUILayout.Height(50)))
         {
             currentSelected = ElementTypes.Wall;
         }
         else if (GUILayout.Button("Rock"))
         {
             currentSelected = ElementTypes.Rock;
         }
         else if (GUILayout.Button("Flag"))
         {
             currentSelected = ElementTypes.Flag;
         }
         else if (GUILayout.Button("Goop"))
         {
             currentSelected = ElementTypes.Goop;
         }
         else if (GUILayout.Button("Is"))
         {
             currentSelected = ElementTypes.IsWord;
         }
         else if (GUILayout.Button("BabaWord"))
         {
             currentSelected = ElementTypes.BabaWord;
         }
         else if (GUILayout.Button("WallWord"))
         {
             currentSelected = ElementTypes.WallWord;
         }
         else if (GUILayout.Button("FlagWord"))
         {
             currentSelected = ElementTypes.FlagWord;
         }
         else if (GUILayout.Button("RockWord"))
         {
             currentSelected = ElementTypes.RockWord;
         }
         else if (GUILayout.Button("YouWord"))
         {
             currentSelected = ElementTypes.YouWord;
         }
         else if (GUILayout.Button("PushWord"))
         {
             currentSelected = ElementTypes.PushWord;
         }
         else if (GUILayout.Button("WinWord"))
         {
             currentSelected = ElementTypes.WinWord;
         }
         else if (GUILayout.Button("StopWord"))
         {
             currentSelected = ElementTypes.StopWord;
         }*/


    }
}
