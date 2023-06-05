using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;

public class LoginController : MonoBehaviourPunCallbacks
{
    [Header("------ UI (Inputs) ------")]
    public TMP_InputField inputUsernameField;
    public TMP_InputField inputRoomField;
    public Toggle offlineToggle;

    [Header("------ UI (Panels) ------")]
    public GameObject formPanel;
    public GameObject connectingPanel;
    public GameObject waitingPanel;

    [Header("------ User ------")]
    public User user = null;

    public bool loading = false;
    public bool confirmed = false;
    public bool inRoom = false;

    private void Start()
    {

    }


    public void Cancel()
    {
        loading = false;
        confirmed = false;
        inRoom = false;
        PhotonNetwork.Disconnect();
    }

    bool started = false;
    private void Update()
    {
        formPanel.SetActive(!confirmed);
        connectingPanel.SetActive(confirmed && !PhotonNetwork.IsConnected);
        waitingPanel.SetActive(confirmed && PhotonNetwork.IsConnected);

        if (PhotonNetwork.IsConnected && PhotonNetwork.CurrentRoom != null)
        {
            Debug.Log($"Connected {PhotonNetwork.CurrentRoom}");
            if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers && !started)
            {
                started = true;
                PhotonNetwork.LoadLevel("Gameplay");
            }
        }
    }

    public void LoginWithUsername()
    {
        string username = inputUsernameField.text;
        username = username.Replace(" ", "");
        if (username.Length >= 3)
        {
            StartCoroutine(AsyncLogin(username));
        }
    }

    IEnumerator AsyncLogin(string username)
    {
        loading = true;
        bool ok = false;
        yield return BackendAPI.Get($"api/user/find/{username}", (req, res) =>
        {
            try
            {
                this.user = res.SelectToken("user").ToObject<User>();
                ok = true;
            }
            catch (System.Exception)
            {
                Debug.Log("Error");
            }
        });
        if (!ok)
        {
            yield return BackendAPI.Post($"api/user", JsonConvert.SerializeObject(new User(username)), (req, res) =>
            {
                try
                {
                    this.user = res.SelectToken("user").ToObject<User>();
                    ok = true;
                }
                catch (System.Exception)
                {
                    Debug.Log("Error");
                }
            });
        }

        if (ok)
        {
            confirmed = true;
            Connect();
        }
        else
        {
            loading = false;
        }

    }

    void Connect()
    {
        Debug.Log("Conectar");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado ao Master");

        RoomOptions options = new RoomOptions();
        PhotonNetwork.NickName = inputUsernameField.text;

        if (offlineToggle.isOn)
        {
            options.MaxPlayers = 1;
            PhotonNetwork.CreateRoom(PhotonNetwork.NickName, options);
        }
        else
        {
            options.MaxPlayers = 2;
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: options);
        }

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou numa Sala");
        inRoom = true;
    }
}