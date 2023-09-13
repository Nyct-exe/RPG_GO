using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectOnClick : MonoBehaviour
{
    public GameObject gameObjectToInstantiante;
    private GameObject spawnedGameObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        
    }

    bool GetTouchPostion(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetTouchPostion(out Vector2 touchPosition))
        {
            return;
        }

        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnedGameObject == null)
            {
                spawnedGameObject = Instantiate(gameObjectToInstantiante, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedGameObject.transform.position = hitPose.position;
            }
        }
    }
}
