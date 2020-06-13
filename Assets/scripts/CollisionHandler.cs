using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok if this is only script loading scenes

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1f;
    [SerializeField] GameObject deathFX;
    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadLevel", 2f);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
