using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    // Start is called before the first frame update
 void OnTriggerEnter2D(Collider2D other) {
    
    if (other.gameObject.tag == "Player") {
        StartCoroutine(LoadNextLevel());
    }
 }

    IEnumerator LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        Debug.Log("Loading next level" + nextSceneIndex);
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        Debug.Log("Loading next level");
    }

 
}
