using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuileCodex : MonoBehaviour
{
    public GameObject V1;
    public GameObject V2;
    public GameObject V3;
    public GameObject V4;

    public GameObject F1;
    public GameObject F2;
    public GameObject F3;
    public GameObject F4;

    public GameObject C1;
    public GameObject C2;
    public GameObject C3;
    public GameObject C4;

    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;

    public GameObject CV1;
    public GameObject CV2;
    public GameObject CV3;
    public GameObject CV4;

    public GameObject CF1;
    public GameObject CF2;
    public GameObject CF3;
    public GameObject CF4;

    public GameObject FV1;
    public GameObject FV2;
    public GameObject FV3;
    public GameObject FV4;

    public GameObject EV1;
    public GameObject EV2;
    public GameObject EV3;
    public GameObject EV4;

    public GameObject CE1;
    public GameObject CE2;
    public GameObject CE3;
    public GameObject CE4;

    public GameObject EF1;
    public GameObject EF2;
    public GameObject EF3;
    public GameObject EF4;

    public Dictionary<string, GameObject> TouteTuiles = new Dictionary<string, GameObject>();
    void Start()
    {
        TouteTuiles.Add("F1", F1);
        TouteTuiles.Add("F2", F2);
        TouteTuiles.Add("F3", F3);
        TouteTuiles.Add("F4", F4);

        TouteTuiles.Add("V1", V1);
        TouteTuiles.Add("V2", V2);
        TouteTuiles.Add("V3", V3);
        TouteTuiles.Add("V4", V4);

        TouteTuiles.Add("C1", C1);
        TouteTuiles.Add("C2", C2);
        TouteTuiles.Add("C3", C3);
        TouteTuiles.Add("C4", C4);

        TouteTuiles.Add("E1", E1);
        TouteTuiles.Add("E2", E2);
        TouteTuiles.Add("E3", E3);
        TouteTuiles.Add("E4", E4);

        TouteTuiles.Add("CV1", CV1);
        TouteTuiles.Add("CV2", CV2);
        TouteTuiles.Add("CV3", CV3);
        TouteTuiles.Add("CV4", CV4);

        TouteTuiles.Add("CF1", CF1);
        TouteTuiles.Add("CF2", CF2);
        TouteTuiles.Add("CF3", CF3);
        TouteTuiles.Add("CF4", CF4);

        TouteTuiles.Add("FV1", FV1);
        TouteTuiles.Add("FV2", FV2);
        TouteTuiles.Add("FV3", FV3);
        TouteTuiles.Add("FV4", FV4);

        TouteTuiles.Add("EV1", EV1);
        TouteTuiles.Add("EV2", EV2);
        TouteTuiles.Add("EV3", EV3);
        TouteTuiles.Add("EV4", EV4);

        TouteTuiles.Add("EF1", EF1);
        TouteTuiles.Add("EF2", EF2);
        TouteTuiles.Add("EF3", EF3);
        TouteTuiles.Add("EF4", EF4);

        TouteTuiles.Add("CE1", CE1);
        TouteTuiles.Add("CE2", CE2);
        TouteTuiles.Add("CE3", CE3);
        TouteTuiles.Add("CE4", CE4);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
