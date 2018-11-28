using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToastManager : MonoBehaviour {

    [Header("Text object that gets the message")]
    public Text msg;

    [Header("Keep this many rows of messages.")]
    public int messageHistory = 3;

    [Header("Toast holder. Self-cleanup is required.")]
    public GameObject toastPanel;

    private void Start()
    {
        msg.text = "";
        toastPanel.SetActive(false);
    }

    /// <summary>
    /// Put up the toast. Destruction is handled elsewhere.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public void DisplayToast(string message)
    {
        toastPanel.SetActive(false);
        toastPanel.SetActive(true);
        StartCoroutine( UpdateMessageText(message) );
        //force the enable event
    }

    /// <summary>
    /// Put a yes response on the message.
    /// Has a bunch of spaces to look right justified.
    /// </summary>
    public void InsertYes()
    {
        msg.text += "\n                                  yes";
    }
    public void InsertNo()
    {
        msg.text += "\n                                   no";
    }

    /// <summary>
    /// Format the text in the history and assign it to the display.
    /// TODO: could animate this with some roll (add characters 1 at a time with a delay maybe?).
    /// </summary>
    /// <param name="message">The new message</param>
    private IEnumerator UpdateMessageText(string message)
    {
        string str = msg.text;
        string[] str2 = str.Split('\n');
        str = "";
        int i = str2.Length - messageHistory;
        i = (i < 0) ? 0 : i;
        for(; i < str2.Length; ++i)
        {
            str += str2[i] + "\n";
        }
        msg.text = str;

        for (int j = 0; j < message.Length; ++j)
        {
            msg.text += message[j];
            yield return null;
        }
        yield return null;
    }

}
