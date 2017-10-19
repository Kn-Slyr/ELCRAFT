using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyPageLoader : MonoBehaviour {

    public GameObject popup;

    private bool popupValid;

	// Use this for initialization
	void Start () {
        popupValid = false;
	}

    public void CallOptionPopup()
    {
        popupValid = true;
        popup.SetActive(true);
    }

    public void ExitOptionPopup()
    {
        popupValid = false;
        popup.SetActive(false);
    }

    public void MovingScene(string Scenename)
    {
        SceneManager.LoadScene(Scenename);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
