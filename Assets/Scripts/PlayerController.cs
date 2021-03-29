using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public LayerMask ground;
    public Collider2D coll;

    public float speed;
    public float jumpforce;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        SwitchAnim();
    }

    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float faceddirection = Input.GetAxisRaw("Horizontal");

        //角色移动
        if (horizontalmove!=0)
        {
            rb.velocity = new Vector2(horizontalmove*speed, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(faceddirection));
        }
        if (faceddirection != 0)
        {
            transform.localScale = new Vector3(faceddirection, 1, 1);
        }
        //角色跳跃
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            anim.SetBool("jumping", true);
        }
    }
    //控制跳起和降落动画转换
    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("falling", false);
        }
    }
}
