using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public List<PlayerInfo> players;
    PlayerInfo currentPlayer;
    public Objective tutorialObjective;
    public GameObject titleScreen;
    public GameObject playerRegistrationUI;
    public GameObject playerHUD;
    public GameObject startGameUI;
    public GameObject highestScoresBoard;
    public TMP_InputField playerNameInputField;
    public TeleportationActivate teleportationActivate;
    public SceneTransitionManager sceneTransitionManager;
    public SaveLoadSystem saveLoadSystem;
    public string tutorialObjectiveDialog;
    public string endTutorialDialog;

    void Start()
    {
        players = saveLoadSystem.LoadPlayerList();
        titleScreen.SetActive(true);
        playerRegistrationUI.SetActive(false);
        startGameUI.SetActive(false);
        tutorialObjective.gameObject.SetActive(false);
        ShowHighestScores();
    }

    public void StartNewPlayer()
    {
        titleScreen.SetActive(false);
        playerRegistrationUI.SetActive(true);
    }


    public void SubmitPlayerName()
    {
        if(playerNameInputField.text != "")
        {
            currentPlayer = new PlayerInfo(playerNameInputField.text);
            playerRegistrationUI.SetActive(false);
            Debug.Log(currentPlayer.Name);
            StartTutorial();
        }
    }

    void StartTutorial()
    {
        EnableTeleport(true);
        playerHUD.GetComponent<HUDManager>().ShowCurrentObjective(tutorialObjectiveDialog);
        tutorialObjective.gameObject.SetActive(true);
    }

    public void EnableTeleport(bool enableTeleport)
    {
        teleportationActivate.EnableTeleport(enableTeleport);
    }

    public void CompletedObjective()
    {
        playerHUD.GetComponent<HUDManager>().ShowCurrentObjective(endTutorialDialog);
        tutorialObjective.gameObject.SetActive(false);
        startGameUI.SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("PlayerName",currentPlayer.Name);
        sceneTransitionManager.GoToScene(1);
    }

    public void ShowHighestScores()
    {   
        if(players.Count > 0) { 
            List<PlayerInfo> sortedPlayerList = players.OrderByDescending(x => x.Score).ToList(); ;
            int index = 5;
            if(sortedPlayerList.Count < index)
            {
                index = sortedPlayerList.Count;
            }
            for(int i = 0; i < index; i++)
            {
                GameObject playerResult = Instantiate(highestScoresBoard.gameObject.transform.Find("Player Result").gameObject,highestScoresBoard.transform);
                playerResult.transform.SetParent(highestScoresBoard.transform, false);
                playerResult.transform.localPosition = new Vector3(0, -i, 0);
                playerResult.gameObject.transform.Find("Player Name Txt").GetComponent<TextMeshProUGUI>().text = sortedPlayerList[i].Name;
                playerResult.gameObject.transform.Find("Player Score Txt").GetComponent<TextMeshProUGUI>().text = sortedPlayerList[i].Score.ToString();
            }
        }
    }

    public void ShowDraggedItemName(string itemName)
    {
        playerHUD.gameObject.GetComponent<HUDManager>().ShowDraggedItemName(itemName);
    }
}
