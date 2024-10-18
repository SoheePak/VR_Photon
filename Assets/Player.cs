using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Unity.XR.CoreUtils;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    public Transform playerHead;
    public Transform playerLeft;
    public Transform playerRight;

    Transform xrHead;
    Transform xrLeft;
    Transform xrRight;
    PhotonView pv; //내 위치값과 회전값을 다른 곳을 보낼 떄 쓰인다

    public Animator animatorL;
    public Animator animatorR;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        XROrigin o = FindObjectOfType<XROrigin>();
        xrHead = o.transform.Find("Camera Offset/Main Camera");
        xrLeft = o.transform.Find("Camera Offset/LeftHand Controller");
        xrRight = o.transform.Find("Camera Offset/RightHand Controller");
        if (pv.IsMine)
        {
            foreach (var r in GetComponentsInChildren<Renderer>())
            {
                r.enabled = false;   //내 플레이어 안에 있는 랜더러를 다 끊다, 내가 조종하는 애만 랜더러를 키고 아니면 끊다
                //플레이어 스포너에서 인스텐스 할 때 isMine이 true,false를 설정한다 
            }
        }

    }
    void SetAnimation(InputDevice input, Animator animator)
    { //입력에 따라 애니메이션 파라미터 변경
        if (input.TryGetFeatureValue(CommonUsages.trigger, out float t))
            animator.SetFloat("Trigger", t);
        else animator.SetFloat("Trigger", 0);

        if (input.TryGetFeatureValue(CommonUsages.grip, out float g))
            animator.SetFloat("Grip", g);
        else animator.SetFloat("Grip", 0);
    }
    void CopyTransform(Transform t, Transform s)
    {
        t.position = s.position;
        t.rotation = s.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            CopyTransform(playerHead, xrHead);
            CopyTransform(playerLeft, xrLeft);
            CopyTransform(playerRight, xrRight);

            SetAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), animatorL);
            SetAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), animatorR);
        }
    }
}
