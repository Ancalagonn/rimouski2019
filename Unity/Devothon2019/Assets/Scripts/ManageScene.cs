using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageScene : MonoBehaviour {

    public static ManageScene instance;

    public CanvasGroup cg;
    string scene;

    private float speed = 0.5f;

    public bool sceneLoading = false;
    public bool downAlpha = true;

    private void Awake()
    {
        cg = gameObject.transform.GetChild(0).GetChild(0).GetComponent<CanvasGroup>();
        cg.GetComponent<Image>().enabled = true;
    }

    // Use this for initialization
    void Start () {
        instance = this;

        
	}
	
	// Update is called once per frame
	void Update () {
        if (sceneLoading)
        {
            if (cg != null && cg.alpha < 0.95f)
            {
                cg.alpha += speed * Time.fixedUnscaledDeltaTime;
                //Debug.Log(cg.alpha);
            }
            else
                SceneManager.LoadScene(scene);
        }
        else
        {
            if (cg != null && cg.alpha > 0.05f)
                cg.alpha -= speed * Time.deltaTime;
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
