using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameSceneManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timeText;
    public Image progressBarImage;
    public GameObject timerUI_Gameobject;

    [Header("Managers")]
    public GameObject cubeSpawnManager;

    //Audio related
    float audioClipLength;
    private float timeToStartGame = 5.0f;

    public GameObject currentScoreUI_GO;
    public GameObject finalScoreUI_GO;

    public GameObject ui_Helper;
    public GameObject swordGO;
    public GameObject gunGO;


    private void Awake()
    {
        ui_Helper.SetActive(false);
        swordGO.SetActive(true);
        gunGO.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Getting the duration of the song
        audioClipLength = AudioManager.instance.musicTheme.clip.length;
        Debug.Log(audioClipLength);

        //Starting the countdown with song
        StartCoroutine(StartCountdown(audioClipLength));

        //Resetting progress bar
        progressBarImage.fillAmount = Mathf.Clamp(0, 0, 1);

        finalScoreUI_GO.SetActive(false);
        currentScoreUI_GO.SetActive(true);
    }


    public IEnumerator StartCountdown(float countdownValue)
    {
        while (countdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            countdownValue -= 1;

            timeText.text = ConvertToMinAndSeconds(countdownValue);

            progressBarImage.fillAmount = (AudioManager.instance.musicTheme.time / audioClipLength);

        }
        GameOver();
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
        timeText.text = ConvertToMinAndSeconds(0);

        //Disable cube spawning
        cubeSpawnManager.SetActive(false);

        //Disable timer UI
        timerUI_Gameobject.SetActive(false);

        finalScoreUI_GO.SetActive(true);
        currentScoreUI_GO.SetActive(false);

        finalScoreUI_GO.transform.rotation = Quaternion.Euler(Vector3.zero);
        finalScoreUI_GO.transform.position = GameObject.Find("OVRCameraRig").transform.position + new Vector3(0, 2, 4);

        ui_Helper.SetActive(true);
        swordGO.SetActive(false);
        gunGO.SetActive(false);
    }


    private string ConvertToMinAndSeconds(float totalTimeInSeconds)
    {
        string timeText = Mathf.Floor(totalTimeInSeconds / 60).ToString("00") + ":" + Mathf.FloorToInt(totalTimeInSeconds % 60).ToString("00");
        return timeText;
    }

  
}
