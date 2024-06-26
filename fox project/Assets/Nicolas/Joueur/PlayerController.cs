using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    //[SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    //private float wallJumpCooldown;


    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();


        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // //wall jummp logic
        // if (wallJumpCooldown < 0.2f)
        // {
        //     if (Input.GetKey(KeyCode.Space))
        //         Jump();

        //     body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //     if (onWall() && isGrounded())
        //     {
        //         body.gravityScale = 0;
        //         body.velocity = Vector2.zero;
        //     }
        //     else
        //         body.gravityScale = 3;
        // }
        // else
        // wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        //anim.SetTrigger("jumping");
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {

    // }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    //private bool onWall()
    //{
    //    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
    //     return raycastHit.collider != null;
    //}
}