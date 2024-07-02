using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Grid : MonoBehaviour
{
    public float moveSpeed;
    public float tileSize;

    public PiecePrefab[] piecePrefabs;
    public GameObject backgroundPrefab;
    public TMPro.TextMeshProUGUI textAmount;
    [SerializeField]
    public GameOver gameOver;

    private int _numberDestroy = 0;
    private GamePiece[,] _pieces;

    private int _intReward = 0;

    private Dictionary<PieceType, GameObject> piecePrefabDict;
    //private GamePiece _enteredPiece;

    private List<int> waterObj ;
    private List<WaterType> waterObjType;


    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    }

    public void EnterPiece(GamePiece piece)
    {
        //_enteredPiece = piece;
    }

    public int NumberDestroy
    {
        get => _numberDestroy;
        set { _numberDestroy = value; }
    }

    public int IntReward
    {
        get => _intReward;
        set { _intReward = value; }
    }

    void Start()
    {
        gameOver.getSaveData();
        if (Util.getData(Util.KEY_SOUND).Equals("") || Util.getData(Util.KEY_SOUND).Equals("yes"))
            SoundManager.Instance.PlaySound(SoundType.TypeGameOver);
        textAmount.GetComponent<TMPro.TextMeshProUGUI>().text = "Amount: " + gameOver.Amount.ToString();
        initGripWater();
    }


    Vector2 GetWorldPosition(float x, float y)
    {
        return new Vector2(-y / 2 + tileSize / 2, x / 2 - tileSize /2);
    }

    public void renderListWaterType(List<WaterType> waterObjType)
    {
        string gridOpenFirst = "";
        if (gameOver.GridWaterTypeCache != null && gameOver.GridWaterTypeCache.Length > 0)
        {
           for(int i = 0; i< gameOver.GridWaterTypeCache.Length; i++)
            {
                gridOpenFirst = gridOpenFirst + "," + gameOver.GridWaterTypeCache[i];
                switch (gameOver.GridWaterTypeCache[i])
                {
                    case "1":
                        waterObjType.Add(WaterType.AMOUNT_ONE);
                        break;
                    case "2":
                        waterObjType.Add(WaterType.AMOUNT_TWO);
                        break;
                    case "3":
                        waterObjType.Add(WaterType.AMOUNT_THREE);
                        break;
                    case "4":
                        waterObjType.Add(WaterType.AMOUNT_FOUR);
                        break;
                    case "5":
                        waterObjType.Add(WaterType.AMOUNT_FIVE);
                        _numberDestroy++;
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameOver.xRows * gameOver.yColumn; i++)
            {
                waterObj.Add(1);
            }
            int remain = (gameOver.TotalWater - gameOver.Level) - gameOver.xRows * gameOver.yColumn;
            int randomPos;
            while (remain > 0)
            {
                randomPos = Random.Range(0, gameOver.xRows * gameOver.yColumn - 1);
                if (waterObj[randomPos] > 2) continue;
                else
                {
                    waterObj[randomPos] = waterObj[randomPos] + 1;
                    remain--;
                }
            }
            for (int i = 0; i < waterObj.Count; i++)
            {
                gridOpenFirst = gridOpenFirst + "," + waterObj[i];
                switch (waterObj[i])
                {
                    case 1:
                        waterObjType.Add(WaterType.AMOUNT_ONE);
                        break;
                    case 2:
                        waterObjType.Add(WaterType.AMOUNT_TWO);
                        break;
                    case 3:
                        waterObjType.Add(WaterType.AMOUNT_THREE);
                        break;
                    case 4:
                        waterObjType.Add(WaterType.AMOUNT_FOUR);
                        break;

                }
            }
        }
        gameOver.NewOpenGameCache = gridOpenFirst;
    }

    private void initGripWater()
    {
        waterObj = new List<int>();
        waterObjType = new List<WaterType>();
        renderListWaterType(waterObjType);
        piecePrefabDict = new Dictionary<PieceType, GameObject>();

        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }
        for (int x = 0; x < gameOver.xRows; x++)
        {
            for (int y = 0; y < gameOver.yColumn; y++)
            {
                GameObject game = (GameObject)Instantiate(backgroundPrefab, transform);
                float posX = x * tileSize;
                float posY = y * -tileSize;
                game.transform.position = new Vector2(posX, posY);
                game.transform.localScale = new Vector2(tileSize, tileSize);
            }
        }

        _pieces = new GamePiece[gameOver.xRows, gameOver.yColumn];
        int pos = 0;
        for (int x = 0; x < gameOver.xRows; x++)
        {
            for (int y = 0; y < gameOver.yColumn; y++)
            {
                GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], transform); // => PieceType de sau nay mở rộng chức năng của hạt nước, vd: nó là 1 viên đá cản đường giọt nước bay
                newPiece.name = "Piece(" + x + "," + y + ")";
                float posX = x * tileSize;
                float posY = y * -tileSize;
                newPiece.transform.position = new Vector2(posX, posY);

                _pieces[x, y] = newPiece.GetComponent<GamePiece>();
                _pieces[x, y].Init(x, y, this, PieceType.NORMAL, waterObjType[pos]);
                pos++;
                //if (_pieces[x, y].IsColored())
                //{
                _pieces[x, y].ColorComponent.SetType(_pieces[x, y].Water);
                //}x
            }
        }
        transform.position = GetWorldPosition(gameOver.xRows * tileSize, gameOver.yColumn * tileSize);
    }

    private void Update()
    {
        if(_numberDestroy == gameOver.xRows * gameOver.yColumn)
        {
            gameOver.Level++;
            gameOver.ShowWinNextLevel("", 2);
            _numberDestroy = 0;
            ConvertWaterTypeSave();
        } else if(gameOver.Amount == 0 && !gameOver.IsShowEndGame)
        {
            gameOver.ShowEndGame();
        }
    }

    private void OnApplicationQuit()
    {
        gameOver.SaveData(gameOver.Amount.ToString(), gameOver.Level.ToString(), ConvertWaterTypeSave());

    }

    public string ConvertWaterTypeSave()
    {
        string ConvertString = "";
        int _Type = 1;
        for (int x = 0; x < gameOver.xRows; x++)
        {
            for (int y = 0; y < gameOver.yColumn; y++)
            {
                switch (_pieces[x, y].Water)
                {
                    case WaterType.AMOUNT_ONE:
                        _Type = 1;
                        break;
                    case WaterType.AMOUNT_TWO:
                        _Type = 2;
                        break;
                    case WaterType.AMOUNT_THREE:
                        _Type = 3;
                        break;
                    case WaterType.AMOUNT_FOUR:
                        _Type = 4;
                        break;
                    case WaterType.AMOUNT_FIVE:
                        _Type = 5;
                        break;
                }
                ConvertString = ConvertString + "," + _Type.ToString();
            }
        }
        return ConvertString;
    }

    public bool isShowPopup() => gameOver.IsShowNextGame() || gameOver.IsShowEndGame;

    public void setAmount(bool UserClick)
    {
        if (UserClick)
        {
            _intReward = 0;
            gameOver.Amount--;
            gameOver.IsStateNewClick = true;
        }
        else
        {
            _intReward++;
            gameOver.Amount++;
            gameOver.ShowWinReward(_intReward);
            gameOver.IsStateNewClick = false;
        }        
        textAmount.GetComponent<TMPro.TextMeshProUGUI>().text = "Amount: " + gameOver.Amount.ToString();
   
    }
}
