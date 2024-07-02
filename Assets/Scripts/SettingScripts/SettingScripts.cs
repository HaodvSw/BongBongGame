using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingScripts : MonoBehaviour
{
    [SerializeField]
    public Image checkBoxSound;
    public Sprite Uncheck;
    public Sprite Checker;

    private string language;
    private string sound;

    private void Awake()
    {
        sound = Util.getData(Util.KEY_SOUND); // value on sound = yes
        if (sound.ToString().Equals("") || sound.ToString().Equals("yes")) checkBoxSound.sprite = Checker;
        else checkBoxSound.sprite = Uncheck;
    }

    public void SettingSound()
    {
        sound = Util.getData(Util.KEY_SOUND);
        if (sound.ToString().Equals("") || sound.ToString().Equals("yes"))
        {
            Util.SetData("no", Util.KEY_SOUND);
            checkBoxSound.sprite = Uncheck;
        }
        else
        {
            Util.SetData("yes", Util.KEY_SOUND);
            checkBoxSound.sprite = Checker;
        }
    }

    public void OnClickBackSetting()
    {
        SceneManager.LoadScene("MoreScenes");
    }
}
