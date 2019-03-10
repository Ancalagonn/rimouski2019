using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapAnimation : MonoBehaviour
{
    private float timeout = 7f;

    public RawImage mySelf;

    public bool loadScene = false;
    public string sceneName = "";

    public List<Texture> images = new List<Texture>();

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Play("TransitionMap", Vector3.zero);
        StartCoroutine(Animation());
    }

    private IEnumerator Animation() {

        float step = (timeout - 1.5f) / images.Count;
        foreach (Texture image in images)
        {
            mySelf.texture = image;
            yield return new WaitForSeconds(step);
        }

        yield return new WaitForSeconds(1.5f);
        
        if (loadScene) {
            ManageScene.instance.LoadSceneBlack(sceneName);
        }

        yield return null;
    }
}
