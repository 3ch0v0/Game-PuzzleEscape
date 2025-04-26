using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Puzzle State")]
    public bool isFixed = false;
    public bool isRotatable = true;
    
    private bool defaultRotatable;
    
    [Header("Puzzle Zone")]
    public Vector2 zoneSize = new Vector2(40f, 40f); 
    public float zoneOffset = 0f;
    public int playerCount = 0;
    
    void Start()
    {
        defaultRotatable = isRotatable;
        CreatePuzzleZone();
    }

    void CreatePuzzleZone()
    {
        GameObject triggerZone = new GameObject("PlayerTriggerZone");
        triggerZone.transform.SetParent(transform);
        triggerZone.transform.localPosition = new Vector3(0, zoneOffset, 0);

        BoxCollider2D trigger = triggerZone.AddComponent<BoxCollider2D>();
        trigger.size = zoneSize;
        trigger.isTrigger = true;

        PuzzleTriggerWatcher watcher = triggerZone.AddComponent<PuzzleTriggerWatcher>();
        watcher.parentPiece = this;
    }

    public void AddPlayer()
    {
        playerCount++;
        isRotatable = false;
    }

    public void RemovePlayer()
    {
        playerCount--;
        if (playerCount <= 0 && !isFixed)
        {
            isRotatable = defaultRotatable;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Vector3 offset = new Vector3(0, zoneOffset, 0);
        Vector3 size = new Vector3(zoneSize.x, zoneSize.y, 0);
        Gizmos.DrawWireCube(transform.position + offset, size);
    }
#endif

}
