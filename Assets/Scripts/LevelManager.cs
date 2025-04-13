using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject destination;          // 终点
    public Text starCounterText;            // UI 显示（Text 或 TextMeshProUGUI）
    private int totalStars;
    public int collectedStars = 0;
    private bool destinationActive = false;
    public PlayerController player;

    void Start()
    {
        destination?.SetActive(false); // 关掉传送点

        // 统计星星总数
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
        
        //UpdateStarUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }
    
    public void CollectStar()
    {
        collectedStars++;
        player.gravityNum++;
        //UpdateStarUI();

        if (collectedStars >= totalStars && !destinationActive)
        {
            ActivateDestination();
        }
    }

    // void UpdateStarUI()
    // {
    //     if (starCounterText != null)
    //     {
    //         starCounterText.text = $"Stars: {collectedStars} / {totalStars}";
    //     }
    // }

    void ActivateDestination()
    {
        destinationActive = true;
        destination?.SetActive(true);
        Debug.Log("Destination activated!");
    }

    public void TriggerLevelComplete()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (currentIndex < totalScenes - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("🎉 Game completed!");
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}