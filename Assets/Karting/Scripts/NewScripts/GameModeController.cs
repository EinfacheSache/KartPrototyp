using TMPro;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    [SerializeField] private GameObject powerUpCountParent;
    [SerializeField] private TimeDisplayItem firstPlayerTimeText;
    [SerializeField] private TimeDisplayItem secondPlayerTimeText;
    [SerializeField] private bool selectionFinisched = false;

    [SerializeField] private static GameMode gameMode = GameMode.None;

    // Start is called before the first frame update
    void Start()
    {
        powerUpCountParent.SetActive(false);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void gameSelected(int value) 
    { 
       if(value == 1)
        {
            Debug.Log("PowerRush");

            gameMode = GameMode.PowerRush;
            powerUpCountParent.SetActive(true);
            selectionFinisched = true;

        }
        else if(value == 2)
        {
            Debug.Log("KingOfTheHill");

            gameMode = GameMode.KingOfTheHill;
            firstPlayerTimeText.SetText(firstPlayerTimeText.getTimeString(45));
            secondPlayerTimeText.SetText(secondPlayerTimeText.getTimeString(45));
            selectionFinisched = true;
        }
        else if (value == 3)
        {
            Debug.Log("LapsMaster");

            gameMode = GameMode.LapsMaster;
            firstPlayerTimeText.SetText("0 / 5");
            secondPlayerTimeText.SetText("0 / 5");
            selectionFinisched = true;
        }

        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public enum GameMode
    {
        None,
        PowerRush,
        KingOfTheHill,
        LapsMaster
    }

    public static GameMode GetGameMode()
    {
        return gameMode;
    }

    public bool getSelectionFinisched()
    {
        return selectionFinisched;
    }

}
