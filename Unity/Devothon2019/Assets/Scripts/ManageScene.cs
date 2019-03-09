using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour {

    public static ManageScene instance;

    public CanvasGroup cg;
    string scene;

    private float speed = 0.5f;

    public bool sceneLoading = false;
    public bool downAlpha = true;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (sceneLoading && cg.alpha > 0)
        {
            cg.alpha -= speed * Time.deltaTime;
        }
        else if(sceneLoading)
        {
            SceneManager.LoadScene(scene);
        }
	}

    public void LoadSceneBlack(string sceneName)
    {
        if (!sceneLoading) {
                sceneLoading = true;
            scene = sceneName;
            StartCoroutine(LoadYourAsyncScene());
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        
        StopCoroutine(LoadYourAsyncScene());
    }

}
