using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endScoreText;
    
    // Start is called before the first frame update
    void Awake()
    {
        PointSystem.OnPointsChanged += SetEndScore;
    }

    private void SetEndScore(int score)
    {
        endScoreText.text = score + "";
    }
}
