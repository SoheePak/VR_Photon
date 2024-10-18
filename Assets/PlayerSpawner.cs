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
        //같은 룸 안에 있는 모든 사람들에게 플레이어가 생성된다. 그냥 게임에서 인스탠스랑 다름
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
