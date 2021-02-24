/**
 * Niklas Romero
 *Battle brawllers tile behavor
 * The class handles all tile functionalities
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileScript : MonoBehaviour
{
    public int timesclicked;
    public SpriteRenderer tileRenderer;
    // Start is called before the first frame update
    void Start()
    {
        timesclicked = 0;
        tileRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }
    void OnMouseDown()
    {
        tileRenderer.color = new Color(1, 0, 0, 1);
    }
}
