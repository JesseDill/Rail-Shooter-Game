using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Speed & Range")]
    [Tooltip("In m/s^1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In m/s^1")] [SerializeField] float ySpeed = 4f;

    [SerializeField] float xMovRange = 3.2f;
    [SerializeField] float yMovRange = 3.2f;

    [Header("Rotation Calculations and Effects")]
    [SerializeField] float pitchFactor = 6f;
    [SerializeField] float yawFactor = 6f;

    [SerializeField] float pitchNudge= 6f;
    [SerializeField] float rollNudge = 6f;

    [SerializeField] GameObject[] guns;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }

        else
        {
            DeactivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }


    void OnPlayerDeath()//called by string reference
    {
        isControlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * -pitchFactor;
        float pitchDueToControlThrow = yThrow * pitchNudge;

        float pitch = pitchDueToPosition - pitchDueToControlThrow;
        float yaw = transform.localPosition.x * yawFactor;


        float roll = xThrow * -rollNudge;



        transform.localRotation = Quaternion.Euler(pitch, yaw, roll); //x pitch, y yaw, z roll
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float xClamp = Mathf.Clamp(rawNewXPos, -xMovRange, xMovRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float yClamp = Mathf.Clamp(rawNewYPos, -yMovRange, yMovRange);

        transform.localPosition = new Vector3(xClamp, yClamp, transform.localPosition.z);
    }
}
