using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterItem : MonoBehaviour
{

    public string text;
    public float text_size = 22.0f;
    public Material material;


    void Start()
    {
        //UpdateData(text, material, text_size);
    }

    public void UpdateData(string new_text, Material material, float t_size)
    {
        text = new_text;
        text_size = t_size;
        this.material = material;
        GetComponent<Renderer>().material = material;
        
        GetComponentInChildren<TextMeshProUGUI>().text = text;
        GetComponentInChildren<TextMeshProUGUI>().fontSize = t_size;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LetterSlot")
        {
            other.gameObject.GetComponent<LetterSlot>().AttachLetter(this);
            LetterManager.Instance.CheckSequence();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LetterSlot")
        {
            other.gameObject.GetComponent<LetterSlot>().RemoveLetter(this);
            LetterManager.Instance.CheckSequence();
        }

    }

    public string GetLetterText()
    {
        return text;
    }



}
