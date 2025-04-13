using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]public Rigidbody2D playerRb;

    [SerializeField] public float speed = 5.0f;
    [SerializeField] public float horizontalInput;
    [SerializeField] public int gravityNum=1;
    //[SerializeField] public int gravityDirection=1;
    //[SerializeField] public int maxJumps = 2;
    //public int jumpCount = 0;
    //[SerializeField] public float jumpForce = 2.0f;
    [SerializeField] public float fallMultiplier = 2.5f;
    [SerializeField] public float lowJumpMultiplier = 2.0f;
    //public float fallCheckDistance = 10f;
    //public Vector2 detecionDirection= Vector2.right;
    //[SerializeField] public LayerMask groundLayer;
    [SerializeField] public Vector2 bottomOffset;
    [SerializeField] public float collisionRadius = 0.5f;
    private Collider2D playerCollider;


    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        //FindPlayerCurrentPiece();
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerMovement();
        
        
    }

    void PlayerMovement()
    {
        //transform.rotation=new Quaternion(transform.rotation.x,transform.rotation.y,0,0);
        //Walk
        horizontalInput = Input.GetAxis("Horizontal");
        
        playerRb.linearVelocity = new Vector2(horizontalInput * speed, playerRb.linearVelocity.y);
        
        //Jump
        // if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        // {
        //     playerRb.linearVelocity = new(playerRb.linearVelocity.x, jumpForce);
        //     jumpCount++;
        //     Debug.Log(jumpCount);
        // }
        //
        // if (playerRb.linearVelocity.y == 0 && (Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer)))
        // {
        //     jumpCount = 0;
        // }
        
        if (Input.GetKeyDown(KeyCode.Space)&&gravityNum>0)
        {
            //gravityDirection *= -1;
            playerRb.gravityScale*=-1;
            gravityNum--;
            //playerRb.transform.localScale.y= new Vector3(0.0f,0.0f,0.0f);
            if (playerCollider != null && playerCollider.sharedMaterial != null)
            {
                playerCollider.sharedMaterial.friction = 0f; // 让下落时不摩擦
            }
        }
        
        //Fall
        if (playerRb.linearVelocity.y < 0)
        {
            
            playerRb.linearVelocity += Vector2.up *Physics2D.gravity * (fallMultiplier* Time.deltaTime);
            
            
        }
        
        else if (playerRb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            playerRb.linearVelocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier* Time.deltaTime);
        }
    }
    
    void CheckFallDeath()
    {
        // if (transform.position.y < -50) // **防止意外情况，如果掉得太远直接重置**
        // {
        //     ReloadScene();
        //     return;
        // }

        // if (!IsGroundBelow(fallCheckDistance))
        // {
        //     ReloadScene(); // **如果玩家脚下 10 单位内没有地面，重新加载场景**
        // }
    }
    
    // bool IsGroundBelow(Vector2 direction, float checkDistance)
    // {
    //     Vector2 position = transform.position;
    //     RaycastHit2D hit = Physics2D.Raycast(position, direction, checkDistance, groundLayer);
    //     Debug.DrawRay(position, direction * fallCheckDistance, Color.red, 0.1f);
    //     return hit.collider != null;
    // }
    //
    // // 4️⃣ 重新加载场景（死亡处理）
    // void ReloadScene()
    // {
    //     Debug.Log("玩家坠落，重新加载场景！");
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // **重新加载当前场景**
    // }

    // public void FindPlayerCurrentPiece()
    // {
    //     currentPiece = piecesController.GetPieceAtPosition(transform.position);
    // }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }
}
