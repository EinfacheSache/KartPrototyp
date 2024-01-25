using KartGame.KartSystems;
using System.Collections;
using UnityEngine;
using static GameModeController;

public class ObjectiveReachTargets : MonoBehaviour
{


    public static int playerWin;
    private float firstPlayerTime = 45;
    private float secondPlayerTime = 45;
    private float firstPlayerLaps = 0;
    private float secondPlayerLaps = 0;
    private float firstPlayerLastProgress = 0;
    private float secondPlayerLastProgress = 0;
    private int playerInLead = 0;

    [SerializeField] private GameObject[] players;
    [SerializeField] private TimeDisplayItem firstPlayerTimeText;
    [SerializeField] private TimeDisplayItem secondPlayerTimeText;

    void Start()
    {
        playerWin = 0;
    }

    private void Update()      
    {

        int progressFirstPlayer = players[0].GetComponent<ResetController>().getCheckPointCount();
        int progressSecondPlayer = players[1].GetComponent<ResetController>().getCheckPointCount();

        if (GameModeController.GetGameMode() == GameMode.PowerRush)
        {
            foreach (var player in players)
            {
                if (player.GetComponent<PowerUpCollect>().powerUpCount == 25 && playerWin == 0)
                {
                    OnPlayerWin(player);
                }
            }
        }

        else if (GameModeController.GetGameMode() == GameMode.KingOfTheHill)
        {

            if(firstPlayerTime <= 0)
            {
                OnPlayerWin(players[0]);
                return;
            }else if(secondPlayerTime <= 0)
            {
                OnPlayerWin(players[1]);
                return;
            }


            if (progressFirstPlayer > progressSecondPlayer)
            {
                playerInLead = 1;
                firstPlayerTime = firstPlayerTime - Time.deltaTime;
                firstPlayerTimeText.SetText(firstPlayerTimeText.getTimeString(firstPlayerTime));
            }
            else if (progressFirstPlayer < progressSecondPlayer)
            {
                playerInLead = 2;
                secondPlayerTime = secondPlayerTime - Time.deltaTime;
                secondPlayerTimeText.SetText(secondPlayerTimeText.getTimeString(secondPlayerTime));
            }
            else
            {
                if (playerInLead == 1)
                {
                    firstPlayerTime = firstPlayerTime - Time.deltaTime;
                    firstPlayerTimeText.SetText(firstPlayerTimeText.getTimeString(firstPlayerTime));
                }
                else if (playerInLead == 2)
                {
                    secondPlayerTime = secondPlayerTime - Time.deltaTime;
                    secondPlayerTimeText.SetText(secondPlayerTimeText.getTimeString(secondPlayerTime));
                }
            }
        }else if(GameModeController.GetGameMode() == GameMode.LapsMaster)
        {
            if (firstPlayerLaps >= 5)
            {
                OnPlayerWin(players[0]);
                return;
            }
            if (secondPlayerLaps >= 5)
            {
                OnPlayerWin(players[1]);
                return;
            }

            if (progressFirstPlayer % 56 == 0 && firstPlayerLastProgress != progressFirstPlayer)
            {


                firstPlayerLaps++;
                firstPlayerLastProgress = progressFirstPlayer;
                firstPlayerTimeText.SetText(firstPlayerLaps + " / 5");
            }
            if (progressSecondPlayer % 56 == 0 && secondPlayerLastProgress != progressSecondPlayer)
            {
                secondPlayerLaps++; 
                secondPlayerLastProgress = progressSecondPlayer;
                secondPlayerTimeText.SetText(secondPlayerLaps + " / 5");
            }
        }
    }


    void OnPlayerWin(GameObject player)
    {
        Debug.Log($"Player {player.GetComponent<KeyboardInput>().playerID} win the game");
        playerWin = player.GetComponent<KeyboardInput>().playerID;
    }
}
