using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiQuizManager : Photon.PunBehaviour {

    PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void SendAllAnswerTime(float time) {
        float myTime = time;
        photonView.RPC("SendTimeRPC", PhotonTargets.Others,time);

    }

    [PunRPC]
    private void SendTimeRPC(float time)
    {

    }
    public bool isFasterThan()
    {
        return true;
    }
}
