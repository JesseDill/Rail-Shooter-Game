using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class RunDollyTrack : MonoBehaviour
{
    [SerializeField] float TargetSpeed = 30f;
    [SerializeField] float Acceleration = 1.5f;

    CinemachineVirtualCamera Camera;
    CinemachineTrackedDolly Dolly;
    float CurrentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    private void SetUpCamera()
    {
        Camera = GetComponent<CinemachineVirtualCamera>();
        Dolly = Camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        Dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
    }

    // Update is called once per frame
    void Update()
    {
        MoveDolly();
    }

    private void MoveDolly()
    {
        CalculateSpeed();
        Dolly.m_PathPosition = Dolly.m_PathPosition + CurrentSpeed * Time.deltaTime;

        //prevents path position overflow in loop
        if(Dolly.m_Path.Looped && Dolly.m_PathPosition > Dolly.m_Path.PathLength)
        {
            Dolly.m_PathPosition = Dolly.m_PathPosition - Dolly.m_Path.PathLength;
        }
    }

    private void CalculateSpeed()
    {
        if (Math.Abs(CurrentSpeed - TargetSpeed) <= Acceleration |Math.Abs(Acceleration)< float.Epsilon)
        {
            CurrentSpeed = TargetSpeed;
            return;
        }

        else if (CurrentSpeed < TargetSpeed)
        {
            CurrentSpeed += Acceleration;
        }
        else
        {
            CurrentSpeed -= Acceleration;
        }
    }
}
