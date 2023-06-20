using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultScreenManager : MonoBehaviour
{
    public TextMeshProUGUI txtFinalScore;
    public TextMeshProUGUI txtNumCompletedObjectives;

    public void SetResultInfo(int score, int numCompletedObjectives, int numTotalObjectives)
    {
        txtFinalScore.text = "Score: "+score.ToString();
        txtNumCompletedObjectives.text = "You've completed " + numCompletedObjectives.ToString()+ " objectives of "+numTotalObjectives+".";
    }
}
