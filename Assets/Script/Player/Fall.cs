using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D playerCollider) {
        print(SceneManager.GetActiveScene().name);
        if (playerCollider.gameObject.tag == "Player") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
