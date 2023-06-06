using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScore : UI_List<PlayerController>
{
    public PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateList(playerManager.playerControllers);
    }
}
