using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;       //네트워크 기능을 사용하기 위한 네임스페이스
using Photon.Realtime;  //실시간 기능을 다루는 네임스페이스
using System.Xml.Serialization;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public List<Room> listRoom;
    public GameObject roomUI;

    public InputField NickName;
   
    [System.Serializable]
    public class Room
    {
        public string name;
        public int scene;
    }

    void Start()
    {
        //ServerConnect();
    }
    public void ServerConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
   
    public override void OnConnectedToMaster()
    { //서버에 성공적으로 연결되면 호출된다.

        base.OnConnectedToMaster();                   //부모 클래스의 기능 호출

        /*RoomOptions roomOptions = new RoomOptions(); //새로운 방을 만들거나 기본 방에 참가하기 위해 RoomOptions객체를 생성한다.
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("skyroom", roomOptions, TypedLobby.Default);
        //JoinOrCreateRoomf를 사용하여 skyroom이라는 방에 참가하거나 방이 없으면 새로 만든다.*/
        PhotonNetwork.JoinLobby();
    }
    public void CreateRoom(int roomIndex)
    {
        PhotonNetwork.LoadLevel(listRoom[roomIndex].scene);     
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom(listRoom[roomIndex].name, roomOptions, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Room joined");
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.LocalPlayer.NickName = NickName.text;
        roomUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
