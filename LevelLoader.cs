using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelWithTransition(index));
    }

    IEnumerator LoadLevelWithTransition(int index)
    {
        transition.SetTrigger("Start");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(transitionTime);
        
        SceneManager.LoadScene(index);
    }
}
