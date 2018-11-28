/* Joe Snider
 * 4/18
 * 
 * Handle the receiver events */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receiver : Photon.PunBehaviour {

    [Header("Placeholer if we want a status object later.")]
    public GameObject status;

    [Header("Buttons that pop up for the phone logic")]
    public Button answer;
    public Button hangup;

    [Header("Buttons that popup for the messaging logic")]
    public Button yesButton;
    public Button noButton;

    [Header("Text toaster")]
    public ToastManager toastObject;

    [Header("The sender for the call.")]
    public PhotonView sender;

    [Header("Colors for the talking icon")]
    public Color phoneActive;
    public Color phoneInactive;
    public Image talkingIcon;

    /// <summary>
    /// coordinate the initial state
    /// </summary>
    /*
    private void OnEnable()
    {
        answer.gameObject.SetActive(false);
        hangup.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        answer.gameObject.SetActive(false);
        hangup.gameObject.SetActive(false);
    }*/
    private void Start()
    {
        answer.gameObject.SetActive(false);
        hangup.gameObject.SetActive(false);
    }

    //logic for the call handling.
    //not very robust for this controlled environment.
    private Coroutine ringerJob = null;
    private bool busy = false;

    /// <summary>
    /// Enable the answer button and ring the bell.
    /// </summary>
    private IEnumerator Ringer()
    {
        answer.gameObject.SetActive(true);
        for (int i = 0; i < 10; ++i)
        {
            AudioManager.instance.RingPhone();
            yield return new WaitForSeconds(AudioManager.instance.phoneSource.clip.length + 0.1f);
        }
    }

    /// <summary>
    /// Answer the incoming call.
    /// </summary>
    public void Answer()
    {
        answer.gameObject.SetActive(false);
        talkingIcon.color = phoneActive;
        hangup.gameObject.SetActive(true);

        //StartTalking also stops the ringer.
        sender.RPC("StartTalking", PhotonTargets.Others);
    }

    public void Hangup()
    {
        if (busy)
        {
            sender.RPC("ReceiverHungUp", PhotonTargets.All);
        }
        sender.RPC("StopTalking", PhotonTargets.Others);

        hangup.gameObject.SetActive(false);
        talkingIcon.color = phoneInactive;
        StopCoroutine(ringerJob);
        AudioManager.instance.StopPhone();
        busy = false;
    }

    /// <summary>
    /// Activate the button and wait for calls.
    /// </summary>
    [PunRPC]
    void CallSignal()
    {
        Debug.Log("received a call");
        if (busy)
        {
            Debug.LogError("Dropped call: Called while busy.");
        }
        else
        {
            busy = true;
            ringerJob = StartCoroutine(Ringer());
        }
    }

    [PunRPC]
    public void StopRinging()
    {
        StopCoroutine(ringerJob);
        AudioManager.instance.StopPhone();
    }

    [PunRPC]
    public void HangupSignal()
    {
        Debug.Log("Hanging up from the caller.");
        Hangup();
    }

    [PunRPC]
    public void TextSignal(string message)
    {
        Debug.Log("received text: " + message);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        toastObject.DisplayToast(message);
        AudioManager.instance.AlertText();
    }

    /// <summary>
    /// Present a message in the toast window with buttons placed below.
    /// Just a few canned responses (yes, no, etc..).
    /// </summary>
    /// <param name="message"></param>
    /// <param name="responses"></param>
    [PunRPC]
    public void TextSignalFeedback(string message, bool yes, bool no)
    {
        Debug.Log("received text: " + message);
        toastObject.DisplayToast(message);
        yesButton.gameObject.SetActive(yes);
        noButton.gameObject.SetActive(no);
        AudioManager.instance.AlertText();
    }

    /// <summary>
    /// This is to check the network latency round-trip time.
    /// </summary>
    [PunRPC]
    public void PingBack()
    {
        sender.RPC("PingRec", PhotonTargets.AllViaServer);
    }
}
