using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUp : MonoBehaviour
{

    public float speed;

    private Rigidbody2D rigidbody;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        if (Util.getData(Util.KEY_SOUND).Equals("") || Util.getData(Util.KEY_SOUND).Equals("yes"))
            SoundManager.Instance.PlaySound(SoundType.TypeMove);
    }
    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Camera.main.pixelHeight - Camera.main.pixelWidth/2 - 80)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(0f, speed);
    }

   
}
