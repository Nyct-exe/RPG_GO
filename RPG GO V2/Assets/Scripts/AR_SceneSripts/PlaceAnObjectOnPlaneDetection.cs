using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARPlaneManager))]
public class PlaceAnObjectOnPlaneDetection : MonoBehaviour
{
    public GameObject gameObjectToInstantiante;
    public GameObject _spawnedMonster;
    private ARPlaneManager _arPlaneManager;
    
    
    void Awake()
    {
        Assert.IsNotNull(_arPlaneManager);
        Assert.IsNotNull(gameObjectToInstantiante);
        Assert.IsNotNull(Camera.main);
        _arPlaneManager = GetComponent<ARPlaneManager>();
        _arPlaneManager.planesChanged += PlaneChanged;
    }

    private void Start()
    {
        // Since other monsters that are in the monster log can also be picked up
        // this is a workaround to pick up only active monsters
        foreach(Monster m in GamePlayController.FindObjectsOfType<Monster>()){
            if (m.gameObject.activeSelf)
            {
                gameObjectToInstantiante = GamePlayController.FindObjectOfType<Monster>().gameObject;
                gameObjectToInstantiante.transform.Find("HealthCanvas").gameObject.SetActive(true);
                gameObjectToInstantiante.SetActive(false);
            }
        }
    }

    private void PlaneChanged(ARPlanesChangedEventArgs args)
    {
        if (args.added != null)
        {
            ARPlane arPlane = args.added[0];
            if (_spawnedMonster == null && args.added != null)
            {
                _spawnedMonster = gameObjectToInstantiante;
                _spawnedMonster.transform.position = arPlane.transform.position;
                _spawnedMonster.transform.rotation = Quaternion.identity;
                _spawnedMonster.transform.LookAt(Camera.main.transform);
                _spawnedMonster.SetActive(true);
            }
        }
    }
    
    
}
