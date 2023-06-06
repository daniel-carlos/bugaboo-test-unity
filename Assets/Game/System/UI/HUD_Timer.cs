using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_Timer : MonoBehaviour
{
    public PlayerManager manager;
    public TMP_Text label;

    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int mins = Mathf.FloorToInt( manager.time / 60);
        int seconds = Mathf.FloorToInt(manager.time % 60);
        label.text = $"{mins.ToString("00")}:{seconds.ToString("00")}";
    }
}
