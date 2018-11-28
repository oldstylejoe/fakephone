using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageVoice : Photon.MonoBehaviour {

    private GameObject voicePrefab;

    /// <summary>
    /// Called by photon.
    /// </summary>
    public void OnJoinedRoom()
    {
        Debug.Log("ManageVoice: Joined the room.");

        voicePrefab = PhotonNetwork.Instantiate("PUNVoicePrefab", Vector3.zero, Quaternion.identity, 0);
    }

    /// <summary>
    /// Start transmitting voice.
    /// </summary>
    public void StartTransmitting() {
        voicePrefab.GetComponent<PhotonVoiceRecorder>().Transmit = true;
    }

    /// <summary>
    /// Stop transmitting voice (may still listen).
    /// </summary>
    public void StopTransmitting() {
        voicePrefab.GetComponent<PhotonVoiceRecorder>().Transmit = false;
    }

}
