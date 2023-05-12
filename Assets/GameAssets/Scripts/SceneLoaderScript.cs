using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    public OVROverlay overlayBackground;
    public OVROverlay overlayText;

    public static SceneLoaderScript instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(ShowOverlayAndLoad(sceneName));
    }

    IEnumerator ShowOverlayAndLoad(string sceneName)
    {
        overlayBackground.enabled = true;
        overlayText.enabled = true;

        GameObject centerEyeAnchor = GameObject.Find("CenterEyeAnchor");
        overlayText.gameObject.transform.position = centerEyeAnchor.transform.position + new Vector3(0, 0, 3);

        yield return new WaitForSeconds(3);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        overlayBackground.enabled = false;
        overlayText.enabled = false;

        yield return null;
    }

}
