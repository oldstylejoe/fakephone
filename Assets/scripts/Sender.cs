using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class Sender : Photon.PunBehaviour {
    
    [Header("Control the calling")]
    public Button call;
    public Button hangup;
    public Text callStatus;

    [Header("The message to send and check for feedback.")]
    public Text textMessage;
    public Toggle yesToggle;
    public Toggle noToggle;

    [Header("The text message will appear here.")]
    public GameObject toastPanel;

    [Header("The receiver for the call signal.")]
    public PhotonView rc;

    [Header("Text slot for the ping return time.")]
    public Text pingStatus;

    // Use this for initialization
    void Start () {
        toastPanel.SetActive(false);
	}

    /// <summary>
    /// Get a photon view for the RPC calls.
    /// </summary>
    public override void OnJoinedRoom()
    {
    }

    /// <summary>
    /// Send the call event over the network.
    /// </summary>
    public void SendCall()
    {
        callStatus.text = "Calling";
        rc.RPC("CallSignal", PhotonTargets.All);
        call.gameObject.SetActive(false);
        hangup.gameObject.SetActive(true);
    }

    /// <summary>
    /// Tell the receiver to end the call.
    /// </summary>
    public void HangupCall()
    {
        Hangup();
        callStatus.text = "Hanging up";
        rc.RPC("HangupSignal", PhotonTargets.All);
    }

    /// <summary>
    /// The receiver hung up the call.
    /// </summary>
    [PunRPC]
    public void ReceiverHungUp()
    {
        UnityEngine.Debug.Log("Hanging up from receiver.");
        callStatus.text = "Receier Hanging up";
        Hangup();
    }

    public void Hangup()
    {
        StopTalking();

        call.gameObject.SetActive(true);
        hangup.gameObject.SetActive(false);

        callStatus.text = "idle";
    }

    /// <summary>
    /// Send message out to all of the receivers.
    /// </summary>
    public void SendText()
    {
        rc.RPC("TextSignalFeedback", PhotonTargets.All, textMessage.text, yesToggle.isOn, noToggle.isOn);
    }
    public void SendText(string t)
    {
        rc.RPC("TextSignalFeedback", PhotonTargets.All, t, yesToggle.isOn, noToggle.isOn);
    }
    public void SendText(string t, bool yes, bool no)
    {
        rc.RPC("TextSignalFeedback", PhotonTargets.All, t, yes, no);
    }

    /// <summary>
    /// Enable the micropone. Basically push-to-talk.
    /// Only the caller has control. We want to hear (and record?) everything the sender says.
    /// </summary>
    [PunRPC]
    public void StartTalking()
    {
        rc.RPC("StopRinging", PhotonTargets.All);
        FindObjectOfType<PhotonVoiceRecorder>().Transmit = true;
    }

    /// <summary>
    /// Disable the micropone. Basically push-to-talk.
    /// </summary>
    [PunRPC]
    public void StopTalking()
    {
        FindObjectOfType<PhotonVoiceRecorder>().Transmit = false;
    }


    /// <summary>
    /// send a ping to the other client(s). start timing.
    /// </summary>
    private Stopwatch stopWatch;
    public void Ping()
    {
        stopWatch = new Stopwatch();
        stopWatch.Stop();
        stopWatch.Reset();
        stopWatch.Start();
        rc.RPC("PingBack", PhotonTargets.AllViaServer);
    }

    [PunRPC]
    public void PingRec()
    {
        stopWatch.Stop();
        pingStatus.text = "Ping Back time: " + stopWatch.ElapsedMilliseconds + " ms";
        UnityEngine.Debug.Log("Ping Back time: " + stopWatch.ElapsedMilliseconds + " ms");
    }

}
