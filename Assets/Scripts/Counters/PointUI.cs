using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    
    private void Awake()
    {
        PointSystem.OnPointsChanged += ChangePointText;
    }

    private void ChangePointText(int point)
    {
        pointText.text = point + "";
    }
}
