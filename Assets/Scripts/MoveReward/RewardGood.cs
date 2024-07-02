using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReward : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rigidbody;
    private Color tmp;
    private float alpha = 1f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = alpha;

        if (Util.getData(Util.KEY_SOUND).Equals("") || Util.getData(Util.KEY_SOUND).Equals("yes"))
            SoundManager.Instance.PlaySound(SoundType.TypeMove);
    }
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Camera.main.pixelHeight)
        {
            Destroy(gameObject);
        }

        if (alpha > 0f)
        {
            alpha = alpha - 0.002f;
            tmp.a = alpha;
            GetComponent<SpriteRenderer>().color = tmp;
        }

    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(0f, speed);
    }

}
