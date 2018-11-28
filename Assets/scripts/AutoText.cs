using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour {

    [Header("Case sensitive message format: <yes> <no> <message>.")]
    public List<string> messages;
    private int n = 0;

    [Header("Network object.")]
    public Sender sc;

    [Header("The key that sends the message (e.g. from python)")]
    public string pulseKey = "q";

    [Header("Show the next message in the queue")]
    public Text nextMessage;

    /// <summary>
    /// Have to use update since we are trapping a key.
    /// Pulls the yes/no state from the message, e.g. a message of 
    /// yes no I like pickles
    /// will put up "I like pickles" with both the yes and no buttons, or
    /// yes I like pickles
    /// will put up "I like pickles" with only the yes option.
    /// </summary>
	void Update () {
		if(Input.GetKeyDown(pulseKey))
        {
            bool yes = false;
            bool no = false;
            string y = messages[n];
            //Debug.Log("automessage gh0 " + y);
            if (y.StartsWith("yes"))
            {
                yes = true;
                y = y.Remove(0, 4);
            }
            //Debug.Log("automessage gh1 " + y);
            if (y.StartsWith("no"))
            {
                no = true;
                y = y.Remove(0, 3);
            }
            //Debug.Log("automessage gh2 " + y);
            sc.SendText(y, yes, no);
            n = (n + 1) % messages.Count;

            nextMessage.text = messages[n];
        }
	}
}
