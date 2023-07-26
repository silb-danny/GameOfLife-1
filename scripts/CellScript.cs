using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellScript : MonoBehaviour
{
    float min;
    float max;
    public GameObject cell;
    public int num = 0;
    void Start()
    {
        min = GameObject.Find("UIFcontroller").GetComponent<UISCRIPT>().min;

        max = GameObject.Find("UIFcontroller").GetComponent<UISCRIPT>().max;
    }
    public Vector3 ActionCellCheck() 
    {
        Vector3 pos = transform.position;
        RaycastHit2D[] Surroundings = Physics2D.BoxCastAll(pos, new Vector2(2.5f,2.5f), 0f, Vector2.zero);
        int dec  = Surroundings.Length;
        int result = 0;
        if(gameObject.name == cell.name)
        {
            if(dec - 1 >= min && dec - 1<= max)
            {
                result = 1;
            }
        }else if(gameObject.name != cell.name )
        {
            if(dec > min && dec <= max)
            {
                result = 1;
            }
        }
        if(gameObject.name != cell.name)
        {
        Debug.Log(this.name + " " + (dec-1));
        Debug.Log(result);
        }
        return new Vector3(pos.x, pos.y, result);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector2(2.5f,2.5f));
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(this.name != cell.name)
        {
            if(col.gameObject.layer == 0 || col.GetComponent<CellScript>().num < num){
                Debug.Log(col.name);
                Destroy(this.gameObject);
            }
        }
    }
}
