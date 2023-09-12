using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    // Start is called before the first frame update
 void OnTriggerEnter2D(Collider2D other) {
    
    if (other.gameObject.tag == "Player") {
        StartCoroutine(LoadNextLevel());
    }
 }

    IEnumerator LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(1f);
        //FindObjectOfType<GameSession>().LoadNextLevel();
        SceneManager.LoadScene(currentSceneIndex + 1);
        Debug.Log("Loading next level");
    }

 
}
