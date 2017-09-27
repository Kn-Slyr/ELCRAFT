using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
	
	void Update ()
	{
		
	}

	private void OnMouseDown()
	{
		if (!popupValid)
		{
			popupValid = true;
			CallLoginPopup();
		}
	}

	void CallLoginPopup()
	{
		popup.SetActive(true);
	}

	public void LoginProcess()
	{
		string id = inputId.text;
		string pswd = inputPswd.text;
		Debug.Log("( ID: " + id + ", PSWD: " + pswd + " ) LogIn!!");
	}

	public void ExitLoginPopup()
	{
		popup.SetActive(false);
		popupValid = false;
	}

	public void SignupPorcess()
	{
		Debug.Log("SignUp Process!!");
	}
}
