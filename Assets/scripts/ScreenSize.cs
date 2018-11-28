/// <summary>
/// Screen size.
/// Joe Snider, 5/18
/// 
/// Set the size of the screen for windows.
/// </summary>
/// 

using UnityEngine;

public class ScreenSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Application.platform == RuntimePlatform.WindowsPlayer) {
			Screen.SetResolution(450, 800, false);
		}
	}
	
}
