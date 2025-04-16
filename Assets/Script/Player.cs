using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f;

    Rigidbody2D rigid;
    Animator animator;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        rigid.linearVelocity = new Vector2(inputX * speed, rigid.linearVelocity.y);

        if (inputX != 0)
            Flip(inputX > 0);
    }

    void Flip(bool facingRight)
    {
        Vector3 scale = transform.GetChild(0).localScale;
        scale.x = facingRight ? -1f : 1f;
        transform.GetChild(0).localScale = scale;
    }
}
