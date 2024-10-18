using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    GameObject player;

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        player = PhotonNetwork.Instantiate("Player", transform.position, transform.rotation);
        //���� �� �ȿ� �ִ� ��� ����鿡�� �÷��̾ �����ȴ�. �׳� ���ӿ��� �ν��Ľ��� �ٸ�
    }
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        PhotonNetwork.Destroy(player);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
