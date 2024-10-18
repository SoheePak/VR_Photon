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
    PhotonView pv; //�� ��ġ���� ȸ������ �ٸ� ���� ���� �� ���δ�

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
                r.enabled = false;   //�� �÷��̾� �ȿ� �ִ� �������� �� ����, ���� �����ϴ� �ָ� �������� Ű�� �ƴϸ� ����
                //�÷��̾� �����ʿ��� �ν��ٽ� �� �� isMine�� true,false�� �����Ѵ� 
            }
        }

    }
    void SetAnimation(InputDevice input, Animator animator)
    { //�Է¿� ���� �ִϸ��̼� �Ķ���� ����
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
