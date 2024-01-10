using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Grabber : MonoBehaviour
{
    private GameObject PreviewTile;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI piocheText;
    private GridBoard board;
    private GameObject selectedObject;
    public int pposx;
    public int pposy;
    private int level;
    private TuileCodex codex;
    public string newtag;
    private PiocheScript pioche;
    public bool pose;
    public float score;
    public float scorePreview;
    public GameObject PreviewTuilePlateau;
    public GameObject stock;
    public int palierActu = 1;
    public float step = 10;
    public float lastep;
    public Color sombre;
    public Image chart;
    public int alphaSpeed;
    public AudioManager audio;
    public float timingLerp;
    public GameObject ABop;
    public GameObject eau;
    public GameObject village;
    public GameObject champ;
    public GameObject foret;
    public GameObject fv;
    public GameObject ef;
    public GameObject ev;
    public GameObject cv;
    public GameObject cf;
    public GameObject ce;
    public Image bar;
    // Start is called before the first frame update
    void Start()
    {
        PreviewTile = GameObject.Find("PreviewTile");
        score = 0;
        board = GetComponent<GridBoard>();
        codex = GetComponent<TuileCodex>();
        pioche = GetComponent<PiocheScript>();
        checkBoardLvlUp();
        
        pioche.Draw();

        ChangePreview();

        audio.Play("Theme");

    }
    // Update is called once per frame
    void Update()
    {

        bar.fillAmount = (score-lastep) / (step-lastep);
        if (timingLerp > 0)
        {
            Bop(ABop);
            timingLerp -= Time.deltaTime*5;
        }
        if (pioche.nbPioche > 0)
        {
            if(Input.GetKey(KeyCode.Tab)) 
            {
                if(sombre.a <0.96f)
                {
                    sombre.a += alphaSpeed * Time.deltaTime;
                    if (sombre.a > 0.96f)
                        sombre.a = 0.96f;

                }
            }
            else
            {
                if (sombre.a >0)
                {
                    sombre.a -= alphaSpeed * Time.deltaTime;
                    
                }
            }
            
            chart.color = sombre;

            RaycastHit hit = CastRay();
            if (hit.collider)
            {
                if (Input.GetMouseButtonDown(0))
                {
                
                    selectedObject = hit.collider.gameObject;
                    if (selectedObject.tag == "vide")
                    {
                        PoseTuile(pioche.current,selectedObject);

                        
                        score += Scoring(pioche.current, selectedObject.GetComponent<TuileConfig>().posx, selectedObject.GetComponent<TuileConfig>().posy);
                    }
                    else if (GetTag(selectedObject.tag) != GetTag(pioche.current) && selectedObject.tag.Length < 3)
                    {

                        audio.Play("Fusion");
                        timingLerp = 1;
                        PoseTuile(FusionneTuile(pioche.current), selectedObject);
                    }
                    if (pose)
                    {
                        
                        NextTurn();
                        pose = false;
                    }
                }
                else
                {
                    selectedObject = hit.collider.gameObject;
                    if(PreviewTuilePlateau == null)
                    {
                        if (selectedObject.tag == "vide")
                        {
                            ChangeRender(false, selectedObject);
                            stock = selectedObject;
                            PreviewTuilePlateau = Instantiate(codex.TouteTuiles[pioche.current], selectedObject.transform.position, selectedObject.transform.rotation);
                            PreviewTuilePlateau.GetComponent<Collider>().enabled = false;

                        }
                        else if (GetTag(selectedObject.tag) != GetTag(pioche.current) &&  selectedObject.tag.Length != 3)
                        {
                            ChangeRender(false, selectedObject);
                            stock = selectedObject;
                            PreviewTuilePlateau = Instantiate(codex.TouteTuiles[FusionneTuile(pioche.current)], selectedObject.transform.position, selectedObject.transform.rotation);
                            PreviewTuilePlateau.GetComponent<Collider>().enabled = false;
                        }
                    }
                    else  
                    {
                        if (selectedObject.tag == "vide")
                        {
                            ChangeRender(true, stock);
                            ChangeRender(false, selectedObject);
                            stock = selectedObject;
                            if (PreviewTuilePlateau.tag.Length > 2)
                            {
                                Destroy(PreviewTuilePlateau);
                                Debug.Log("ca se lance");
                                PreviewTuilePlateau = Instantiate(codex.TouteTuiles[pioche.current], selectedObject.transform.position, selectedObject.transform.rotation);
                                PreviewTuilePlateau.GetComponent<Collider>().enabled = false;

                            }
                            else
                            {
                                PreviewTuilePlateau.transform.position = selectedObject.transform.position;
                            }
                            scorePreview = ScoringPreview(pioche.current, selectedObject.GetComponent<TuileConfig>().posx, selectedObject.GetComponent<TuileConfig>().posy);
                        }
                        else if (selectedObject.tag.Length == 3)
                        {
                            if (stock)
                                ChangeRender(true, stock);
                            stock = null;
                            Destroy(PreviewTuilePlateau);
                            PreviewTuilePlateau = null;
                            scorePreview = 0;
                        }
                        else if (selectedObject.tag.Length == 2 && GetTag(selectedObject.tag) != GetTag(pioche.current))
                        {
                            if (PreviewTuilePlateau.tag == FusionneTuile(pioche.current))
                            {

                                PreviewTuilePlateau.transform.position = selectedObject.transform.position;
                                ChangeRender(true, stock);
                                ChangeRender(false, selectedObject);
                                stock = selectedObject;
                            }
                            else
                            {
                                ChangeRender(true, stock);
                                Destroy(PreviewTuilePlateau);
                                ChangeRender(false, selectedObject);
                                stock = selectedObject;
                                PreviewTuilePlateau = Instantiate(codex.TouteTuiles[FusionneTuile(pioche.current)], selectedObject.transform.position, selectedObject.transform.rotation);
                                PreviewTuilePlateau.GetComponent<Collider>().enabled = false;
                            }
                            scorePreview = 0;
                        }
                        else 
                        {
                            if (stock)
                                ChangeRender(true, stock);
                            stock = null;
                            Destroy(PreviewTuilePlateau);
                            PreviewTuilePlateau = null;
                            scorePreview = ScoringPreview(pioche.current, selectedObject.GetComponent<TuileConfig>().posx, selectedObject.GetComponent<TuileConfig>().posy);

                        }
                    }
                }
                
                scoreText.text = score.ToString() + " + " + scorePreview + " / " + step.ToString();
            }
            else
            {
                if(stock)
                    ChangeRender(true, stock);
                stock = null;
                Destroy(PreviewTuilePlateau);
                PreviewTuilePlateau = null;
                scorePreview = 0;
            }
        }
        else
        {
            Debug.Log("perdu");
        }
    }

    public void ChangeRender(bool targetState,GameObject Target)
    {
        if (Target)
        {
            if (Target.GetComponent<MeshRenderer>())
                Target.GetComponent<MeshRenderer>().enabled = targetState;
            foreach (Transform child in Target.transform)
            {
                if (child.gameObject.GetComponent<MeshRenderer>())
                    child.gameObject.GetComponent<MeshRenderer>().enabled = targetState;
            }
        }
    }
     
    public void Bop(GameObject boped)
    {
        if(boped)
            {
            if (timingLerp <= 0.5f)
                boped.transform.position = Vector3.Lerp(new Vector3(boped.transform.position.x, 0, boped.transform.position.z), new Vector3(boped.transform.position.x, -0.3f, boped.transform.position.z), Mathf.Sqrt(1 - Mathf.Pow(timingLerp * 2 - 1, 2)));
            else
                boped.transform.position = Vector3.Lerp(new Vector3(boped.transform.position.x, -0.3f, boped.transform.position.z), new Vector3(boped.transform.position.x, 0, boped.transform.position.z), (timingLerp - 0.5f) * 2);
        }
    }
    public void ApplyScoring()
    {
        if(score >= step)
        {
            palierActu++;
            lastep = step;
            step += palierActu * 8;
            pioche.nbPioche += 10;
            audio.Play("Tuile");

        }
        scoreText.text = score.ToString() + " / " + step.ToString();
        piocheText.text = (pioche.nbPioche-1).ToString();

    }

    public void NextTurn()
    {
        checkBoardLvlUp();
        ApplyScoring();
        pioche.Draw();
        ChangePreview();
    }

    private RaycastHit CastRay()//permet de tirer un rayon la ou le joueur clic peut importe la direction de la camera
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        //transformation des position ecran en position monde
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        //tir du raycast
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }

    public void PoseTuile(string tuileAPoser,GameObject aRemplacer)
    {
        TuileConfig cibleScript = aRemplacer.GetComponent<TuileConfig>();
        board.grille[cibleScript.posx,cibleScript.posy] = Instantiate(codex.TouteTuiles[tuileAPoser], aRemplacer.transform.position, aRemplacer.transform.rotation);
        if(timingLerp <=0)
            ABop = board.grille[cibleScript.posx, cibleScript.posy];
        timingLerp = 1;
        board.grille[cibleScript.posx, cibleScript.posy].transform.Rotate(new Vector3(0, 0,60 * UnityEngine.Random.Range(1, 7)));
        board.grille[cibleScript.posx, cibleScript.posy].GetComponent<TuileConfig>().posx = cibleScript.posx;
        board.grille[cibleScript.posx, cibleScript.posy].GetComponent<TuileConfig>().posy = cibleScript.posy;
        if (GetTag(tuileAPoser) == "V")
            village.SetActive(true);
        else if(GetTag(tuileAPoser) == "F")
            foret.SetActive(true);
        else if(GetTag(tuileAPoser) == "E")
            eau.SetActive(true);
        else if(GetTag(tuileAPoser) == "C")
            champ.SetActive(true);
        else if (GetTag(tuileAPoser) == "FV")
            fv.SetActive(true);
        else if (GetTag(tuileAPoser) == "EF")
            ef.SetActive(true);
        else if (GetTag(tuileAPoser) == "CF")
            cf.SetActive(true);
        else if (GetTag(tuileAPoser) == "EV")
            ev.SetActive(true);
        else if (GetTag(tuileAPoser) == "CE")
            ce.SetActive(true);
        else if (GetTag(tuileAPoser) == "CV")
            cv.SetActive(true);








        if (GetLevelTile(tuileAPoser) == 1 && tuileAPoser.Length <3)
        {
            if(tuileAPoser.Length < 3)
                audio.Play(tuileAPoser);
            else
                audio.Play("Fusion");
        }
        Destroy(aRemplacer);
       // board.grille[cibleScript.posx, cibleScript.posy].GetComponent<Animator>().SetTrigger("Pose");
        pose = true;
    }

    public string FusionneTuile(string tuileAPoser)
    {
        level =  GetLevelTile(selectedObject.tag);
        newtag = GetTag(selectedObject.tag);
        newtag = newtag + codex.TouteTuiles[tuileAPoser].tag.Substring(0, codex.TouteTuiles[tuileAPoser].tag.Length - 1);
        newtag = Alphabetize(newtag);
        newtag += level;
        return newtag ;

    }

    public float ScoringPreview(string tuilePose, int x, int y)
    {
        float scoreSup = 0;
        foreach (GameObject check in checkAround(x, y))
        {
            if (check.tag != "vide")
            {
                scoreSup += GetLevelTile(check.tag);

            }
            if (GetTag(tuilePose) == "F" && GetTag(check.tag).Contains("V"))
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "V" && GetTag(check.tag).Contains("E"))
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "C" && GetTag(check.tag).Contains("F"))
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "E" && GetTag(check.tag).Contains("C"))
            {
                scoreSup += GetLevelTile(check.tag);
            }

        }
        return scoreSup;
    }

    public float Scoring(string tuilePose,int x,int y)
    {
        float scoreSup = 0;
        foreach(GameObject check in checkAround(x,y))
        {
            if(check.tag != "vide")
            {
                scoreSup += GetLevelTile(check.tag);
                
            }
            if(GetTag(tuilePose) == "F" && GetTag(check.tag).Contains("V"))
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "V" && GetTag(check.tag).Contains("E"))
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "C" && GetTag(check.tag).Contains("F"))
            {
                scoreSup += GetLevelTile(check.tag);
            }
            if (GetTag(tuilePose) == "E" && GetTag(check.tag).Contains("C"))
            {
                scoreSup += GetLevelTile(check.tag);
            }

        }
        return scoreSup;
    }
    

    public string Alphabetize(string s)
    {
        // Convert to char array.
        // Convert to char array.
        char[] a = s.ToCharArray();

        // Sort letters.
        Array.Sort(a);

        // Return modified string.
        return new string(a);
    }

    public void checkBoardLvlUp()
    {
        
        foreach(GameObject check in board.grille)
        {
            if(check.tag != "vide")
            {
                if (GetLevelTile(check.tag) < 3)
                {
                    int posix = check.GetComponent<TuileConfig>().posx;
                    int posiy = check.GetComponent<TuileConfig>().posy;
                    int newLevel = GetLevelTile(check.tag) + 1;
                    bool canLevelUp = true;
                    foreach (char biome in GetTag(check.tag))
                    {
                        
                        if (biome.ToString() == "F")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "C", "E", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }
                        if (biome.ToString() == "C")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "E", "V", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }
                        if (biome.ToString() == "E")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "V", "F", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }
                        if (biome.ToString() == "V")
                        {
                            if (!CheckTileIndividualy(posix, posiy, "F", "C", newLevel))
                            {

                                canLevelUp = false;
                            }
                        }

                    }
                    if(canLevelUp)
                    {
                        PoseTuile(GetTag(check.tag) + newLevel, check);
                    }
                }
            }
        }
    }
    public void ChangePreview()
    {
        GameObject asupr = PreviewTile;
        PreviewTile = Instantiate(codex.TouteTuiles[pioche.current], PreviewTile.transform.position, PreviewTile.transform.rotation);
        PreviewTile.layer = LayerMask.NameToLayer("Preview");
        foreach(Transform child in PreviewTile.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Preview");
        }
        Destroy(asupr );
    }
    public bool CheckTileIndividualy(int x,int y,string tagCheck, string tagCount,int levelUp)
    {
        bool checkUpgrade = false;
        int levelMini = 0;
        foreach (GameObject check in checkAround(x,y))
        {
            if(GetTag(check.tag) == tagCheck)
            {
                checkUpgrade = true;
            }
            if(GetTag(check.tag) == tagCount)
            {
                levelMini += GetLevelTile(check.tag);
            }
        }
        if(checkUpgrade && levelMini >= levelUp)
            return true;
        else
            return false;
        
    }


    public List<GameObject> checkAround(int x, int y)
    {
            List<GameObject> tuilesAutour = new List<GameObject>();
            if (y != 11)
            {
                tuilesAutour.Add(board.grille[x, y + 1]);//tuile du dessus
                if (x != 11)
                {
                    tuilesAutour.Add(board.grille[x + 1, y + (x % 2) ]);//coter droit haut
                }                    
                if (x != 0)          
                {                    
                    tuilesAutour.Add(board.grille[x - 1, y + (x % 2)]);//coter gauche haut
                }

            }
            if (y != 0)
            {
                tuilesAutour.Add(board.grille[x, y - 1]);//tuile du dessous
                if (x != 11)
                {
                    tuilesAutour.Add(board.grille[x + 1, y -1 + (x % 2)]);//coter droit bas
                }
                if (x != 0)
                {
                    tuilesAutour.Add(board.grille[x - 1, y -1+ (x % 2)]);//coter gauche bas
                }

            }
            
            return tuilesAutour;

    }

  
    public string GetTag(string tagg)
    {
        return tagg.Substring(0, tagg.Length-1 );
    }
    public int GetLevelTile(string tagg)
    { 
        return int.Parse(tagg.Substring(tagg.Length - 1, 1));
    }
}
