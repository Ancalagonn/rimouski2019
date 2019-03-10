using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPad : MonoBehaviour
{
    private bool canExit = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F) && canExit) {
            this.ExitScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D p_other) {
        Debug.Log("Enter");
        if (p_other.tag == "Player") {
            canExit = true;
        }
    } 

    private void OnTriggerExit2D(Collider2D p_other) {
        if (p_other.tag == "Player") {
            canExit = false;
        }
    }

    public void ExitScene() {
        foreach (GameObject g in SceneManager.GetActiveScene().GetRootGameObjects()) {
            g.SetActive(false);
        }
        SceneManager.UnloadSceneAsync("BoatScene");
    }
}
