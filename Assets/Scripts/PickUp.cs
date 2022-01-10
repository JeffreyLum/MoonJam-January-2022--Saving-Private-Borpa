using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public GameObject ui;
    public GameObject cam;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ui.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            cam.GetComponent<PlayerMovement>().enabled = false;
        }


    }
}
