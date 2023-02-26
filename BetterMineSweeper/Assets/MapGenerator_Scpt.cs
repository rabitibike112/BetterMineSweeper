using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MapGenerator_Scpt : MonoBehaviour
{
    [SerializeField]
    GameObject MapTileSquare;
    [SerializeField]
    GameObject MapTileHex;
    private int Type = 0;
    // Tile types end
    private int BombsTodeath = 0;
    // Bombs to death end
    private int Gamemode = 1;
    // gamemode end
    private int MapSizeX, MapSizeY, NumberOfBombs;
    // map size end
    private List<GameObject> Tiles = new List<GameObject>();
    //private GameObject[,] Tiles;
    private WaitForSecondsRealtime OneSecond = new WaitForSecondsRealtime(1f);
    public int SecondsSinceStart = 0;
    public int NonBombTilesRemaining = -1;
    public int FlagsRemaining;
    [SerializeField]
    public Sprite[] NumberIcons;
    private GameObject T1, T10, T100, T1000;
    private GameObject F1, F10, F100, F1000;
    public List<GameObject> ListOfBombTiles;
    public bool BombTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        StaticStart();
        T1 = StaticLinks.TimeCounter.transform.Find("T1").gameObject;
        T10 = StaticLinks.TimeCounter.transform.Find("T10").gameObject;
        T100 = StaticLinks.TimeCounter.transform.Find("T100").gameObject;
        T1000 = StaticLinks.TimeCounter.transform.Find("T1000").gameObject;
        F1 = StaticLinks.FlagCounter.transform.Find("F1").gameObject;
        F10 = StaticLinks.FlagCounter.transform.Find("F10").gameObject;
        F100 = StaticLinks.FlagCounter.transform.Find("F100").gameObject;
        F1000 = StaticLinks.FlagCounter.transform.Find("F1000").gameObject;
    }

    public void GetGenInfo()
    {
        //Tile type
        switch (StaticLinks.TopBCG.transform.Find("Tiles").GetComponent<TMP_Dropdown>().value)
        {
            case 0: Type = 0; break; // Square
            case 1: Type = 1; break; // Hex
            default: Type = 0; break; // Square
        }
        //Bombs to death
        switch (StaticLinks.TopBCG.transform.Find("Difficulty").GetComponent<TMP_Dropdown>().value)
        {
            case 0: BombsTodeath = 1; break;
            case 1: BombsTodeath = 3; break;
            case 2: BombsTodeath = 5; break;
            case 3: BombsTodeath = 1000; break;
            default: BombsTodeath = 1; break;
        }
        //Gamemode Select
        switch (StaticLinks.TopBCG.transform.Find("Mode").GetComponent<TMP_Dropdown>().value)
        {
            case 0: Gamemode = 0; break; // normal
            case 1: Gamemode = 1; break; // wrap around
            case 2: Gamemode = 2; break; // endless
            default: Gamemode = 0; break;
        }

        switch (StaticLinks.UI_DropDown.GetComponent<TMP_Dropdown>().value)
        {
            case 0: SetInputFields(false); MapSizeX = 9; MapSizeY = 9; NumberOfBombs = 10; break;
            case 1: SetInputFields(false); MapSizeX = 16; MapSizeY = 16; NumberOfBombs = 40; break;
            case 2: SetInputFields(false); MapSizeX = 30; MapSizeY = 16; NumberOfBombs = 99; break;
            case 3: SetInputFields(true);
                int.TryParse(StaticLinks.InputFieldX.GetComponent<TMP_InputField>().text, out int tempX);
                MapSizeX = tempX;
                int.TryParse(StaticLinks.InputFieldY.GetComponent<TMP_InputField>().text, out int tempY);
                MapSizeY = tempY;
                int.TryParse(StaticLinks.InputFieldBomb.GetComponent<TMP_InputField>().text, out int tempBomb);
                NumberOfBombs = Mathf.RoundToInt((MapSizeX * MapSizeY) * (Mathf.Clamp(tempBomb, 5, 40) * 0.01f));
                break;
            default: SetInputFields(false); MapSizeX = 9; MapSizeY = 9; NumberOfBombs = 10; break;
        }
        FlagsRemaining = NumberOfBombs;
    }
    public void GenerateMap()
    {
        ResetEverything();
        GetGenInfo();
        if (Type == 0)
        {
            for (int x1 = 0; x1 < MapSizeX; x1++)
            {
                for (int x2 = 0; x2 < MapSizeY; x2++)
                {
                    GameObject temp = Instantiate(MapTileSquare, new Vector3(x1, x2, 0), Quaternion.identity);
                    Tiles.Add(temp);
                    temp.transform.name = "" + x1 + " " + x2;
                    temp.transform.parent = StaticLinks.TileContainer.transform;
                    temp.GetComponent<TileBehaviour>().PositionX = x1;
                    temp.GetComponent<TileBehaviour>().PositionY = x2;
                    temp = null;
                }
            }
        }
        else if (Type == 1)
        {
            for (int x1 = 0; x1 < MapSizeX; x1++)
            {
                if (x1 % 2 == 0)
                {
                    for (int x2 = 0; x2 < MapSizeY; x2++)
                    {
                        GameObject temp = Instantiate(MapTileHex, new Vector3(x1 * 0.76f, x2 * 0.85f, 0), Quaternion.identity);
                        Tiles.Add(temp);
                        temp.transform.name = "" + x1 + " " + x2;
                        temp.transform.parent = StaticLinks.TileContainer.transform;
                        temp.GetComponent<TileBehaviour>().PositionX = x1;
                        temp.GetComponent<TileBehaviour>().PositionY = x2;
                    }
                }
                else
                {
                    for (int x2 = -1; x2 < MapSizeY; x2++)
                    {
                        GameObject temp = Instantiate(MapTileHex, new Vector3(x1 * 0.76f, (x2 * 0.85f) + 0.425f, 0), Quaternion.identity);
                        Tiles.Add(temp);
                        temp.transform.name = "" + x1 + " " + x2;
                        temp.transform.parent = StaticLinks.TileContainer.transform;
                        temp.GetComponent<TileBehaviour>().PositionX = x1;
                        temp.GetComponent<TileBehaviour>().PositionY = x2;
                    }
                }
            }
        }
        MoveContainerToPossition();
        AddBombs();
        //CountBombs();
        UpdateFlagCounter();
        StaticLinks.Started = true;
        StartCoroutine(TimerTicker());
    }

    public void WinCondition()
    {
        //Debug.LogError(NonBombTilesRemaining);
        NonBombTilesRemaining -= 1;
        if(NonBombTilesRemaining == 0)
        {
            StopAllCoroutines();
            foreach(GameObject x in ListOfBombTiles)
            {
                if(x.transform.TryGetComponent<TileBehaviour>(out TileBehaviour script))
                {
                    script.SetFlagTrue();
                }
            }
            StaticLinks.HappyFaceText.GetComponent<TMP_Text>().text = "8)";
            StaticLinks.Started = false;
        }
    }

    public void UpdateFlagCounter()
    {
        F1.GetComponent<Image>().sprite = NumberIcons[FlagsRemaining % 10];
        F10.GetComponent<Image>().sprite = NumberIcons[(FlagsRemaining / 10) % 10];
        F100.GetComponent<Image>().sprite = NumberIcons[(FlagsRemaining / 100) % 10];
        F1000.GetComponent<Image>().sprite = NumberIcons[(FlagsRemaining / 1000) % 10];
    }

    public void Flag(int x)
    {
        FlagsRemaining += x;
        UpdateFlagCounter();
    }

    private void AddBombs()
    {
        NonBombTilesRemaining = Tiles.Count - NumberOfBombs;
        ListOfBombTiles = new();
        GameObject[] TempTiles = Tiles.ToArray();
        while (NumberOfBombs > 0)
        {
            for(int x1 = 0; x1 < TempTiles.Length; x1++)
            {
                if(NumberOfBombs!=0 && (Random.Range(1, 1000) == 1))
                {
                    if(TempTiles[x1].GetComponent<TileBehaviour>().isBomb == false)
                    {
                        TempTiles[x1].GetComponent<TileBehaviour>().SetBomb();
                        ListOfBombTiles.Add(TempTiles[x1]);
                        NumberOfBombs -= 1;
                    }
                }
            }
        }
        Debug.LogError(NonBombTilesRemaining);
    }

    private void MoveContainerToPossition()
    {
        StaticLinks.TileContainer.transform.position = new Vector3(-MapSizeX / 2, -MapSizeY / 2, 0);
    }
    public void ResetEverything()
    {
        Tiles.Clear();
        StaticLinks.TileContainer.transform.position = new Vector3(0, 0, 0);
        if (StaticLinks.TileContainer.transform.childCount > 0)
        {
            for (int zero = 0; zero < StaticLinks.TileContainer.transform.childCount; zero++)
            {
                Destroy(StaticLinks.TileContainer.transform.GetChild(zero).gameObject);
            }
        }
        StopAllCoroutines();
        NonBombTilesRemaining = -1;
        SecondsSinceStart = 0;
        ListOfBombTiles.Clear();
        StaticLinks.HappyFaceText.GetComponent<TMP_Text>().text = ":)";
    }

    public void TriggeredBomb(GameObject bombTile)
    {
        if(BombTriggered == false)
        {
            bombTile.GetComponent<SpriteRenderer>().color = Color.red;
        }
        BombTriggered = true;
        foreach (GameObject x in ListOfBombTiles)
        {
            if(x.TryGetComponent<TileBehaviour>(out TileBehaviour script))
            {
                script.Show();
            }
        }
        StaticLinks.HappyFaceText.GetComponent<TMP_Text>().text = ":(";
        StaticLinks.Started = false;
        StopAllCoroutines();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator TimerTicker()
    {
        while (true)
        {
            yield return OneSecond;
            SecondsSinceStart += 1;
            UpdateTimeTickerUi();
        }
    }

    private void UpdateTimeTickerUi()
    {
        T1.GetComponent<Image>().sprite = NumberIcons[SecondsSinceStart % 10];
        T10.GetComponent<Image>().sprite = NumberIcons[(SecondsSinceStart/10) % 10];
        T100.GetComponent<Image>().sprite = NumberIcons[(SecondsSinceStart / 100) % 10];
        T1000.GetComponent<Image>().sprite = NumberIcons[(SecondsSinceStart / 1000) % 10];
    }

    public void SetInputFields(bool x)
    {
        StaticLinks.InputFieldX.SetActive(x);
        StaticLinks.InputFieldY.SetActive(x);
        StaticLinks.InputFieldBomb.SetActive(x);
    }

    private void StaticStart()
    {
        StaticLinks.Started = false;
        StaticLinks.UI_DropDown = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("Dropdown").gameObject;
        StaticLinks.MapGen_Scpt = GameObject.Find("MapGenerator").gameObject.GetComponent<MapGenerator_Scpt>();
        StaticLinks.TileContainer = GameObject.Find("TileCont").gameObject;
        StaticLinks.InputFieldX = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("InputX").gameObject;
        StaticLinks.InputFieldY = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("InputY").gameObject;
        StaticLinks.InputFieldBomb = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("InputBomb").gameObject;
        StaticLinks.FlagCounter = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("FlagParent").gameObject;
        StaticLinks.TimeCounter = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("TimeParent").gameObject;
        StaticLinks.HappyFaceText = GameObject.Find("Canvas").transform.Find("TopBCG").transform.Find("Generate button").transform.GetChild(0).gameObject;
        StaticLinks.RefCamera = GameObject.Find("RefCamera").gameObject;
        StaticLinks.TopBCG = GameObject.Find("Canvas").transform.Find("TopBCG").gameObject;
        SetInputFields(false);
    }
}

public static class StaticLinks
{
    public static bool Started { get; set; }
    public static GameObject UI_DropDown { get; set; }
    public static MapGenerator_Scpt MapGen_Scpt { get; set; }
    public static GameObject TileContainer { get; set; }
    public static GameObject InputFieldX { get; set; }
    public static GameObject InputFieldY { get; set; }
    public static GameObject InputFieldBomb { get; set; }
    public static GameObject FlagCounter { get; set; }
    public static GameObject TimeCounter { get; set; }
    public static GameObject HappyFaceText { get; set; }
    public static GameObject RefCamera { get; set; }
    public static GameObject TopBCG { get; set; }
}
