using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPiece : MonoBehaviour
{
    [System.Serializable]
    public struct AmountWaterSprite
    {
        public WaterType color;
        public Sprite sprite;
    }

    public AmountWaterSprite[] amountWaterSprites;

    private WaterType _color;

    public WaterType Color
    {
        get => _color;
        set => SetType(value);
    }

    public int NumColors => amountWaterSprites.Length;

    private SpriteRenderer _sprite;
    private Dictionary<WaterType, Sprite> _colorSpriteDict;

    private void Awake()
    {
        _sprite = transform.Find("piece").GetComponent<SpriteRenderer>();

        // instantiating and populating a Dictionary of all Color Types / Sprites (for fast lookup)
        _colorSpriteDict = new Dictionary<WaterType, Sprite>();

        for (int i = 0; i < amountWaterSprites.Length; i++)
        {
            if (!_colorSpriteDict.ContainsKey(amountWaterSprites[i].color))
            {
                _colorSpriteDict.Add(amountWaterSprites[i].color, amountWaterSprites[i].sprite);
            }
        }
    }

    public void SetType(WaterType newColor)
    {
        _color = newColor;

        if (_colorSpriteDict.ContainsKey(newColor))
        {
            _sprite.sprite = _colorSpriteDict[newColor];
        }
    }
}
