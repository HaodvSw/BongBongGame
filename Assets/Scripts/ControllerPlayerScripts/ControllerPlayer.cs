using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ControllerPlayer : MonoBehaviour
{

    [SerializeField]
    public GameOver gameOver;

    [SerializeField]
    public GameObject guideOject;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
       
    }

    public void OnClickBackMore()
    {
        SceneManager.LoadScene("PlayerScenes");
    }

    public void ExitsApp()
    {
        Application.Quit();
    }

    public void OnClickMore()
    {
        SceneManager.LoadScene("MoreScenes");
    }

    public void OpenSetting()
    {
        SceneManager.LoadScene("SettingScene");
    }

    public void ResetGame()
    {
        gameOver.setDefaultData();
    }

    public void GuideGame()
    {
        Util.SetData("open", Util.KEY_GUIDE);
        guideOject.SetActive(true);
    }


    public void OpenPrivacy()
    {
        Application.OpenURL("https://www.termsfeed.com/live/9234b18f-566c-4d07-8cb3-13579ccec444");
    }

    public void OpenTemsAndConditions()
    {
        Application.OpenURL("https://www.termsfeed.com/live/9234b18f-566c-4d07-8cb3-13579ccec444");
    }

    public void OpenRateGame()
    {
        //Application.OpenURL("//market");
    }


}
