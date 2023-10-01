using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        Debug.Log("New Level load: " + sceneName);
        StartCoroutine(LoadAsync(sceneName));
    }

    public void LoadAfterDelay(string sceneName, float delaySeconds)
    {
        StartCoroutine(LoadSceneAfterDelay(sceneName, delaySeconds));
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

    IEnumerator LoadSceneAfterDelay(string sceneName, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);

        StartCoroutine(LoadAsync(sceneName));
    }
    
    IEnumerator LoadAsync(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
