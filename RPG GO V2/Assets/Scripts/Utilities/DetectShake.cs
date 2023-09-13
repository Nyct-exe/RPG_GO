using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DetectShake : MonoBehaviour
{
    [SerializeField] private Animator Rotation;
    int nextPosition = 1;
    private void OnDestroy()
    {
        Accelerometer.Instance.OnShake -= ActionToRunWhileShaking;
    }

    // Update is called once per frame
    void Update()
    {
        // Making sure animation is finished before starting another one
        Accelerometer.Instance.OnShake += ActionToRunWhileShaking;
    }

    private void ActionToRunWhileShaking()
    {
        if (Rotation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            Debug.Log(Input.acceleration);
            // Animation Only Runs Once
            if (nextPosition == 1 && Rotation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Rotation.Play("FirstRotation");
                nextPosition = 2;
            }
            else if (nextPosition == 2 && Rotation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Rotation.Play("SecondRotation");
                nextPosition = 3;
            }
            else if (nextPosition == 3 && Rotation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                Rotation.Play("ThirdRotation");
                nextPosition = 1;
                
            }
       
        }
    }
}
