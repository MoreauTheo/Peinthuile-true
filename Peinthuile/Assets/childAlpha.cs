using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class childAlpha : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Image>().color = new Vector4(255,255,255,GetComponent<Image>().color.a);

        }
    }
}
