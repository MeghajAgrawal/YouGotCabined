using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIMenu : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    //Menu Functions
    public void GameStart()
    {
        LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }

    //Loading Screen
    public void LoadScene(int SceneID)
    {
        StartCoroutine(LoadSceneSync(SceneID));
    }

    IEnumerator LoadSceneSync(int SceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        LoadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progressView = Mathf.Clamp01(operation.progress/0.9f);
            LoadingBarFill.fillAmount = progressView;
            yield return null;
        }
    }


}
