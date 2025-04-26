using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzlePiecesController : MonoBehaviour
{
    [Header("PuzzleSetting")]
    [SerializeField]public GameObject[] pieces;
    [SerializeField]public float pieceSize=40;
    [SerializeField]public GameObject emptyPiece;
    //public Vector3 emptyPiecePos;
     //private PlayerController player;
    public Rigidbody2D playerRb;
    [Header("Interaction Animation")] 
    public float minScale = 0.5f;
    public float pieceRotationDuration=0.5f;
    public float pieceMovingDuration=0.5f;
    private Vector3 pieceOriginalScale;

    void Start()
    {
        //player=GetComponent<PlayerController>();
        //playerRb = GetComponent<Rigidbody2D>();
        pieceOriginalScale=transform.localScale;
    }
    
    void Update()
    {
        //Move the piece
        if (Input.GetMouseButtonDown(0))
        {
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 
            
            CheckAndMovePiece(mousePosition);
        }
        //Rotate the piece
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
                
            playerRb.constraints = RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation; 
            CheckAndRotatePiece(mousePosition);
            playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            //player.playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    
    
    void CheckAndMovePiece(Vector3 mousePosition)
    {
        foreach (GameObject piece in pieces)
        {
            PuzzlePiece pieceScript = piece.GetComponent<PuzzlePiece>();
            if (Vector3.Distance(piece.transform.position, mousePosition) < pieceSize*0.5)
            {
                if (pieceScript.isFixed)
                {
                    Debug.Log("Can't move piece！");
                    return;
                }
                if (IsAdjacent(piece.transform.position, emptyPiece.transform.position))
                {
                    
                    
                    Debug.Log(piece.transform.position);
                    StartCoroutine(SwapPiece(piece, pieceScript));
                    
                }
                break;
            }
        }
    }

    void CheckAndRotatePiece(Vector3 mousePosition)
    {
        foreach (GameObject piece in pieces)
        {
            
            PuzzlePiece pieceScript = piece.GetComponent<PuzzlePiece>();
            if (Vector3.Distance(piece.transform.position, mousePosition) < pieceSize*0.5)
            {
                if (pieceScript.isFixed||pieceScript.isRotatable==false)
                {
                    Debug.Log("Can't rotate piece！");
                    return;
                }
                StartCoroutine(RotatePiece(piece, pieceScript));
                break;
            }
        }
    }

    //detect movable piece
    bool IsAdjacent(Vector2 piecePosition, Vector2 emptyPosition)
    {
        float distanceX = Mathf.Abs(piecePosition.x - emptyPosition.x);
        float distanceY = Mathf.Abs(piecePosition.y - emptyPosition.y);
        return (distanceX == pieceSize && distanceY == 0) || (distanceX == 0 && distanceY == pieceSize);
    }

    //change position with the emptyslot
    IEnumerator SwapPiece(GameObject piece, PuzzlePiece pieceScript)
    {
        Debug.Log("Move piece");
        Vector2 pieceOldPos = piece.transform.position;
        Vector2 pieceNewPos = emptyPiece.transform.position;
        float timeElapsed = 0f;
        float duration = pieceMovingDuration;
        
        //Disable colliders in the piece before moving
        Collider2D[] colliders = piece.GetComponentsInChildren<Collider2D>();
        if (pieceScript.playerCount == 0)
        {
            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }
        }
        
        emptyPiece.SetActive(false);
        
        while (timeElapsed < duration)
        {
            piece.transform.position = Vector2.Lerp(pieceOldPos, pieceNewPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        piece.transform.position = pieceNewPos;
        emptyPiece.transform.position = pieceOldPos;
        emptyPiece.SetActive(true);
        
        //enable colliders in the piece after moving
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
        }
        
    }
    
    IEnumerator RotatePiece(GameObject piece,PuzzlePiece pieceScript)
    {
        
        Debug.Log("Rotating piece");
        Quaternion pieceOldRotation = piece.transform.rotation;
        Quaternion newRotation = piece.transform.rotation * Quaternion.Euler(0,0,90.0f);
        Vector3 rotatingScale = piece.transform.localScale * minScale;
        float timeElapsed = 0f;
        float duration = pieceRotationDuration;
        
        //Disable colliders in the piece before rotating
        Collider2D[] colliders = piece.GetComponentsInChildren<Collider2D>();
        if (pieceScript.playerCount == 0)
        {
            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }
        }
        
        while (timeElapsed < duration)
        {
            float t= timeElapsed / duration;
            if (t < 0.5f)
            {
                float subT = t / 0.5f;
                piece.transform.localScale = Vector3.Lerp(pieceOriginalScale, rotatingScale, subT);
            }
            else
            {
                float subT = (t - 0.5f) / 0.5f;
                piece.transform.localScale = Vector3.Lerp(rotatingScale, pieceOriginalScale, subT);
            }

            piece.transform.rotation=Quaternion.Slerp(pieceOldRotation, newRotation, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        piece.transform.localScale = pieceOriginalScale;
        
        Vector3 rotation = piece.transform.eulerAngles;
        rotation.z = Mathf.Round(rotation.z / 90) * 90; 
        piece.transform.eulerAngles = rotation;
        piece.transform.rotation = Quaternion.Euler(rotation);
        
        //enable colliders in the piece after rotating
        foreach (Collider2D col in colliders)
        {
            col.enabled = true;
        }
    }
}
