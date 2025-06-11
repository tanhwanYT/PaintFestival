using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayerMoving : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    float max_speed = 3.0f;
    float jump_power = 5.0f;

    public bool is_jump = false;
    public bool is_ground = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > max_speed)
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        if (rigid.velocity.x < -max_speed)
            rigid.velocity = new Vector2(-max_speed, rigid.velocity.y);

        Debug.DrawRay(rigid.position, Vector2.down * 1.0f, Color.green);

        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.down, 1.2f, LayerMask.GetMask("PlatForm"));
        is_ground = rayhit.collider != null;

        if (is_ground)
        {
            is_jump = false;
        }
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = h < 0 ? -1 : 1;
            transform.localScale = scale;
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && !is_jump && is_ground)
        {
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            is_jump = true;
            is_ground = false;
        }
    }
}
