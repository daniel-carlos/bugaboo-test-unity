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
    [Header("------ UI ------")]
    public TMP_InputField inputUsernameField;
    public TMP_InputField inputRoomField;
    [Tooltip("Objetos da UI que serão desabilitados enquanto esstiver carregando")] public Selectable[] inteactableElements;

    [Header("------ User ------")]
    public User user = null;

    public bool loading = true;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnected()
    {
        loading = false;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }

    private void Update()
    {
        foreach (var item in inteactableElements)
        {
            item.interactable = !loading;
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
            Connect();
        }
        else
        {
            loading = false;
        }

    }

    void Connect()
    {
        bool randomRoom = inputRoomField.text == "";

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.NickName = inputUsernameField.text;
        PhotonNetwork.JoinRandomOrCreateRoom();
    }


}