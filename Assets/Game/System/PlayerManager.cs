using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Newtonsoft.Json;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    public List<PlayerController> playerControllers;

    public bool started = false;
    public bool paused = false;
    public bool finished = false;

    public float time = 30f;


    [Header("------ UI ------")]
    public GameObject uiPausedModal;
    public GameObject uiResultsPanel;

    public void ToggoPlayPause()
    {
        if (paused)
        {
            StartGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        paused = false;
        started = true;

        uiResultsPanel.SetActive(false);
        uiPausedModal.SetActive(false);

        playerControllers.ForEach(pc =>
        {
            pc.control = true;
        });
    }

    public void PauseGame()
    {
        // Só pausa se estiver sozinho
        if (PhotonNetwork.CurrentRoom.PlayerCount == 0) { return; }
        paused = true;
        Time.timeScale = 0f;

        uiResultsPanel.SetActive(false);
        uiPausedModal.SetActive(true);

        playerControllers.ForEach(pc =>
        {
            pc.control = false;
        });
    }

    public void ShowResults()
    {
        finished = true;

        uiResultsPanel.SetActive(true);
        uiPausedModal.SetActive(false);

        playerControllers.ForEach(pc =>
        {
            pc.control = false;
        });

        if (photonView.Owner.IsMasterClient)
        {
            playerControllers.ForEach(pc =>
            {
                StartCoroutine(BackendAPI.Put(
                        $"api/user/score",
                        JsonConvert.SerializeObject(new { score = pc.score, username = pc.photonView.Owner.NickName })
                   ));
            });
        }
    }

    public void FinishGame()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Login");
    }

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

        Invoke("StartGame", 3f);
    }

    IEnumerator LoadUserData(PlayerController player)
    {
        yield return null;
    }

    private void Update()
    {
        // excluir da lista de controllers todos que forem nulos
        playerControllers.RemoveAll(item => item == null);

        if (started && !paused && !finished)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0 && !finished)
        {
            time = 0f;
            ShowResults();
            Invoke("FinishGame", 5f);
        }
    }

}
