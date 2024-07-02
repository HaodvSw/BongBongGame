using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideGame : MonoBehaviour
{

    [SerializeField]
    GameObject screenGuide;

    private string isFirstApp;

    // Start is called before the first frame update
    void Start()
    {
        isFirstApp = Util.getData(Util.KEY_GUIDE); // value on isFirstApp = yes
        if (isFirstApp.ToString().Equals("")) // || isFirstApp.ToString().Equals("yes") || isFirstApp.ToString().Equals("open")
        {
            screenGuide.SetActive(true);
            Util.SetData("no", Util.KEY_GUIDE);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickOk()
    {
        screenGuide.SetActive(false);
    }
}
