using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject destination;            
    private int totalStars;
    public int collectedStars = 0;
    private bool destinationActive = false;
    public PlayerController player;

    void Start()
    {
        destination?.SetActive(false); 
        
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;
        
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
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            Debug.Log("Game completed");
        }
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}