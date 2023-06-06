using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{
    public InputType inputType;
    public float speed = 2f;
    public Rigidbody2D body;

    public Vector2 moveIntention = Vector2.zero;


    public PlayerManager manager;

    public int score = 0;

    public bool control = false;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!photonView.IsMine) { return; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine) { return; }
        if(!control) { return; }
        body.velocity = speed * new Vector3(moveIntention.x, moveIntention.y);
    }

    public void Move(Vector2 dir)
    {
        moveIntention = dir.normalized;
    }

    public void OnColletTitem()
    {
        if (photonView.IsMine)
        {
            score++;
            manager.photonView.RPC("OnItemCollected", RpcTarget.All, photonView.Owner);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Envia o valor da propriedade para os outros clientes
            stream.SendNext(score);
        }
        else if (stream.IsReading)
        {
            // Recebe o valor da propriedade dos outros clientes
            score = (int)stream.ReceiveNext();
        }
    }
}


public enum InputType
{
    Keyboard,
    Gamepad
}