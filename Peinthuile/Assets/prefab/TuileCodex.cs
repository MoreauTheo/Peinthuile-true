using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuileCodex : MonoBehaviour
{
    public GameObject V1;
    public GameObject V2;
    public GameObject V3;

    public GameObject F1;
    public GameObject F2;
    public GameObject F3;

    public GameObject C1;
    public GameObject C2;
    public GameObject C3;

    public GameObject E1;
    public GameObject E2;
    public GameObject E3;

    public GameObject CV1;
    public GameObject CF1;
    public GameObject FV1;
    public Dictionary<string, GameObject> TouteTuiles = new Dictionary<string, GameObject>();
    void Start()
    {
        TouteTuiles.Add("F1", F1);
        TouteTuiles.Add("F2", F2);
        TouteTuiles.Add("F3", F3);

        TouteTuiles.Add("V1", V1);
        TouteTuiles.Add("V2", V2);
        TouteTuiles.Add("V3", V3);

        TouteTuiles.Add("C1", C1);
        TouteTuiles.Add("C2", C2);
        TouteTuiles.Add("C3", C3);

        TouteTuiles.Add("E1", E1);
        TouteTuiles.Add("E2", E2);
        TouteTuiles.Add("E3", E3);

        TouteTuiles.Add("CV1", CV1);
        TouteTuiles.Add("CF1", CF1);
        TouteTuiles.Add("FV1",FV1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
