using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    public GameObject menu;
    public string playertag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerControler>();

        if (player != null)
            menu.SetActive(true);
    }
}
