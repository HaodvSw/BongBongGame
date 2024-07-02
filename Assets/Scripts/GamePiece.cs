using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int score;
    public float moveSpeed;
    public Transform transformWater;
    public GameObject moveUp;
    public GameObject moveDown;
    public GameObject moveLeft;
    public GameObject moveRight;

    private int _x;
    private int _y;

    public int X
    {
        get => _x;
        //set { if (IsMovable()) { _x = value; } }
    }

    public int Y
    {
        get => _y;
        //set { if (IsMovable()) { _y = value; } }
    }

    private PieceType _type;

    public PieceType Type => _type;

    private WaterType _waterType;

    public WaterType Water => _waterType;

    private Grid _grid;

    public Grid GridRef => _grid;

    //private MovablePiece _movableComponent;

    //public MovablePiece MovableComponent => _movableComponent;

    private ColorPiece _colorComponent;

    public ColorPiece ColorComponent => _colorComponent;

    //private ClearablePiece _clearableComponent;

    //public ClearablePiece ClearableComponent => _clearableComponent;

    private void Awake()
    {
        _colorComponent = GetComponent<ColorPiece>();
    }

    public void Init(int x, int y, Grid grid, PieceType type, WaterType waterType)
    {
        _x = x;
        _y = y;
        _grid = grid;
        _type = type;
        _waterType = waterType;
    }

    //public WaterType getWaterType()
    //{
    //    return _waterType;
    //}

    //private void OnMouseEnter()
    //{

    //    //_grid.EnterPiece(this);
    //    transformItem(this);
    //}

    private void OnMouseDown()
    {
        //   _grid.PressPiece(this);
        if (!_grid.isShowPopup() && _waterType != WaterType.AMOUNT_FIVE)
        {
            transformItem(this);
            if (Util.getData(Util.KEY_SOUND).Equals("") || Util.getData(Util.KEY_SOUND).Equals("yes"))
                SoundManager.Instance.PlaySound(SoundType.TypeSelect);
            _grid.setAmount(true);
        }
    }

    //private void OnMouseUp()
    //{
    //    _grid.ReleasePiece();
    //}

    //public bool IsMovable()
    //{
    //    return _movableComponent != null;
    //}

    public bool IsColored()
    {
        return _colorComponent != null;
    }

    //public bool IsClearable()
    //{
    //    return _clearableComponent != null;
    //}

    private void transformItem(GamePiece gamePiece)
    {
        WaterType newType = _waterType;
        if (newType == WaterType.AMOUNT_FIVE) return;
        switch (newType)
        {
            case WaterType.AMOUNT_ONE:
                newType = WaterType.AMOUNT_TWO;
                break;
            case WaterType.AMOUNT_TWO:
                newType = WaterType.AMOUNT_THREE;
                break;
            case WaterType.AMOUNT_THREE:
                newType = WaterType.AMOUNT_FOUR;
                break;
            case WaterType.AMOUNT_FOUR:
                newType = WaterType.AMOUNT_FIVE;
                Instantiate(moveUp, transform.position, Quaternion.identity);
                Instantiate(moveDown, transform.position, Quaternion.identity);
                Instantiate(moveLeft, transform.position, Quaternion.identity);
                Instantiate(moveRight, transform.position, Quaternion.identity);
                _grid.NumberDestroy++;
                break;

        }
        gamePiece.ColorComponent.SetType(newType);
        Init(_x, _y, _grid, _type, newType);
        _grid.ConvertWaterTypeSave();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Move") return;
        WaterType newType = _waterType;
        switch (_waterType)
        {
            case WaterType.AMOUNT_ONE:
                newType = WaterType.AMOUNT_TWO;
                Destroy(collision.gameObject);
                break;
            case WaterType.AMOUNT_TWO:
                newType = WaterType.AMOUNT_THREE;
                Destroy(collision.gameObject);
                break;
            case WaterType.AMOUNT_THREE:
                newType = WaterType.AMOUNT_FOUR;
                Destroy(collision.gameObject);

                break;
            case WaterType.AMOUNT_FOUR:
                if (Util.getData(Util.KEY_SOUND).Equals("") || Util.getData(Util.KEY_SOUND).Equals("yes"))
                    SoundManager.Instance.PlaySound(SoundType.TypePop);
                newType = WaterType.AMOUNT_FIVE;
                Instantiate(moveUp, transform.position, Quaternion.identity);
                Instantiate(moveDown, transform.position, Quaternion.identity);
                Instantiate(moveLeft, transform.position, Quaternion.identity);
                Instantiate(moveRight, transform.position, Quaternion.identity);
                _grid.setAmount(false);
                _grid.NumberDestroy++;
                _grid.IntReward++;
                Destroy(collision.gameObject);
                break;
            case WaterType.AMOUNT_FIVE:
                break;

        }
        this.ColorComponent.SetType(newType);
        Init(_x, _y, _grid, _type, newType);
        _grid.ConvertWaterTypeSave();
    }

}
