using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Player " + ObjectiveReachTargets.playerWin + " WIN";
    } 
}
