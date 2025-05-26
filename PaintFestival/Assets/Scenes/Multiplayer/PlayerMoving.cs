using Photon.Pun;
using UnityEngine;

public class PlayerMoving : MonoBehaviourPun
{
    Rigidbody2D rigid;
    float max_speed = 3.0f;
    float jump_power = 5.0f;

    public bool is_jump = false;
    public bool is_ground = false;

    private bool isMineOrOffline;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        isMineOrOffline = !PhotonNetwork.IsConnected || photonView.IsMine;
    }

    private void FixedUpdate()
    {
        if (!isMineOrOffline) return;

        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > max_speed)
            rigid.velocity = new Vector2(max_speed, rigid.velocity.y);
        if (rigid.velocity.x < -max_speed)
            rigid.velocity = new Vector2(-max_speed, rigid.velocity.y);

        Debug.DrawRay(rigid.position, Vector2.down * 0.6f, Color.green);

        RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector2.down, 1.2f, LayerMask.GetMask("PlatForm"));
        is_ground = rayhit.collider != null;

        if (is_ground)
        {
            is_jump = false;
        }
    }

    void Update()
    {
        if (!isMineOrOffline) return;

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
