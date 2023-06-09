using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseButton : MonoBehaviour
{
    public PlayerManager playerManager;

    [Header("------ Icon ------")]
    public Image targetIconImage;
    public Sprite playIcon;
    public Sprite pauseIcon;

    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Destroy(gameObject);
            return;
        };
        if (PhotonNetwork.CurrentRoom.PlayerCount > 1) Destroy(gameObject);
        if (playerManager == null) { return; }
        targetIconImage.sprite = playerManager.paused ? playIcon : pauseIcon;
    }
}
