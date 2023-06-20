using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private int maxLives = 3;
    private string playerName;
    public int playerLives;
    public int playerScore;
    public List<Objective> objectives;
    public List<string> objectivesDialog;
    private int currentObjective;
    public GameObject HUD;
    public GameObject introScreen;
    public GameObject resultScreen;
    public TeleportationActivate teleportationActivate;
    float timer;
    public float maxTime = 120.0f;
    public bool objectiveInProgress;
    bool gameCompleted;
    public SceneTransitionManager sceneTransitionManager;
    public SaveLoadSystem saveLoadSystem;
    List<PlayerInfo> playersList;

    void Start()
    {
        if (playerLives>maxLives) {
            playerLives = maxLives;
        }
        HUD.GetComponent<HUDManager>().UpdateHearts(playerLives);
        EnableTeleport(false);
        playerName = PlayerPrefs.GetString("PlayerName");
        playerScore = 0;
        currentObjective = -1;
        ActivateObjective(currentObjective);
        HUD.GetComponent<HUDManager>().SetPlayerInfo(playerName,playerScore);
        resultScreen.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (objectiveInProgress) { 
            timer -= Time.deltaTime;
            float minutes = Mathf.Floor(timer / 60);
            float seconds = timer % 60;
            HUD.GetComponent<HUDManager>().UpdateTimer(minutes, seconds);
            if (timer <= 0.0f)
            {
                ReducePlayerLives();
            }
        }

    }

    private void ReducePlayerLives()
    {
        playerLives--;
        HUD.GetComponent<HUDManager>().UpdateHearts(playerLives);
        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            ActivateObjective(currentObjective);
        }
    }

    private void GameOver()
    {
        ActivateObjective(-1);
        objectiveInProgress = false;
        HUD.GetComponent<HUDManager>().ShowTimer(false);
        HUD.GetComponent<HUDManager>().ShowCurrentObjective("");
        EnableTeleport(false);
        resultScreen.SetActive(true);
        player.transform.position = new Vector3(0,player.transform.position.y,0);
        player.transform.rotation = Quaternion.identity;
        if (gameCompleted)
        {
            SetResultInfo(playerScore, objectives.Count,objectives.Count);
        }
        else
        {
            SetResultInfo(playerScore, (currentObjective - 1), objectives.Count);
        }
        
    }

    public void StartGame()
    {
        EnableTeleport(true);
        currentObjective = 1;
        ActivateObjective(currentObjective);
    }

    void ActivateObjective(int objective)
    {
        for(int i = 0; i < objectives.Count; i++)
        {
            if((i+1) == objective)
            {
                objectives[i].gameObject.SetActive(true);
                HUD.GetComponent<HUDManager>().ShowCurrentObjective(objectivesDialog[i]);
                timer = maxTime;
                objectiveInProgress = true;
            }
            else
            {
                objectives[i].gameObject.SetActive(false);
            }
        }
    }

    public void CompletedObjective()
    {
        objectiveInProgress = false;
        playerScore = playerScore + (objectives[currentObjective-1].GetObjectivePoints()+ (int)(objectives[currentObjective - 1].GetObjectivePoints()*(timer/maxTime)));
        HUD.GetComponent<HUDManager>().UpdateScore(playerScore);
        if (currentObjective != objectives.Count)
        {
            currentObjective++;
            ActivateObjective(currentObjective);
        }
        else
        {
            if (playerLives > 0)
            {
                playerScore = playerScore*playerLives;
                HUD.GetComponent<HUDManager>().UpdateScore(playerScore);
            }
            gameCompleted = true;
            GameOver();
        }
        Debug.Log(currentObjective);
    }

    public void ShowDraggedItemName(string itemName)
    {
        HUD.GetComponent<HUDManager>().ShowDraggedItemName(itemName);
    }

    public void CloseIntroScreen()
    {
        introScreen.gameObject.SetActive(false);
        StartGame();
    }

    public void EnableTeleport(bool enableTeleport)
    {
        teleportationActivate.EnableTeleport(enableTeleport);
    }


    public void SetResultInfo(int score, int numCompletedObjectives, int numTotalObjectives)
    {
        resultScreen.GetComponent<ResultScreenManager>().SetResultInfo(score, numCompletedObjectives, numTotalObjectives);
    }

    public void RestartGame()
    {
        sceneTransitionManager.GoToScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        playersList = saveLoadSystem.LoadPlayerList();
        PlayerInfo currentPlayer = new PlayerInfo(playerName,playerScore);
        playersList.Add(currentPlayer);
        saveLoadSystem.SavePlayerList(playersList);
        sceneTransitionManager.GoToScene(0);
    }
}
