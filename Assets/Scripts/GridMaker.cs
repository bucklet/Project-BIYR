using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridMaker : MonoBehaviour
{
    int rows, cols;
    public GameObject cellHolder;
    public List<LevelCreator> levelHolder = new List<LevelCreator>();
    public List<GameObject> cells = new List<GameObject>();
    public List<SpriteLibrary> spriteLibrary = new List<SpriteLibrary>();
    public static GridMaker instance = null;
    public GameObject boundary;
    public GameObject AlertPanel;
    public GameObject AudioPanel;
    public GameAudioHandler GameAudioHandler;
    int currentLevel = 0;
    bool checkForPlayer;
    public int Rows
    {
        get { return rows; }
    }

    public int Cols
    {
        get { return cols; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        checkForPlayer = true;
        AlertPanel.SetActive(false);
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        currentLevel = PlayerPrefs.GetInt("Level");
        if (PlayerPrefs.GetInt("Win", 0)==1)
        {
            //Debug.Log("Playing WinSound");
            GameAudioHandler.GetComponent<GameAudioHandler>().PlayWinSound();
            PlayerPrefs.SetInt("Win", 0);
        }
        this.gameObject.GetComponent<OnLvlButtons>().setNameText(levelHolder[currentLevel].lvlName);
        this.gameObject.GetComponent<OnLvlButtons>().setHelpText(levelHolder[currentLevel].lvlHelp);
        /*float count = levelHolder[currentLevel].level.Count;
        rows = (int)Mathf.Sqrt(count);
        cols = rows;*/
        float count = levelHolder[currentLevel].level.Count;
        cols = levelHolder[currentLevel].lvlColumns;
        rows = levelHolder[currentLevel].lvlRows;
        

        CreateGrid();
        CompileRules();
        
        if (rows > cols)
            this.transform.position = new Vector3(cols / 2, rows + 1, rows / 2);
        else
            this.transform.position = new Vector3(cols / 2, cols*0.75f + 1, rows / 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AlertPanel.SetActive(false);
        }
    }

    public void CreateGrid()
    {
        for (int gI = -1; gI <= rows; gI += 1)
        {
            for (int gJ = -1; gJ <= cols; gJ += 1)
            {
                if (gI == -1 || gJ == -1 || gI == rows || gJ == cols)
                    Instantiate(boundary, new Vector3(gJ, 0, gI), Quaternion.Euler(90, 0, 0));
                
            }
        }
        int counter = 0;
        for (int gJ = 0; gJ < rows; gJ += 1)
        {
            for (int gI = 0; gI < cols; gI += 1)
            {
                if (levelHolder[currentLevel].level[counter] != ElementTypes.Empty_obj)
                {
                    GameObject g = Instantiate(cellHolder, new Vector3(gI, 0, gJ), Quaternion.Euler(90, 0, 0));
                    cells.Add(g);
                    ElementTypes currentElement = levelHolder[currentLevel].level[counter];
                    g.GetComponent<CellProperty>().AssignInfo(gJ, gI, currentElement);
                }
                counter++;
            }
        }

        /*
        int counter = 0;
        for (int i = 0; i < levelHolder[currentLevel].level.Count; i++)
        {
            if (levelHolder[currentLevel].level[i] != ElementTypes.Empty_obj)
            {
                GameObject g = Instantiate(cellHolder, new Vector3(counter % cols, 0, counter / rows), Quaternion.Euler(90, 0, 0));
                cells.Add(g);
                ElementTypes currentElement = levelHolder[currentLevel].level[i];

                g.GetComponent<CellProperty>().AssignInfo(counter / rows, counter % cols, currentElement);
                //Debug.Log( currentElement.ToString() + "R : " + i / rows + " C : " + i % cols);


            }
            counter++;
        }
        */
    }

    public Sprite ReturnSpriteOf(ElementTypes e)
    {
        return spriteLibrary.Find(x => x.element == e).sprite;
    }

    public Vector3 Return3D(int i)
    {
        return new Vector3(i % cols, 0, i / rows);
    }


    public bool IsStop(int r, int c, Vector3 dir)
    {
        bool isPush = false;
        int curRow = r, curCol = c;
        List<GameObject> atRC = FindObjectsAt(curRow, curCol);
        if (r >= rows || c >= cols || r < 0 || c < 0)
            return true;
        foreach (GameObject g in atRC)
        {
            CellProperty currentCell = g.GetComponent<CellProperty>();

            if (currentCell.IsStop)
            {
                return true;
            }
            else if (currentCell.IsPushable)
            {
                isPush = true;
            }
        }

        if (!isPush)
            return false;

        if (dir == Vector3.right)
        {
            return IsStop(curRow, curCol + 1, Vector3.right);
        }
        else if (dir == Vector3.left)
        {
            return IsStop(curRow, curCol - 1, Vector3.left);
        }
        else if (dir == Vector3.forward)
        {
            return IsStop(curRow + 1, curCol, Vector3.forward);
        }
        else if (dir == Vector3.back)
        {
            return IsStop(curRow - 1, curCol, Vector3.back);
        }
        return true;
    }

    public void CompileRules()
    {
        ResetData();
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i] != null)
            {
                CellProperty currentcell = cells[i].GetComponent<CellProperty>();

                if (IsElementStartingWord(currentcell.Element))
                {

                    /*if (DoesListContainElement(FindObjectsAt(currentcell.CurrentRow + 1, currentcell.CurrentCol), ElementTypes.IsWord))
                    {
                        if (DoesListContainWord(FindObjectsAt(currentcell.CurrentRow +2, currentcell.CurrentCol)))
                        {
                            Rule(currentcell.Element, ReturnWordAt(currentcell.CurrentRow + 2, currentcell.CurrentCol));
                        }
                    }*/
                    if (DoesListContainElement(FindObjectsAt(currentcell.CurrentRow - 1, currentcell.CurrentCol), ElementTypes.Is_txt))
                    {
                        if (DoesListContainWord(FindObjectsAt(currentcell.CurrentRow - 2, currentcell.CurrentCol)))
                        {
                            Rule(currentcell.Element, ReturnWordAt(currentcell.CurrentRow - 2, currentcell.CurrentCol));
                            
                            cells[i].GetComponent<CellProperty>().HighlightCell(true);
                            foreach (GameObject cellgo in (FindObjectsAt(currentcell.CurrentRow - 1, currentcell.CurrentCol)))
                            {
                                cellgo.GetComponent<CellProperty>().HighlightCell(true);
                            }
                            foreach (GameObject cellgo in (FindObjectsAt(currentcell.CurrentRow - 2, currentcell.CurrentCol)))
                            {
                                cellgo.GetComponent<CellProperty>().HighlightCell(true);
                            }
                        }
                    }
                    if (DoesListContainElement(FindObjectsAt(currentcell.CurrentRow, currentcell.CurrentCol + 1), ElementTypes.Is_txt))
                    {
                        if (DoesListContainWord(FindObjectsAt(currentcell.CurrentRow, currentcell.CurrentCol + 2)))
                        {
                            Rule(currentcell.Element, ReturnWordAt(currentcell.CurrentRow, currentcell.CurrentCol + 2));

                            cells[i].GetComponent<CellProperty>().HighlightCell(true);
                            foreach (GameObject cellgo in (FindObjectsAt(currentcell.CurrentRow, currentcell.CurrentCol + 1)))
                            {
                                cellgo.GetComponent<CellProperty>().HighlightCell(true);
                            }
                            foreach (GameObject cellgo in (FindObjectsAt(currentcell.CurrentRow, currentcell.CurrentCol + 2)))
                            {
                                cellgo.GetComponent<CellProperty>().HighlightCell(true);
                            }
                        }
                    }

                    /*if (DoesListContainElement(FindObjectsAt(currentcell.CurrentRow, currentcell.CurrentCol -1), ElementTypes.IsWord))
                    {
                        if (DoesListContainWord(FindObjectsAt(currentcell.CurrentRow, currentcell.CurrentCol - 2)))
                        {
                            Rule(currentcell.Element, ReturnWordAt(currentcell.CurrentRow, currentcell.CurrentCol - 2));
                        }
                    }*/
                }
            }

        }
    }


    public ElementTypes GetActualObjectFromWord(ElementTypes e)
    {
        ElementTypes result;
        switch(e)
        {
            case ElementTypes.Bob_txt:
                result = ElementTypes.Bob_obj;
                break;
            case ElementTypes.Flag_txt:
                result = ElementTypes.Flag_obj;
                break;
            case ElementTypes.Star_txt:
                result = ElementTypes.Star_obj;
                break;
            case ElementTypes.Wall_txt:
                result = ElementTypes.Wall_obj;
                break;
            case ElementTypes.Water_txt:
                result = ElementTypes.Water_obj;
                break;
            case ElementTypes.Skull_txt:
                result = ElementTypes.Skull_obj;
                break;
            case ElementTypes.Lava_txt:
                result = ElementTypes.Lava_obj;
                break;
            default:
                result = ElementTypes.Empty_obj;
                break;
        }
        return result;
        /*
        if (e == ElementTypes.Bob_txt)
        {
            return ElementTypes.Bob_obj;
        }
        else if (e == ElementTypes.Flag_txt)
        {
            return ElementTypes.Flag_obj;
        }
        else if (e == ElementTypes.Star_txt)
        {
            return ElementTypes.Star_obj;
        }
        else if (e == ElementTypes.Wall_txt)
        {
            return ElementTypes.Wall_obj;
        }
        else if (e == ElementTypes.Water_txt)
        {
            return ElementTypes.Water_obj;
        }
        return ElementTypes.Empty_obj;
        */
    }


    public void Rule(ElementTypes a, ElementTypes b)
    {
        if ((int)b >= 100 && (int)b < 150)
        {
            //Replace all a objects to b
            List<CellProperty> cellsOf = GetAllCellsOf(GetActualObjectFromWord(a));
            for (int i = 0; i < cellsOf.Count; i++)
            {
                cellsOf[i].ChangeObject(GetCellOf(GetActualObjectFromWord(b)));
            }
        }
        else if ((int)b >= 150)
        {
            //Properties change
            if (b == ElementTypes.You_txt)
            {
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsPlayer(true);
                }
                //player property true
            }
            else if (b == ElementTypes.Push_txt)
            {
                //pushable property true
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItPushable(true);
                }
            }
            else if (b == ElementTypes.Win_txt)
            {
                //win property true
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItWin(true);
                }
            }
            else if (b == ElementTypes.Stop_txt)
            {
                //stop property true
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItStop(true);
                }
            }
            else if (b == ElementTypes.Sink_txt)
            {
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItSink(true);
                }
            }
            else if (b == ElementTypes.Defeat_txt)
            {
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItDefeat(true);
                }
            }
            else if (b == ElementTypes.Hot_txt)
            {
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItHot(true);
                }
            }
            else if (b == ElementTypes.Melt_txt)
            {
                foreach (CellProperty p in GetAllCellsOf(GetActualObjectFromWord(a)))
                {
                    p.IsItMelt(true);
                }
            }
            
        }

    }

    public void ResetData()
    {
        foreach (GameObject g in cells)
        {
            if (g != null)
                g.GetComponent<CellProperty>().Initialize();
        }
    }

    public void CheckPlayerObjects()
    {
        if (checkForPlayer)
        {
            bool alert = true;
            foreach (GameObject g in cells)
            {
                if (g != null)
                {
                    if (g.GetComponent<CellProperty>().IsPlayableObject)
                    {
                        alert = false;
                    }
                }
            }
            if (alert == true)
            {
                AudioPanel.GetComponent<AudioPanelScript>().EmergencyStop();
                AlertPanel.SetActive(true);
                Debug.Log("noPlayerFound");
                checkForPlayer = false;
            }
        }
    }

    public CellProperty GetCellOf(ElementTypes e)
    {
        foreach (GameObject g in cells)
        {
            if (g != null && g.GetComponent<CellProperty>().Element == e)
            {
                return g.GetComponent<CellProperty>();
            }
        }
        return null;
    }

    public List<CellProperty> GetAllCellsOf(ElementTypes e)
    {
        List<CellProperty> cellProp = new List<CellProperty>();

        foreach (GameObject g in cells)
        {
            if (g != null && g.GetComponent<CellProperty>().Element == e)
            {
                cellProp.Add(g.GetComponent<CellProperty>());
            }
        }
        return cellProp;

    }

    public bool IsTherePushableObjectAt(int r, int c)
    {
        List<GameObject> objectsAtRC = FindObjectsAt(r, c);

        foreach (GameObject g in objectsAtRC)
        {
            if (g.GetComponent<CellProperty>().IsPushable)
            {
                return true;
            }
        }
        return false;

    }

    public GameObject GetPushableObjectAt(int r, int c)
    {
        List<GameObject> objectsAtRC = FindObjectsAt(r, c);

        foreach (GameObject g in objectsAtRC)
        {
            if (g.GetComponent<CellProperty>().IsPushable)
            {
                return g;
            }
        }

        return null;
    }

    public bool IsElementStartingWord(ElementTypes e)
    {
        if ((int)e >= 100 && (int)e < 150)
        {
            return true;
        }
        return false;
    }

    public List<GameObject> FindObjectsAt(int r, int c)
    {
        return cells.FindAll(x => x != null && x.GetComponent<CellProperty>().CurrentRow == r && x.GetComponent<CellProperty>().CurrentCol == c);
    }

    public ElementTypes ReturnWordAt(int r, int c)
    {
        List<GameObject> l = FindObjectsAt(r, c);

        foreach (GameObject g in l)
        {
            ElementTypes e = g.GetComponent<CellProperty>().Element;
            if ((int)e >= 100)
            {
                return e;
            }

        }
        return ElementTypes.Empty_obj;
    }

    public bool DoesListContainElement(List<GameObject> l, ElementTypes e)
    {
        foreach (GameObject g in l)
        {
            if (g.GetComponent<CellProperty>().Element == e)
            {
                return true;
            }
        }
        return false;
    }

    public bool DoesListContainWord(List<GameObject> l)
    {
        foreach (GameObject g in l)
        {
            if ((int)g.GetComponent<CellProperty>().Element >= 100)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsElementIsWord(ElementTypes e)
    {
        if ((int)e == 99)
        {
            return true;
        }
        return false;
    }

    public void NextLevel()
    {
        if (PlayerPrefs.GetInt("lvlComplete") == (PlayerPrefs.GetInt("Level")) - 1)
        {
            PlayerPrefs.SetInt("lvlComplete", (PlayerPrefs.GetInt("lvlComplete") + 1));
            Debug.Log("lvlComplete++");
        }
        if (PlayerPrefs.GetInt("Level") >= levelHolder.Count)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
[System.Serializable]
public class SpriteLibrary
{

    public ElementTypes element;
    public Sprite sprite;

}