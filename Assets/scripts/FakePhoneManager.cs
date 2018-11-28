/* Joe Snider
 * 4/18
 * 
 * Manager for a simple caller/receiver that can also mark events temporally.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the various UI screens for the fake phone. Everything is in 1 scene.
/// </summary>
public class FakePhoneManager : Singleton<FakePhoneManager> {

    [Header("The 3 possible panels")]
    public GameObject startupGroup;
    public GameObject receiverGroup;
    public GameObject senderGroup;

	void Start () {
        startupGroup.GetComponent<FocusCanvas>().GiveFocus();
        receiverGroup.GetComponent<FocusCanvas>().RemoveFocus();
        senderGroup.GetComponent<FocusCanvas>().RemoveFocus();
    }

    /// <summary>
    /// Allow this phone to make calls and texts.
    /// This is the 'experimenter' view on the main phone (or computer or whatever).
    /// </summary>
    public void BeCaller()
    {
        startupGroup.GetComponent<FocusCanvas>().RemoveFocus();
        receiverGroup.GetComponent<FocusCanvas>().RemoveFocus();
        senderGroup.GetComponent<FocusCanvas>().GiveFocus();
    }

    /// <summary>
    /// Make the phone into a receiver.
    /// All this is allowed to do is receive texts and calls.
    /// This is the participant's view (on a phone, probably, so watch the UI).
    /// </summary>
    public void BeReceiver()
    {
        //receiver always transmits so we can hear what is going on.
        FindObjectOfType<ManageVoice>().StartTransmitting();
        startupGroup.GetComponent<FocusCanvas>().RemoveFocus();
        receiverGroup.GetComponent<FocusCanvas>().GiveFocus();
        senderGroup.GetComponent<FocusCanvas>().RemoveFocus();
    }

}
