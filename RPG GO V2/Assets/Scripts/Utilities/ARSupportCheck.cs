using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

/*
 * The code is created using official AR Foundation documentation as a foundation
 */
public class ARSupportCheck : MonoBehaviour
{
    [SerializeField] ARSession m_Session;
    [SerializeField] private Text infoText;

    private void Awake()
    {
        Assert.IsNotNull(m_Session);
        Assert.IsNotNull(infoText);
    }

    IEnumerator Start() {
        if ((ARSession.state == ARSessionState.None) ||
            (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            infoText.text = "Missing AR Support";
            infoText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
            infoText.gameObject.SetActive(true);
            // Start some fallback experience for unsupported devices
            Debug.Log("Augmented Reality Unsupported");
            List<GameObject> list = new List<GameObject>();
            // There always only one monster in a battle scene so find object will always get what we need
            list.Add(FindObjectOfType<Monster>().gameObject);
            SceneTransManager.Instance.SceneChange(GameConstants.SCENE_NONAR, list);
            Destroy(GameObject.FindWithTag("BattleManager").gameObject);
            
        }
        else
        {
            // Start the AR session
            m_Session.enabled = true;
        }
    }
}

