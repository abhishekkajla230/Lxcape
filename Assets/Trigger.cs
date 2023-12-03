using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        XRGrabInteractable interactable = other.gameObject.GetComponent<XRGrabInteractable>();

        _button.active = !interactable.isSelected;
    }
}
