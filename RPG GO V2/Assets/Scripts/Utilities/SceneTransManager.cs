using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class SceneTransManager : Singleton<SceneTransManager>
{
    private AsyncOperation sceneAsyncOperation;

    public void SceneChange(string scene, List<GameObject> gameObjects)
    {
        StartCoroutine(LoadScene(scene, gameObjects));
    }

    private IEnumerator LoadScene(string scene, List<GameObject> gameObjects)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync(scene);
        SceneManager.sceneLoaded += (newScene, mode) =>
        {
            SceneManager.SetActiveScene(newScene);
        };
        Scene sceneToLoad = SceneManager.GetSceneByName(scene);
        foreach (GameObject obj in gameObjects)
        {
            SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
            DontDestroyOnLoad(obj);
            SceneManager.UnloadSceneAsync(currentScene);
        }

        yield return null;
    }

}
