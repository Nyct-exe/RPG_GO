using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class Pulse : MonoBehaviour
{
     public Transform pulseTransform;
     private float _range;
     private float _rangeMax;

     private void Awake()
     {
         Assert.IsNotNull(pulseTransform);
         _rangeMax = 40;
     }

     // Update is called once per frame
    void Update()
    {
        float speed = 10f;
        _range += speed * Time.deltaTime;
        if (_range > _rangeMax)
        {
            _range = 0f;
        }

        pulseTransform.localScale = new Vector3(_range, _range);
    }
}
