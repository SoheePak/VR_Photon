using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;       //��Ʈ��ũ ����� ����ϱ� ���� ���ӽ����̽�
using Photon.Realtime;  //�ǽð� ����� �ٷ�� ���ӽ����̽�
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
    { //������ ���������� ����Ǹ� ȣ��ȴ�.

        base.OnConnectedToMaster();                   //�θ� Ŭ������ ��� ȣ��

        /*RoomOptions roomOptions = new RoomOptions(); //���ο� ���� ����ų� �⺻ �濡 �����ϱ� ���� RoomOptions��ü�� �����Ѵ�.
        roomOptions.MaxPlayers = 20;
        PhotonNetwork.JoinOrCreateRoom("skyroom", roomOptions, TypedLobby.Default);
        //JoinOrCreateRoomf�� ����Ͽ� skyroom�̶�� �濡 �����ϰų� ���� ������ ���� �����.*/
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
