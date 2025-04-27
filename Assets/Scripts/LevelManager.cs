using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject stageContent;
    public GameObject destination;            
    private int totalStars;
    public int collectedStars = 0;
    private bool destinationActive = false;
    public PlayerController player;
    
    private UIController uiController;
    private bool stageCompleted = false;

    void Start()
    {
        uiController = FindObjectOfType<UIController>();
        destination?.SetActive(false); 
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
        
        if (stageContent != null)
            stageContent.SetActive(false); // 一开始隐藏地图内容

        if (SceneManager.GetActiveScene().buildIndex != 0) // 如果不是菜单
        {
            if (uiController != null)
            {
                uiController.ShowStageUI(SceneManager.GetActiveScene().buildIndex);
                Invoke(nameof(ShowLevelContent), 2f); // 2秒后显示地图
            }
        }
        else
        {
            // 如果是菜单Scene（BuildIndex=0），直接显示levelContent（比如背景动画）
            if (stageContent != null)
                stageContent.SetActive(true);
        }
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

        if (collectedStars >= totalStars && !destinationActive)
        {
            ActivateDestination();
        }
    }
    

    void ActivateDestination()
    {
        destinationActive = true;
        destination?.SetActive(true);
        Debug.Log("Destination activated");
    }

    public void TriggerLevelComplete()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        if (currentIndex < totalScenes - 1)
        {
            StartCoroutine(LoadNextStage(currentIndex + 1));
        }
        else
        {
            if (uiController != null)
            {
                uiController.ShowStageClear();
            }
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void ShowLevelContent()
    {
        if (stageContent != null)
            stageContent.SetActive(true);
    }
    private System.Collections.IEnumerator LoadNextStage(int nextIndex)
    {
        if (uiController != null)
        {
            uiController.ShowStageUI(nextIndex); // 切关时也弹出新的Stage UI
            yield return new WaitForSeconds(2f); // 等2秒
        }

        SceneManager.LoadScene(nextIndex);
    }
}