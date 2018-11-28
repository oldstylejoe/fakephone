///Joe Snider
///11/2018
///Trap a key and trigger the message call.
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCallMessage : MonoBehaviour
{
    [Header("Pulse key to send a message")]
    public string pulseKey = "w";

    [Header("The send controller")]
    public Sender sender;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pulseKey))
        {
            sender.PlayNextMessage();
        }
    }
}
