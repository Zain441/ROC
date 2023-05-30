using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{
    public GameObject loadingScreen;
    public Text loadingText;
    public Slider slider;


    public void LoadLevelFunction(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronically(sceneIndex));
    }

    IEnumerator LoadAsynchronically(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); loadingScreen.SetActive(true); 
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); 
            slider.value = progress;
            loadingText.text = progress * 100f + "%";
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
