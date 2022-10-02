using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    void Start() => StartCoroutine(LoadScene(1f));
    IEnumerator LoadScene(float time)
    {
        yield return time;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("SampleScene");        
        asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);        
        while (!asyncOperation.isDone)
        {            
            //Debug.Log("Loading progress: " + (asyncOperation.progress * 100) + "%");         
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return time;
        }
    }
}