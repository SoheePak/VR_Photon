using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using UnityEngine.UI;

public class VRChat : MonoBehaviour
{
    public Image backUI;

    List<string> list;
    public Text[] text;
    public InputField chat;

    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        list = new List<string>();
    }

    public void SendTalk()
    {
        string str = PhotonNetwork.NickName + ": " + chat.text;
        pv.RPC("AddTalkRPC",RpcTarget.All,str);  //Invork랑 비슷, 함수를 호출하는것
        chat.text = "";
    }
    [PunRPC]
    void AddTalkRPC(string str)
    {
        while (list.Count >= 5) //채팅창에 5줄만 들어갈 수 있기 때문에 
        {
            list.RemoveAt(0); // 0번 인덱스에 있는 걸 지움
        }

        list.Add(str);
        UpdateTalk();
    }

    void UpdateTalk()
    {
        for(int i = 0; i<list.Count; i++)
        {
            text[i].text = list[i];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
