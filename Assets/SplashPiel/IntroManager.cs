using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField]
    private float _introDuration;
    [SerializeField]
    private string _nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator IntroRoutine() {
        yield return new WaitForSeconds(_introDuration);
        StartCoroutine(LoadNextScene());
    }

    private IEnumerator LoadNextScene() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextSceneName);

        // Wait for scene to load
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
