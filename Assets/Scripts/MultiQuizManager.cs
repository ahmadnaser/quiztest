using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiQuizManager : Photon.PunBehaviour {

    PhotonView photonView;

    float myTime;
    float otherTime;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void SendAllAnswerTime(float time) {
        myTime = time;
        photonView.RPC("SendTimeRPC", PhotonTargets.Others,time,isFasterThan());

    }

    [PunRPC]
    private void SendTimeRPC(float time,UnityAction<bool> callback)
    {
        otherTime = time;
    }
    public bool isFasterThan()
    {
        return true;
    }
}
