using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    Rigidbody2D rigid;
    float max_speed = 3.0f;
    float jump_power = 5.0f;

    public bool is_jump = false;
    public bool is_ground = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > max_speed)
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        if (rigid.velocity.x < -max_speed)
            rigid.velocity = new Vector2(-max_speed, rigid.velocity.y);

        Debug.DrawRay(rigid.position, Vector2.down * 0.6f, Color.green);

        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.down, 1.2f, LayerMask.GetMask("PlatForm"));

        if (rayhit.collider != null)
        {
            is_ground = true;
            Debug.Log("h");
        }
        else
        {
            is_ground = false;
            Debug.Log("none ground");
        }

        if (is_ground)
        {
            is_jump = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && !is_jump && is_ground)
        {
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            is_jump = true;
            is_ground = false;
            Debug.Log("do jump");
        }
    }
}
