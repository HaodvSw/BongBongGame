using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (Util.getData(Util.KEY_SOUND).Equals("") || Util.getData(Util.KEY_SOUND).Equals("yes"))
            SoundManager.Instance.PlaySound(SoundType.TypeMove);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y < Camera.main.pixelHeight/2 - Camera.main.pixelWidth / 2)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(0f, -speed);
    }
}
