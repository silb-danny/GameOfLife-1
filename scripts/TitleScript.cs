using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScript : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public float amount = 1;
    public float amountO = 10;
    void Start()
    {
       StartCoroutine(timer());
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(1);
        while(Title.text != "")
        {
            yield return new WaitForSeconds(amount/amountO);
            Title.SetText(Title.text.TrimEnd(Title.text[Title.text.Length-1]));
            Debug.Log(Title.text);
        }
        Title.enabled = false;
    }
}
