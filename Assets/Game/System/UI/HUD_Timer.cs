using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Timer : MonoBehaviour
{
    public PlayerManager manager;
    public TMP_Text label;

    void Start()
    {
        label = GetComponent<TMP_Text>();
    }

    void Update()
    {
        int mins = Mathf.FloorToInt( manager.time / 60);
        int seconds = Mathf.FloorToInt(manager.time % 60);
        label.text = $"{mins.ToString("00")}:{seconds.ToString("00")}";
    }
}
