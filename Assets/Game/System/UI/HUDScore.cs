public class HUDScore : UI_List<PlayerController>
{
    public PlayerManager playerManager;

    void Update()
    {
        UpdateList(playerManager.playerControllers);
    }
}
