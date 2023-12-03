using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string character;
    public Material letterMaterial;

    private void Start()
    {
       // GetComponent<Renderer>().material = letterMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }



}
