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
    
    float base_speed = 3.0f;
    float base_jump_power = 5.0f;

    public bool is_jump = false;
    public bool is_ground = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float scaleFactor = transform.localScale.x; 
        float speedFactor = 1 / Mathf.Max(scaleFactor, 0.1f); 
        float jumpFactor = Mathf.Min(scaleFactor, 2.0f);

        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > max_speed)
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        if (rigid.velocity.x < -max_speed)
            rigid.velocity = new Vector2(-max_speed, rigid.velocity.y);
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
            float jumpFactor = Mathf.Min(transform.localScale.x, 2.0f);
            float jump_power = base_jump_power * jumpFactor;

            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
            is_jump = true;
            is_ground = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlatForm"))
        {
            is_ground = true;
            is_jump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlatForm"))
        {
            is_ground = false;
        }
    }
}
