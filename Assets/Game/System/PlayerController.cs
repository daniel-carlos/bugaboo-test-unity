using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputType inputType;
    public float speed = 2f;
    public Rigidbody2D body;
    public PhotonView view;

    public Vector2 moveIntention = Vector2.zero;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!view.IsMine) { return; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!view.IsMine) { return; }
        body.velocity = speed * new Vector3(moveIntention.x, moveIntention.y);
    }

    public void Move(Vector2 dir)
    {
        moveIntention = dir.normalized;
    }

    public void OnColletTitem()
    {
        view.RPC("OnItemCollected", RpcTarget.All, view.Owner);
    }
}


public enum InputType
{
    Keyboard,
    Gamepad
}