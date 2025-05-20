using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUIPopup : MonoBehaviour
{
    public GameObject popupPanel;

    void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false); // Hide at start
        }
    }

    public void Interact()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }
    }
}

