using UnityEngine;

public class PuzzleTriggerWatcher : MonoBehaviour
{
    [HideInInspector]
    public PuzzlePiece parentPiece;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentPiece?.AddPlayer();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parentPiece?.RemovePlayer();
        }
    }

}
