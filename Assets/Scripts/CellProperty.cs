using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellProperty : MonoBehaviour
{
    public GameObject GameAudioHandler;
    public GameObject MCamera;
    public GameObject CellHighlight;

    ElementTypes element;
    bool isPushable;
    bool sinksObject;
    bool defeatsObject;
    bool isHot;
    bool isMelt;
    bool isWin;
    bool isPlayer;
    bool isStop;
    int currentRow, currentCol;
    public ElementTypes Element
    {
        get { return element; }
    }
    public bool IsStop
    {
        get { return isStop; }
    }
    public bool IsPushable
    {
        get { return isPushable; }
    }
    public bool IsPlayableObject
    {
        get { return isPlayer; }
    }
    public int CurrentRow
    {
        get { return currentRow; }
    }
    public int CurrentCol
    {
        get { return currentCol; }
    }

    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        MCamera = GameObject.FindWithTag("MainCamera");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (GameObject.FindWithTag("GameAudioSource"))
            GameAudioHandler = GameObject.FindWithTag("GameAudioSource");
        else
            Debug.Log("ERROR!");
    }

    public void AssignInfo(int r, int c, ElementTypes e)
    {
        currentRow = r;
        currentCol = c;
        element = e;
        ChangeSprite();
        if (e == ElementTypes.Wall_obj)
        {
            isStop = true;
        }

        if (e == ElementTypes.Bob_obj)
        {
            isPlayer = true;
            spriteRenderer.sortingOrder = 100;
        }
    }


    public void Initialize()
    {
        isPushable = false;
        sinksObject = false;
        defeatsObject = false;
        isWin = false;
        isPlayer = false;
        isStop = false;
        isHot = false;
        isMelt = false;
        HighlightCell(false);

        if ((int)element >= 99)
        {
            isPushable = true;
        }
    }

    public void ChangeSprite()
    {
        Sprite s = GridMaker.instance.spriteLibrary.Find(x => x.element == element).sprite;

        spriteRenderer.sprite = s;

        if (isPlayer || isPushable)
        {
            spriteRenderer.sortingOrder = 100;
        }
        else
        {
            spriteRenderer.sortingOrder = 10;
        }
    }


    public void ChangeObject(CellProperty c)
    {
        element = c.element;
        isPushable = c.isPushable;
        sinksObject = c.sinksObject;
        defeatsObject = c.defeatsObject;
        isWin = c.isWin;
        isPlayer = c.isPlayer;
        isStop = c.IsStop;
        isHot = c.isHot;
        isMelt = c.isMelt;
        ChangeSprite();
    }


    public void IsPlayer(bool isPl)
    {
        isPlayer = isPl;
    }

    public void IsItStop(bool isSt)
    {
        isStop = isSt;
    }

    public void IsItWin(bool isW)
    {
        isWin = isW;
    }
    public void IsItPushable(bool isPush)
    {
        isPushable = isPush;
    }
    public void IsItSink(bool isSink)
    {
        sinksObject = isSink;
    }
    public void IsItDefeat(bool isD)
    {
        defeatsObject = isD;
    }
    public void IsItHot(bool isH)
    {
        isHot = isH;
    }
    public void IsItMelt(bool isM)
    {
        isMelt = isM;
    }

    void Update()
    {
        if (MCamera.GetComponent<OnLvlButtons>().IsPaused == false)
        {
            CheckDestroy();
            if (isPlayer)
            {

                if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && currentCol + 1 < GridMaker.instance.Cols && !GridMaker.instance.IsStop(currentRow, currentCol + 1, Vector3.right))
                {
                    List<GameObject> movingObject = new List<GameObject>();
                    movingObject.Add(this.gameObject);
                    GameAudioHandler.GetComponent<GameAudioHandler>().UpdateGameVolume();
                    GameAudioHandler.GetComponent<GameAudioHandler>().PlayMoveSound();

                    for (int c = currentCol + 1; c < GridMaker.instance.Cols - 1; c++)
                    {
                        if (GridMaker.instance.IsTherePushableObjectAt(currentRow, c))
                        {
                            movingObject.Add(GridMaker.instance.GetPushableObjectAt(currentRow, c));
                        }
                        else
                        {
                            break;
                        }
                    }
                    foreach (GameObject g in movingObject)
                    {
                        g.transform.position = new Vector3(g.transform.position.x + 1, g.transform.position.y, g.transform.position.z);
                        g.GetComponent<CellProperty>().currentCol++;
                    }
                    GridMaker.instance.CompileRules();
                    CheckWin();
                }
                else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && currentCol - 1 >= 0 && !GridMaker.instance.IsStop(currentRow, currentCol - 1, Vector3.left))
                {
                    List<GameObject> movingObject = new List<GameObject>();
                    movingObject.Add(this.gameObject);
                    GameAudioHandler.GetComponent<GameAudioHandler>().PlayMoveSound();

                    for (int c = currentCol - 1; c > 0; c--)
                    {
                        if (GridMaker.instance.IsTherePushableObjectAt(currentRow, c))
                        {
                            movingObject.Add(GridMaker.instance.GetPushableObjectAt(currentRow, c));
                        }
                        else
                        {
                            break;
                        }
                    }
                    foreach (GameObject g in movingObject)
                    {
                        g.transform.position = new Vector3(g.transform.position.x - 1, g.transform.position.y, g.transform.position.z);
                        g.GetComponent<CellProperty>().currentCol--;
                    }
                    GridMaker.instance.CompileRules();
                    CheckWin();
                }
                else if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && currentRow + 1 < GridMaker.instance.Rows && !GridMaker.instance.IsStop(currentRow + 1, currentCol, Vector3.forward))
                {
                    List<GameObject> movingObject = new List<GameObject>();
                    movingObject.Add(this.gameObject);
                    GameAudioHandler.GetComponent<GameAudioHandler>().PlayMoveSound();

                    for (int r = currentRow + 1; r < GridMaker.instance.Rows - 1; r++)
                    {
                        if (GridMaker.instance.IsTherePushableObjectAt(r, currentCol))
                        {
                            movingObject.Add(GridMaker.instance.GetPushableObjectAt(r, currentCol));
                        }
                        else
                        {
                            break;
                        }
                    }
                    foreach (GameObject g in movingObject)
                    {
                        g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z + 1);
                        g.GetComponent<CellProperty>().currentRow++;
                    }
                    GridMaker.instance.CompileRules();
                    CheckWin();
                }
                else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && currentRow - 1 >= 0 && !GridMaker.instance.IsStop(currentRow - 1, currentCol, Vector3.back))
                {
                    List<GameObject> movingObject = new List<GameObject>();
                    movingObject.Add(this.gameObject);
                    GameAudioHandler.GetComponent<GameAudioHandler>().PlayMoveSound();

                    for (int r = currentRow - 1; r >= 0; r--)
                    {
                        if (GridMaker.instance.IsTherePushableObjectAt(r, currentCol))
                        {
                            movingObject.Add(GridMaker.instance.GetPushableObjectAt(r, currentCol));
                        }
                        else
                        {
                            break;
                        }
                    }
                    foreach (GameObject g in movingObject)
                    {
                        g.transform.position = new Vector3(g.transform.position.x, g.transform.position.y, g.transform.position.z - 1);
                        g.GetComponent<CellProperty>().currentRow--;
                    }
                    GridMaker.instance.CompileRules();
                    CheckWin();
                }
            }
        }
    }

    public void CheckWin()
    {
        List<GameObject> objectsAtPlayerPosition = GridMaker.instance.FindObjectsAt(currentRow, currentCol);

        foreach (GameObject g in objectsAtPlayerPosition)
        {
            if (g.GetComponent<CellProperty>().isWin)
            {
                //Debug.Log("Player Won!");
                PlayerPrefs.SetInt("Win", 1);
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                GridMaker.instance.NextLevel();
            }
        }
    }


    public void CheckDestroy()
    {
        List<GameObject> objectsAtPosition = GridMaker.instance.FindObjectsAt(currentRow, currentCol);
        //проверка на свойство Тонуть
        bool sinks = false;
        bool normalObject = false;
        //проверка на свойство Поражение
        bool defeats = false;
        bool playerObject = false;
        //проверка на свойство Плавит и Тает
        bool hotObject = false;
        bool meltingObject = false;
        foreach (GameObject g in objectsAtPosition)
        {
            if (g.GetComponent<CellProperty>().sinksObject)
                sinks = true;
            if (g.GetComponent<CellProperty>().defeatsObject)
                defeats = true;
            if (g.GetComponent<CellProperty>().isPlayer)
                playerObject = true;
            if (!g.GetComponent<CellProperty>().sinksObject)
                normalObject = true;
            if (g.GetComponent<CellProperty>().isHot)
                hotObject = true;
            if (g.GetComponent<CellProperty>().isMelt)
                meltingObject = true;
        }
        //уничтожение управляемых объектов (Поражение)
        if (defeats && playerObject)
        {
            foreach (GameObject g in objectsAtPosition)
            {
                if (!g.GetComponent<CellProperty>().defeatsObject)
                {
                    Destroy(g);
                    GameAudioHandler.GetComponent<GameAudioHandler>().PlayDestroySound();
                }
            }
        }
        //уничтожение объектов, не имеющих свойство Тонуть
        if (sinks && normalObject)
        {
            foreach (GameObject g in objectsAtPosition)
            {
                Destroy(g);
                GameAudioHandler.GetComponent<GameAudioHandler>().PlayDestroySound();
            }
        }
        //уничтожение объектов, имеющих свойство Тает
        if (hotObject && meltingObject)
        {
            foreach (GameObject g in objectsAtPosition)
            {
                if (g.GetComponent<CellProperty>().isMelt)
                {
                    Destroy(g);
                    GameAudioHandler.GetComponent<GameAudioHandler>().PlayDestroySound();
                }
            }
        }
        GridMaker.instance.CheckPlayerObjects();
    }
    public void HighlightCell(bool bruh)
    {
        CellHighlight.SetActive(bruh);
    }
    /*
    public void CheckDefeat()
    {
        List<GameObject> objectsAtPosition = GridMaker.instance.FindObjectsAt(currentRow, currentCol);
        bool defeats = false;
        bool normalObject = false;
        foreach (GameObject g in objectsAtPosition)
        {
            if (!g.GetComponent<CellProperty>().defeatsObject)
            {
                normalObject = true;
            }
            if (g.GetComponent<CellProperty>().defeatsObject)
            {
                defeats = true;
            }
        }
        if (defeats && normalObject)
        {
            foreach (GameObject g in objectsAtPosition)
            {
                if (!g.GetComponent<CellProperty>().defeatsObject)
                    Destroy(g);
            }
        }
    }
    public void CheckMelt()
    {
        List<GameObject> objectsAtPosition = GridMaker.instance.FindObjectsAt(currentRow, currentCol);
        bool hotObject = false;
        bool meltingObject = false;
        foreach (GameObject g in objectsAtPosition)
        {
            if (g.GetComponent<CellProperty>().isHot)
            {
                hotObject = true;
            }
            if (g.GetComponent<CellProperty>().isMelt)
            {
                meltingObject = true;
            }
        }
        if (hotObject && meltingObject)
        {
            foreach (GameObject g in objectsAtPosition)
            {
                if (g.GetComponent<CellProperty>().isMelt)
                    Destroy(g);
            }
        }
    }
    */
}
