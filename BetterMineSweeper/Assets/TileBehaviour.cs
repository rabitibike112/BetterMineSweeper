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
    [SerializeField]
    public bool isHex;
    [SerializeField]
    private Sprite[] Sprites;
    [SerializeField]
    private Sprite[] Numbers;
    [SerializeField]
    private Sprite[] HexSprites;
    [SerializeField]
    private Sprite[] HexNumbers;

    private bool StartClicking = false;
    private int ClickingIndex = 0;
    RaycastHit2D[] Hits;
    Vector2 Position;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if(StartClicking == true)
        {
            Position = new Vector2(0, 0);
            if(isHex == true)
            {
                switch (ClickingIndex)
                {
                    case 0:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 0.85f), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 2:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 0.76f, transform.position.y + 0.425f), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 0.76f, transform.position.y - 0.425f), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 4:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 0.76f, transform.position.y + 0.425f), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 5:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 0.76f, transform.position.y - 0.425f), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;

                    default:
                        {
                        }
                        break;
                }
                ClickingIndex += 1;

                if (ClickingIndex > 5)
                {
                    StartClicking = false;
                    StaticLinks.MapGen_Scpt.WinCondition();
                }
            }
            else
            {
                switch (ClickingIndex)
                {
                    case 0:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 1, transform.position.y + 1), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 1:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 1), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 2:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 1, transform.position.y + 1), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 1, transform.position.y), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 4:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 5:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 1, transform.position.y - 1), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 6:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    case 7:
                        {
                            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 1, transform.position.y - 1), Vector2.down, 0.1f);
                            if (Hits.Length != 0)
                            {
                                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                                {
                                    script.Show();
                                }
                            }
                        }
                        break;
                    default:
                        {
                        }
                        break;
                }
                ClickingIndex += 1;

                if (ClickingIndex > 7)
                {
                    StartClicking = false;
                    StaticLinks.MapGen_Scpt.WinCondition();
                }
            }
        }
    }

    private void GetNumberOfBombs()
    {
        NumberOfBombsAround = 0;
        RaycastHit2D[] Hits;
        if (isHex == false)
        {

            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 1, transform.position.y + 1), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if(script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 1), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 1, transform.position.y + 1), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 1, transform.position.y), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 1, transform.position.y - 1), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 1), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 1, transform.position.y - 1), Vector2.down, 0.1f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
        }
        else //x0.76 y0.425
        {
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y + 0.85f), Vector2.down, 0.2f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y - 0.85f), Vector2.down, 0.2f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 0.76f, transform.position.y + 0.425f), Vector2.down, 0.2f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 0.76f, transform.position.y + 0.425f), Vector2.down, 0.2f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x + 0.76f, transform.position.y - 0.425f), Vector2.down, 0.2f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
            Hits = Physics2D.RaycastAll(new Vector2(transform.position.x - 0.76f, transform.position.y - 0.425f), Vector2.down, 0.2f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if (script.isBomb == true)
                    {
                        NumberOfBombsAround += 1;
                    }
                }
            }
        }
    }

    public void Show()
    {
        if (StaticLinks.CurrentGamemodeEndless == true)
        {
            AddExtraTiles();
        }
        GetNumberOfBombs();
        if(isShown == false)
        {
            if (isFlagged == false)
            {
                isShown = true;
                if (isBomb == true)
                {
                    if (isHex == true)
                    {
                        transform.GetComponent<SpriteRenderer>().sprite = HexSprites[1];
                    }
                    else
                    {
                        transform.GetComponent<SpriteRenderer>().sprite = Sprites[1];
                    }
                    StaticLinks.MapGen_Scpt.TriggeredBomb(this.gameObject);
                }
                else
                {
                    if(StaticLinks.CurrentGamemodeEndless == true)
                    {
                        StaticLinks.MapGen_Scpt.Score();
                    }
                    if(isHex == true)
                    {
                        transform.GetComponent<SpriteRenderer>().sprite = HexNumbers[NumberOfBombsAround];
                    }
                    else
                    {
                        transform.GetComponent<SpriteRenderer>().sprite = Numbers[NumberOfBombsAround];
                    }
                    
                    if (NumberOfBombsAround == 0)
                    {
                        StartClicking = true;
                    }
                    else
                    {
                        //Destroy(transform.GetComponent<Rigidbody2D>());
                        //Destroy(transform.GetComponent<BoxCollider2D>());
                        StaticLinks.MapGen_Scpt.WinCondition();
                        //Destroy(transform.GetComponent<TileBehaviour>());
                    }
                }
            }
        }  
    }
    private void AddExtraTiles()
    {
        StaticLinks.MapGen_Scpt.GenerateTilesAt(this.transform.position);
    }
    public void ToggleFlag()
    {
        if(isShown == false)
        {
            isFlagged = !isFlagged;
            if (isFlagged == true)
            {
                if (isHex == true)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = HexSprites[2];
                }
                else
                {
                    transform.GetComponent<SpriteRenderer>().sprite = Sprites[2];
                }
            }
            else
            {
                if (isHex == true)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = HexSprites[0];
                }
                else
                {
                    transform.GetComponent<SpriteRenderer>().sprite = Sprites[0];
                }
            }
        }
    }

    public void SetFlagTrue()
    {
        isFlagged = true;
        if(isHex == true)
        {
            transform.GetComponent<SpriteRenderer>().sprite = HexSprites[2];
        }
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite = Sprites[2];
        }
        
    }

    public void UnSetBomb()
    {
        isBomb = false;
    }

    public void SetBomb()
    {
        isBomb = true;
    }
}
