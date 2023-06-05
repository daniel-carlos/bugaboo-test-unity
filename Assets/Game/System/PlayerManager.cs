using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    public int score;

    public void SpawnItem()
    {
        GameObject item = PhotonNetwork.InstantiateRoomObject("item", new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {

        }
        else
        {

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Room {PhotonNetwork.CurrentRoom.Name}");
        GameObject player = PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);


        if (PhotonNetwork.IsMasterClient)
        {
            SpawnItem();
        }
    }

    [PunRPC]
    void OnItemCollected(Player player)
    {
        if (player.IsLocal)
        {
            score++;
        }
    }
}
