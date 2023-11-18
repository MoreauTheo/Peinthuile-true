using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuileCodex : MonoBehaviour
{
    public GameObject V1;
    public GameObject F1;
    public GameObject C1;
    public GameObject CV1;
    public GameObject CF1;
    public GameObject FV1;
    public Dictionary<string, GameObject> TouteTuiles = new Dictionary<string, GameObject>();
    void Start()
    {
        TouteTuiles.Add("F1", F1);
        TouteTuiles.Add("V1", V1);
        TouteTuiles.Add("C1", C1);
        TouteTuiles.Add("CV1", CV1);
        TouteTuiles.Add("CF1", CF1);
        TouteTuiles.Add("FV1",FV1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
