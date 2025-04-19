using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 4f;

    Rigidbody2D rigid;
    Animator animator;
    public float walkIdleDelay = 0.05f;
    public float noInputTime = 0f;


    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetTrigger("Attack");

        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        rigid.linearVelocity = new Vector2(inputX * speed, rigid.linearVelocity.y);

        bool isWalking = Mathf.Abs(inputX) > 0;

        if (isWalking)
        {
            animator.SetBool("walk", true);
            noInputTime = 0f;
            Flip(inputX > 0);
        }
        else
        {
            noInputTime += Time.fixedDeltaTime;
            if (noInputTime >= walkIdleDelay)
            {
                animator.SetBool("walk", false);
            }
        }
    }



    void Flip(bool facingRight)
    {
        Vector3 scale = transform.GetChild(0).localScale;
        scale.x = facingRight ? -1f : 1f;
        transform.GetChild(0).localScale = scale;
    }
}
