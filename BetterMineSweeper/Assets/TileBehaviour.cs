using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileBehaviour : MonoBehaviour
{
    public bool isShown = false;
    public bool isBomb = false;
    public bool isFlagged = false;
    public int NumberOfBombsAround;
    public int PositionX, PositionY;
    public GameObject[] AroundTiles;
    [SerializeField]
    private Sprite[] Sprites;
    [SerializeField]
    private Sprite[] Numbers;

    private void Start()
    {
    }

    public void Show()
    {
        if(isShown == false)
        {
            isShown = true;
            if (isFlagged == false)
            {
                if (isBomb == true)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = Sprites[1];
                    StaticLinks.MapGen_Scpt.TriggeredBomb();
                }
                else
                {
                    transform.GetComponent<SpriteRenderer>().sprite = Numbers[NumberOfBombsAround];
                    if (NumberOfBombsAround == 0)
                    {
                        foreach (GameObject x in AroundTiles)
                        {
                            if(x.TryGetComponent<TileBehaviour>(out TileBehaviour script))
                            {
                                script.Show();
                            }
                            
                        }
                    }
                }
            }
        }
        Destroy(transform.GetComponent<TileBehaviour>());
    }

    public void AssignAroundTiles(List<GameObject> x)
    {
        AroundTiles = new GameObject[x.Count];
        AroundTiles = x.ToArray();
    }

    public void SetBomb()
    {
        isBomb = true;
    }
}
