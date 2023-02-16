using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Controls : MonoBehaviour
{
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
                    script.Show();
                }
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU > 16)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU -= 8;
            }
            
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU < 64)
            {
                Camera.main.GetComponent<PixelPerfectCamera>().assetsPPU += 8;
            }
        }
    }
}
