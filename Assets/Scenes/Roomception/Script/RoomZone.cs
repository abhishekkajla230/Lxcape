using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomZone : MonoBehaviour
{
    public GameObject door1;

    void Start()
    {
        door1.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.gameObject.tag);
        if (other.gameObject.tag == "RoomZone")
        {
            door1.SetActive(true);
        }
    }
}
