using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    private static string KEY_DATA = "KEY_DATA";

    public GameObject screenNextGame;
    public GameObject screenEndGame;
    public GameObject scoreParent;
    public TMPro.TextMeshProUGUI textLevel;
    public TMPro.TextMeshProUGUI textCore;
    public Image[] stars;
    public GameObject rewardGood;
    public GameObject rewardVeryGood;
    public GameObject rewardExcellent;

    public int Rows;
    public int Column;
    public int TotalWater;

    private int _Amount;
    private int _Level;
    private int _AmountStart = -1;
    private int _LevelStart = -1;
    private string[] _GridWaterTypeCache;
    private string _NewOpenGameCache = "";

    private bool isShowNextGame;
    private bool isShowEndGame;
    private bool _IsStateNewClick;
    private int AmountGood = 3;
    private int AmountVeryGood = 6;
    private int AmountExcellent = 10;

    private int ExcellentCore = 1;
    private bool isGood;
    private bool isVeryGood;

    private void Awake()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int xRows
    {
        get => Rows;
        set { Rows = value; }
    }

    public bool IsShowEndGame
    {
        get => isShowEndGame;
        set { isShowEndGame = value; }
    }

    public int yColumn
    {
        get => Column;
        set { Column = value; }
    }

    public int Amount
    {
        get => _Amount;
        set { _Amount = value; }
    }

    public int Level
    {
        get => _Level;
        set { _Level = value;  }
    }

    public bool IsStateNewClick
    {
        get => _IsStateNewClick;
        set { _IsStateNewClick = value; }
    }

    public string[] GridWaterTypeCache
    {
        get => _GridWaterTypeCache;
        set { _GridWaterTypeCache = value; }
    }

    public string NewOpenGameCache
    {
        get => _NewOpenGameCache;
        set { _NewOpenGameCache = value; }
    }

    public void ShowWinNextLevel(string waterTypeGrid, int starCount)
    {
        screenNextGame.SetActive(true);
        isShowNextGame = true;
        textLevel.GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + _Level.ToString();

        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.Play("GameOverShow");
        }
    
        StartCoroutine(ShowWinCoroutine(3));

        SaveData(_Amount.ToString(),_Level.ToString(), waterTypeGrid);
    }

    public void ShowWinReward(int core)
    {
        Debug.Log("kshkshkhfs0000000  " + core.ToString());
        if (_IsStateNewClick)
        {
            ExcellentCore = 1;
            isGood = true;
            isVeryGood = true;
        }
        StartCoroutine(ShowCore(core));

    }

    public void ShowEndGame()
    {
        screenEndGame.SetActive(true);
        isShowEndGame = true;
    }

    public bool IsShowNextGame() => isShowNextGame;

    private IEnumerator ShowWinCoroutine(int starCount)
    {
        yield return new WaitForSeconds(0.5f);

        if (starCount < stars.Length)
        {
            for (int i = 0; i <= starCount; i++)
            {
                stars[i].enabled = true;
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    private IEnumerator ShowCore(int core)
    {

        if (core > AmountExcellent)
        {
            if (core - AmountExcellent * ExcellentCore > AmountExcellent)
            {
                ExcellentCore++;
                yield return new WaitForSeconds(0.1f);
                Instantiate(rewardExcellent, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        else if (core > AmountVeryGood && isVeryGood)
        {
            isVeryGood = false;
            yield return new WaitForSeconds(0.8f);
            Instantiate(rewardVeryGood, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (core > AmountGood && isGood)
        {
            isGood = false;
            yield return new WaitForSeconds(0.5f);
            Instantiate(rewardVeryGood, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

    public void setNextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        screenNextGame.SetActive(false);
        isShowNextGame = false;

    }

    public void setDefaultData()
    {
        SaveData((TotalWater).ToString(), "0", ""); // the first amount -> level -> Grid 
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ReplayGame()
    {

        //SaveData(_NewOpenGameCache);
        SaveData(_Amount.ToString(), _Level.ToString(), _NewOpenGameCache);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SaveData(string amount, string level, string waterTypeGrid)
    {
        string saveValue = amount + "," + level + waterTypeGrid;
        string loadValue = PlayerPrefs.GetString(KEY_DATA);
        if (!saveValue.Equals(loadValue))
        {
            PlayerPrefs.SetString(KEY_DATA, saveValue);
            PlayerPrefs.Save();
        }
    }

    //public void SaveData(string data)
    //{
    //        PlayerPrefs.SetString(KEY_DATA, data);
    //        PlayerPrefs.Save();
    //}

    public void getSaveData()
    {
        string oldData = PlayerPrefs.GetString(KEY_DATA);
        Debug.Log("kfhkshfkshfkshf oldData == " + oldData);
        bool doneLevel = true;
        if (oldData != null)
        {
            string[] numbers = oldData.Split(",");
            if (numbers != null && numbers.Length >= 2)
            {
                _Amount = int.Parse(numbers[0]);
                _Level = int.Parse(numbers[1]);
               if(_AmountStart == -1) _AmountStart = int.Parse(numbers[0]);
                if(_LevelStart == -1)_LevelStart = int.Parse(numbers[1]);
                _GridWaterTypeCache = Util.RemoveArray(numbers, 0);
                _GridWaterTypeCache = Util.RemoveArray(_GridWaterTypeCache, 0);
                for(int i = 0; i< _GridWaterTypeCache.Length; i++)
                {
                    if (!_GridWaterTypeCache[i].Equals("5"))
                        {
                        doneLevel = false;
                        break;
                    }
                }
                if(doneLevel)_GridWaterTypeCache = new string[0];
                //if (_NewOpenGameCache.Equals("")) _NewOpenGameCache = oldData;
                //Debug.Log("kfhkshfkshfkshf == " + _NewOpenGameCache);
         
            }
            
        }
    }

}
