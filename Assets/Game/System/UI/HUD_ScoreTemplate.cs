using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_ScoreTemplate : UI_Template<PlayerController>
{
    public TMP_Text nameLabel;
    public TMP_Text scoreLabel;

    public override void RefreshTemplate(PlayerController input)
    {
        nameLabel.text = input.photonView.Owner.NickName;
        scoreLabel.text = $"{input.score}";
    }
}
