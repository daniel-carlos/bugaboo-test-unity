using Photon.Pun;
using UnityEngine;

public class Item : MonoBehaviourPun
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.OnColletTitem();
            if (PhotonNetwork.IsMasterClient){
                PhotonNetwork.Destroy(photonView);
            }
        }
    }
}
