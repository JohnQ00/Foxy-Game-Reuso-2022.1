using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D playerCollider) {
        if (playerCollider.gameObject.tag == "Player") {
            SceneManager.LoadScene(sceneName);
        }
    }
}
