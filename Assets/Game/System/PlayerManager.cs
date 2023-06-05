using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{

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


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.manager = this;

        SpawnItem();
    }

}
