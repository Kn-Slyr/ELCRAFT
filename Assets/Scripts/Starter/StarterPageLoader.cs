using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarterPageLoader : MonoBehaviour
{
	public GameObject popup;
	public InputField inputId;
	public InputField inputPswd;
	
	private bool popupValid;

	void Start ()
	{
		popupValid = false;
	}

	private void OnMouseDown()
	{
		this.GetComponent<Collider2D>();
		if (!popupValid)
			CallLoginPopup();
		else
			ExitLoginPopup();
	}

	void CallLoginPopup()
	{
		popupValid = true;
		popup.SetActive(true);
	}

	public void LoginProcess()
	{
		string userID = inputId.text;
		string userPwd = inputPswd.text;
		Debug.Log("( ID: " + userID + ", PSWD: " + userPwd + " ) LogIn!!");

		// @@have to make session

		SceneManager.LoadScene("Lobby");
	}

	void ExitLoginPopup()
	{
		popupValid = false;
		popup.SetActive(false);
	}

	public void SignupPorcess()
	{
		Debug.Log("SignUp Process!!");
	}
}
