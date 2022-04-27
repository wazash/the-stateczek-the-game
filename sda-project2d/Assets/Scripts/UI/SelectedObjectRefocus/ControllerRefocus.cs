using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerRefocus : MonoBehaviour
{
    private EventSystem system;
    private GameObject lastSelected;

    private void Start()
    {
        system = EventSystem.current;
    }
    private void Update()
    {
        if(system.currentSelectedGameObject == null)
        {
            system.SetSelectedGameObject(lastSelected);
        }
        else
        {
            lastSelected = system.currentSelectedGameObject;
        }
    }
}
