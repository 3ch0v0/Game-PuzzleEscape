using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    SpriteRenderer spriteRenderer;
    private PlayerController playerMovement;
    Rigidbody2D targetRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerController>();
        targetRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerAnimation();
    }

    void PlayerAnimation()
    {
        //Walk
        SpriteFliping(spriteRenderer, playerMovement.horizontalInput);
        animator.SetFloat("x", playerMovement.horizontalInput);
        //Jump
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     animator.SetTrigger("Jump");
        // }
        // animator.SetFloat("y", targetRB.linearVelocity.y);
        
    }

    void SpriteFliping(SpriteRenderer spriteRenderer, float horizontalInput)
    {
        //Walking
        if (playerMovement.horizontalInput>0)
        {
            spriteRenderer.flipX = false;
        }
        else if (playerMovement.horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        //Reverse Gravity
         if (targetRB.gravityScale>0)
         {
             spriteRenderer.flipY = false;
         }
         else if (targetRB.gravityScale<0)
         {
             spriteRenderer.flipY = true;
        }
    }
}
