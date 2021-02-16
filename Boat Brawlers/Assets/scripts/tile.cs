using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public SpriteRenderer tileRenderer;
    // Start is called before the first frame update
    void Start()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Input.GetMouseButtonDown(0))
        {
           tileRenderer.GetComponent<Renderer>().material.color = Color.red;
        }
        
    }
}
