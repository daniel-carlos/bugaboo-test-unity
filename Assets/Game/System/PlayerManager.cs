using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    public List<PlayerController> playerControllers;

    public void SpawnItem()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject item = PhotonNetwork.InstantiateRoomObject("item", new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
        }
    }

    [PunRPC]
    void OnItemCollected(Player player)
    {
        Debug.Log($"Player score {player.NickName}");
        SpawnItem();
    }

    [PunRPC]
    private void RegisterPlayer(int playerId)
    {
        PhotonView playerView = PhotonNetwork.GetPhotonView(playerId);
        playerControllers.Add(playerView.GetComponent<PlayerController>());
        // StartCoroutine(LoadUserData(playerView.GetComponent<PlayerController>()));
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", Random.insideUnitCircle * 3f, transform.rotation);
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.manager = this;

        // playerControllers.Add(controller);
        photonView.RPC("RegisterPlayer", RpcTarget.All, controller.photonView.ViewID);

        SpawnItem();


    }

    IEnumerator LoadUserData(PlayerController player)
    {
        yield return null;
    }

    private void Update()
    {
        // excluir da lista de controllers todos que forem nulos
        playerControllers.RemoveAll(item => item == null);
    }

}
