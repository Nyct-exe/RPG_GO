using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerometer : MonoBehaviour
{
    #region Instance
    private static Accelerometer instance;

    public static Accelerometer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Accelerometer>();
                if (instance == null)
                {
                    instance = new GameObject("Accelerometer", typeof(Accelerometer)).GetComponent<Accelerometer>();
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    #endregion
    
    [Header("Shake Detection")] public Action OnShake;

    [SerializeField] private float shakeDetectionTreshold = 2.0f;

    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private float lowPassKernelWifthInSeconds = 1.0f;
    private float lowPasFilterFactor;
    private Vector3 lowPassValue;
    // Start is called before the first frame update
    void Start()
    {
        lowPasFilterFactor = accelerometerUpdateInterval / lowPassKernelWifthInSeconds;
        shakeDetectionTreshold *= shakeDetectionTreshold;
        lowPassValue = Input.acceleration;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPasFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;
        
        //Detect Shake
        if(deltaAcceleration.sqrMagnitude >= shakeDetectionTreshold)
            OnShake?.Invoke();
    }
}
