using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI txtPlayerName;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtCurrentObjective;
    public TextMeshProUGUI txtDraggedItem;
    public TextMeshProUGUI txtTimer;
    public List<GameObject> hearts;
    public bool gameInProgress;

    void Start()
    {
        txtDraggedItem.text = "";
        txtCurrentObjective.text = "";
        txtTimer.text = "";
    }

    public void SetPlayerInfo(string playerName, int score)
    {
        txtPlayerName.text = playerName;
        txtScore.text = "Score: " + score;
    }

    public void UpdateScore(int score)
    {
        txtScore.text = "Score: " + score;
    }

    public void ShowDraggedItemName(string itemName)
    {
        StartCoroutine(ShowItemName(itemName));
    }

    IEnumerator ShowItemName(string itemName)
    {
        txtDraggedItem.gameObject.SetActive(true);
        txtDraggedItem.text = itemName;
        yield return new WaitForSeconds(10f);
        txtDraggedItem.gameObject.SetActive(false);
    }

    public void ShowCurrentObjective(string currentObjective)
    {
        txtCurrentObjective.text = currentObjective;
    }

    public void UpdateTimer(float minutes, float seconds)
    {
        txtTimer.text = string.Format("{0:00}:{1:00}", minutes,Mathf.RoundToInt(seconds));
    }

    public void UpdateHearts(int currentHealth)
    {
        for(int i = 0; i < hearts.Count; i++)
        {
            hearts[i].SetActive(false);
        }
        for (int i = 0; i < currentHealth; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    public void ShowTimer(bool showTimer)
    {
        txtTimer.gameObject.SetActive(showTimer);
    }

}
