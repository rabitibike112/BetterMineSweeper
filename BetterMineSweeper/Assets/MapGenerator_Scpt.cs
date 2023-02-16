using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MapGenerator_Scpt : MonoBehaviour
{
    [SerializeField]
    GameObject MapTile;
    private int MapSizeX, MapSizeY, NumberOfBombs;
    private GameObject[,] Tiles;
    // Start is called before the first frame update
    void Start()
    {
        StaticStart();
    }

    public void GetGenInfo()
    {
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
                NumberOfBombs = tempBomb;
                break;
            default: SetInputFields(false); MapSizeX = 9; MapSizeY = 9; NumberOfBombs = 10; break;
        }
    }
    public void GenerateMap()
    {
        ResetEverything();
        GetGenInfo();
        Tiles = new GameObject[MapSizeX,MapSizeY];
        for (int x1 = 0; x1 < MapSizeX; x1++)
        {
            for (int x2 = 0; x2 < MapSizeY; x2++)
            {
                Tiles[x1, x2] = Instantiate(MapTile, new Vector3(x1, x2, 0), Quaternion.identity);
                Tiles[x1, x2].transform.name = "" + x1 + " " + x2;
                Tiles[x1, x2].transform.parent = StaticLinks.TileContainer.transform;
                Tiles[x1, x2].GetComponent<TileBehaviour>().PositionX = x1;
                Tiles[x1, x2].GetComponent<TileBehaviour>().PositionY = x2;
            }
        }
        MoveContainerToPossition();
        AddBombs();
        CountBombs();
        CountAroundTiles();
        StaticLinks.Started = true;
    }

    private void AddBombs()
    {
        while(NumberOfBombs > 0)
        {
            for (int x1 = 0; x1 < MapSizeX; x1++)
            {
                for (int x2 = 0; x2 < MapSizeY; x2++)
                {
                    if(NumberOfBombs!=0 && (Random.Range(1, 1000) < 5))
                    {
                        if(Tiles[x1, x2].GetComponent<TileBehaviour>().isBomb == false)
                        {
                            Tiles[x1, x2].GetComponent<TileBehaviour>().SetBomb();
                            NumberOfBombs -= 1;
                        }
                    }
                }
            }
        }
    }

    public void CountBombs()
    {
        for (int x1 = 0; x1 < MapSizeX; x1++)
        {
            for (int x2 = 0; x2 < MapSizeY; x2++)
            {
                int Number = 0;
                if ((x1 - 1 >= 0) && (x2 + 1 < MapSizeY)) // top
                {
                    if (Tiles[x1 - 1, x2 + 1].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                }
                if (x2 + 1 < MapSizeY)
                {
                    if (Tiles[x1, x2 + 1].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                }
                if ((x1 + 1 < MapSizeX) && (x2 + 1 < MapSizeY))
                {
                    if (Tiles[x1 + 1, x2 + 1].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                } // top end
                if (x1 - 1 >= 0) //mid
                {
                    if (Tiles[x1 - 1, x2].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                }
                if (x1 + 1 < MapSizeX)
                {
                    if (Tiles[x1 + 1, x2].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                } // mid end
                if ((x1 - 1 >= 0) && (x2 - 1 >= 0)) // bot
                {
                    if (Tiles[x1 - 1, x2 - 1].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                }
                if (x2 - 1 >= 0)
                {
                    if (Tiles[x1, x2 - 1].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                }
                if ((x1 + 1 < MapSizeX) && (x2 - 1 >= 0))
                {
                    if (Tiles[x1 + 1, x2 - 1].GetComponent<TileBehaviour>().isBomb == true)
                    {
                        Number += 1;
                    }
                } // bot end
                Tiles[x1, x2].GetComponent<TileBehaviour>().NumberOfBombsAround = Number;
            }
        }
    }

    public void CountAroundTiles()
    {
        List<GameObject> AroundTiles = new();
        for (int x1 = 0; x1 < MapSizeX; x1++)
        {
            for (int x2 = 0; x2 < MapSizeY; x2++)
            {
                AroundTiles.Clear();
                if ((x1 - 1 >= 0) && (x2 + 1 < MapSizeY)) // top
                {
                    AroundTiles.Add(Tiles[x1-1, x2+1]);
                }
                if (x2 + 1 < MapSizeY)
                {
                    AroundTiles.Add(Tiles[x1, x2+1]);
                }
                if ((x1 + 1 < MapSizeX) && (x2 + 1 < MapSizeY))
                {
                    AroundTiles.Add(Tiles[x1+1, x2+1]);
                } // top end
                if (x1 - 1 >= 0) //mid
                {
                    AroundTiles.Add(Tiles[x1-1, x2]);
                }
                if (x1 + 1 < MapSizeX)
                {
                    AroundTiles.Add(Tiles[x1+1, x2]);
                } // mid end
                if ((x1 - 1 >= 0) && (x2 - 1 >= 0)) // bot
                {
                    AroundTiles.Add(Tiles[x1 - 1, x2 - 1]);
                }
                if (x2 - 1 >= 0)
                {
                    AroundTiles.Add(Tiles[x1, x2-1]);
                }
                if ((x1 + 1 < MapSizeX) && (x2 - 1 >= 0))
                {
                    AroundTiles.Add(Tiles[x1 + 1, x2-1]);
                } // bot end
                Tiles[x1, x2].GetComponent<TileBehaviour>().AssignAroundTiles(AroundTiles);
            }
        }
    }

    private void MoveContainerToPossition()
    {
        StaticLinks.TileContainer.transform.position = new Vector3(-MapSizeX / 2, -MapSizeY / 2, 0);
    }
    public void ResetEverything()
    {
        StaticLinks.TileContainer.transform.position = new Vector3(0, 0, 0);
        if (StaticLinks.TileContainer.transform.childCount > 0)
        {
            for (int zero = 0; zero < StaticLinks.TileContainer.transform.childCount; zero++)
            {
                Destroy(StaticLinks.TileContainer.transform.GetChild(zero).gameObject);
            }
        }
    }

    public void TriggeredBomb()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
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
}
