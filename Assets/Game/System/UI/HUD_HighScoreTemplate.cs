using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_HighScoreTemplate : UI_Template<User>
{
    public TMP_Text nameLabel;
    public TMP_Text scoreLabel;

    public override void RefreshTemplate(User user)
    {
        nameLabel.text = user.username;
        scoreLabel.text = $"{user.score}";
    }
}
