using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuileConfig : MonoBehaviour
{
    public TuileCodex codex;
    void Start()
    {
        codex = GameObject.Find("GameManager").GetComponent<TuileCodex>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
