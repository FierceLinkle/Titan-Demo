using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public FirstPersonController fpc;

    public TitanSwitch TS;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            TS.EatPlayer = false;
            return;
        }

        print("About to be eaten!");
        TS.EatPlayer = true;
    }
}
