/* Joe Snider
 * 4/18
 * 
 * Control focus on a canvas. Just moves it in/out of the camera view.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    /// <summary>
    /// Put this object in front of the camera. 
    /// No guarantee it is on top (call RemoveFocus on the other objects in the scene).
    /// </summary>
    public void GiveFocus()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// Put this object where the camera can't see it. 
    /// </summary>
    public void RemoveFocus()
    {
        gameObject.transform.position = new Vector3(10000, 0, 0);
    }

    /// <summary>
    /// Check the focus.
    /// </summary>
    /// <returns>True if the camera is looking at this.</returns>
    public bool HasFocus()
    {
        return gameObject.transform.position.x > 5000;
    }

}
