using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [Header("GravityNum UI")]
    public TextMeshProUGUI gravityNumText;
    public PlayerController pCScripts;
    
    [Header("Starting UI")]
    public GameObject pressAnyKeyPanel;

    [Header("Stage UIs")]
    public GameObject[] stageUIs; 
    public GameObject stageClearUI; 

    private bool gameStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) // 主界面才启用Press Any Key
        {
            pressAnyKeyPanel?.SetActive(true);
        }
        else
        {
            pressAnyKeyPanel?.SetActive(false);
            HideAllStageUIs();
            ShowStageUI(SceneManager.GetActiveScene().buildIndex);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && !gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
                pressAnyKeyPanel?.SetActive(false);
                LoadFirstStage();
            }
        }
        GetGravityNum();
    }

    void GetGravityNum()
    {
        gravityNumText.text="Gravity Number: "+pCScripts.gravityNum;
    }
    
    void LoadFirstStage()
    {
        SceneManager.LoadScene(1); // 跳到BuildIndex 1的第一关
    }
    
    public void ShowStageUI(int stageIndex)
    {
        HideAllStageUIs();

        if (stageIndex - 1 < stageUIs.Length && stageIndex > 0)
        {
            stageUIs[stageIndex - 1].SetActive(true);
            Invoke(nameof(HideAllStageUIs), 2f); 
        }
        else
        {
            Debug.LogWarning("Stage index out of range.");
        }
    }

    public void ShowStageClear()
    {
        HideAllStageUIs();
        if (stageClearUI != null)
        {
            stageClearUI.SetActive(true);
        }
    }

    void HideAllStageUIs()
    {
        foreach (var ui in stageUIs)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        if (stageClearUI != null)
            stageClearUI.SetActive(false);
    }
}
