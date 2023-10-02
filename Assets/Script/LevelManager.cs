using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        Debug.Log("New Level load: " + sceneName);
        LevelUpManager._level = 3;
        StartCoroutine(LoadAsync(sceneName));
    }

    public void LoadNextAfterDelay(float delaySeconds)
    {
        StartCoroutine(LoadSceneAfterDelay(delaySeconds));
    }
    
    public void LoadAfterDelay(string name, float delaySeconds)
    {
        StartCoroutine(LoadSceneAfterDelay(name, delaySeconds));
    }

    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    IEnumerator LoadSceneAfterDelay(string name, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        StartCoroutine(LoadAsync(name));
    }

    IEnumerator LoadSceneAfterDelay(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        StartCoroutine(LoadAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadAsync(string sceneId)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }


    IEnumerator LoadAsync(int sceneId)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneId);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}