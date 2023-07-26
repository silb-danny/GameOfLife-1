using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UISCRIPT : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI minTXT;
    public TextMeshProUGUI maxTXT;
    public TextMeshProUGUI BetTXT;
    public int min = 2;
    public int max = 3;
    public static float BetWave = 1f;
    public GameObject camobj;
    public Camera cam;
    public Vector3 resPOZ= Vector3.zero;
    public GameObject Cell;
    string CellPlacerV = "pen";
    public float valu = 0;
    public float tim = 0.2f;
    bool start = false;
    public int LayerOfUnderCells = 8;
    public int LayerOfCells = 0;
    public float CellZPlacement = 0;
    public float StartingCamSize = 12f;
    public GameObject ShowdButtn;
    public GameObject HidedButtn;
    public TextMeshProUGUI genCount;
    public int generation = 0;
    public GameObject timeStop;
    public GameObject resTime;
    Dictionary<Vector2, float> CellPlace = new Dictionary<Vector2, float>();
    Dictionary<Vector2, float> LastPos = new Dictionary<Vector2, float>();
    void FixedUpdate()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length <= 0)
        {
            Clear();
        }
        if(Input.GetKey(KeyCode.R))
        {
            Reset();
        }
        if(start)
        {
            if(Time.time%1 == 0)
            {
                CellPlace.Clear();
                if(GameObject.FindGameObjectsWithTag("Player").Length>0)
                {
                    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
                    {   Vector3 result;
                        result = obj.GetComponent<CellScript>().ActionCellCheck();
                        if(!CellPlace.ContainsKey(new Vector2(result.x, result.y)))
                        {
                            CellPlace.Add(new Vector2(result.x, result.y), result.z);
                        }
                    }
                    CellSpawner(CellPlace);
                    generation+=1;
                }
            }
        }
        genCount.SetText(generation.ToString());
        valu = GameObject.FindGameObjectsWithTag("Player").Length/9;
        tmp.SetText(valu.ToString());
        min = Mathf.Clamp(min, 0, max);
        max = Mathf.Clamp(max, min, 8);
        minTXT.SetText(min.ToString());
        maxTXT.SetText(max.ToString());
        BetTXT.SetText(BetWave.ToString());
        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.Z))
        {
            LastPosition();
        }
        if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(CellPlacerV == "pen")
            {
                var Dec = 0;
                var mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                mousePos = new Vector3(decValue(mousePos.x),decValue(mousePos.y),0);
                if(GameObject.FindGameObjectsWithTag("Player").Length>0)
                {
                    foreach( GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        mousePos.z = obj.transform.position.z;
                        if(obj.transform.position == mousePos)
                        {
                            Dec+=1;
                        }
                    }
                }
                    if(Dec == 0)
                    {
                        Instantiate(Cell, mousePos , Quaternion.identity);
                    }
                }
            else if(CellPlacerV == "ers")
            {
                var mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);
                mousePos = new Vector3(decValue(mousePos.x),decValue(mousePos.y),0);
                if(GameObject.FindGameObjectsWithTag("Player").Length>0)
                {
                    foreach( GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        mousePos.z = obj.transform.position.z;
                        if(obj.transform.position == mousePos)
                        {
                            Destroy(obj);
                        }
                    }
                }
            }
        }
    }
    public void StopTime()
    {
        timeStop.SetActive(false);
        Time.timeScale = 0;
        resTime.SetActive(true);
    }
    public void ResumeTime()
    {
        resTime.SetActive(false);
        Time.timeScale = BetWave;
        timeStop.SetActive(true);
    }
    public void buttonPressed()
    {
    }
    public void LastPosition()
    {
        if(LastPos.Count>0)
        {
            Clear();
            CellSpawner(LastPos);
        }
    }
    void CellSpawner(Dictionary<Vector2, float> CellPlace)
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length>0)
        {
            foreach( GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(obj);
            }
        }
        foreach(KeyValuePair<Vector2, float> pos in CellPlace){
            if(pos.Value == 1){
                Instantiate(Cell, new Vector3(pos.Key.x, pos.Key.y, 0), Quaternion.identity);
            }
        }

    }
    float decValue(float flt)
    {
        var val = flt;
        if(flt - Mathf.Floor(flt) >= 0.5)
        {
            val = Mathf.Ceil(val);
        }else if(flt - Mathf.Floor(flt)<0.5)
        {
            val = Mathf.Floor(val);
        }
        return val;
    }
    public void Reset()
    {
        resPOZ.z=camobj.transform.position.z;
        camobj.transform.position = resPOZ;
        cam.orthographicSize = StartingCamSize;
    }
    public void Pen()
    {
        CellPlacerV = "pen";
    }
    public void Eraser()
    {
        CellPlacerV = "ers";
    }
    public void Clear()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length>0)
        {
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(obj);
            }
        }
        start = false;
        CellPlacerV = "pen";
        generation = 0;
    }
    public void ChangeVal(int type)
    {
        BetWave=Mathf.Clamp(tim*type+BetWave, 0, 100);
        Time.timeScale = BetWave;
    }
    public void Begin()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 0)
        {
            LastPos.Clear();
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(obj.layer == Cell.layer)
                {
                    LastPos.Add(new Vector2(obj.transform.position.x, obj.transform.position.y), 1);
                }
            }

        }
        start = true;
    }
    public void ShowDebug()
    {
        ShowdButtn.SetActive(false);
        cam.cullingMask = -1;
        HidedButtn.SetActive(true);
    }
    public void HideDebug()
    {
        HidedButtn.SetActive(false);
        cam.cullingMask = 1;
        ShowdButtn.SetActive(true);
    }
    public void ChangeValMax(int type)
    {
        max = Mathf.Clamp(type+max, min, 8);
    }
    public void ChangeValMin(int type)
    {
        min = Mathf.Clamp(type+min, 0, max);
    }
}
