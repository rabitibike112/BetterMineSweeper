using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Controls : MonoBehaviour
{
    Vector3 Pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && StaticLinks.Started == true)
        {
            Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] Hits = Physics2D.RaycastAll(Position, Vector2.down, 0.5f);
            if (Hits.Length != 0)
            {
                if(Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if(StaticLinks.FirstClick == true && script.isBomb == true)
                    {
                        script.UnSetBomb();
                        if(StaticLinks.CurrentGamemodeEndless == false)
                        {
                            StaticLinks.MapGen_Scpt.AddOneBomb();
                        }
                    }
                    script.Show();
                }
            }
            if(StaticLinks.FirstClick == true)
            {
                StaticLinks.MapGen_Scpt.StartTimerTicker();
            }
            StaticLinks.FirstClick = false;
        }

        if (Input.GetMouseButtonDown(1) && StaticLinks.Started == true)
        {
            Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] Hits = Physics2D.RaycastAll(Position, Vector2.down, 0.5f);
            if (Hits.Length != 0)
            {
                if (Hits[0].transform.TryGetComponent(out TileBehaviour script))
                {
                    if(script.isShown == false)
                    {
                        if(StaticLinks.CurrentGamemodeEndless == true)
                        {
                            script.ToggleFlag();
                        }
                        else
                        {
                            if (script.isFlagged == false)
                            {
                                if (StaticLinks.MapGen_Scpt.FlagsRemaining > 0)
                                {
                                    script.ToggleFlag();
                                    StaticLinks.MapGen_Scpt.Flag(-1);
                                }
                            }
                            else
                            {
                                script.ToggleFlag();
                                StaticLinks.MapGen_Scpt.Flag(1);
                            }
                        }
                    }   
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU > 18)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU -= 6;
            }
            
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU < 96)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU += 6;
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            Pos = StaticLinks.RefCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            Pos.z = -10;
        }
        if (Input.GetMouseButton(2))
        {
            Vector3 MousePos = StaticLinks.RefCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = -10;
            Vector3 temp = StaticLinks.RefCamera.transform.position + (Pos - MousePos);
            temp.z = -10;
            Camera.main.transform.position = temp;
            //transform.position = temp;
        }
        if (Input.GetMouseButtonUp(2))
        {
            StaticLinks.RefCamera.transform.position = Camera.main.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
            StaticLinks.RefCamera.transform.position = new Vector3(0, 0, -10);
        }
    }
}
