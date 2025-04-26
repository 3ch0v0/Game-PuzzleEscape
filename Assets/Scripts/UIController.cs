using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI gravityNumText;
    public PlayerController pCScripts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        GetGravityNum();
    }

    void GetGravityNum()
    {
        gravityNumText.text="Gravity Number: "+pCScripts.gravityNum;
    }
}
