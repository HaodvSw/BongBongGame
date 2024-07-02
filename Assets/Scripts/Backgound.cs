using UnityEngine;

public class Backgound : MonoBehaviour
{

    [System.Serializable]
    public struct BackgroundSprite
    {
        public Material sprite;
    }

    private MeshRenderer _sprite;

    public BackgroundSprite[] backgroundSprites;


    private void Awake()
    {
        int posRandom = Random.Range(0, backgroundSprites.Length);
        _sprite = GetComponent<MeshRenderer>();
        _sprite.material = backgroundSprites[posRandom].sprite;
    }

    private void Start()
    {
        var worldHeight = Camera.main.orthographicSize * 2f;
        var worldWidth = worldHeight * Screen.width / Screen.height;
        transform.localScale = new Vector3(worldWidth, worldHeight, 0f);
    }
}
