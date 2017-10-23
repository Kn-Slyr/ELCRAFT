using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMaker : MonoBehaviour {
	public List<ActionData> actionList;
	private DataManager dataManager;
	public InputField textBox;

	private void Awake()
	{
		dataManager = new DataManager();
		actionList = new List<ActionData>();
	}

	public void AddActionList(List<ActionData> datas)
	{
		actionList.AddRange(datas);
	}

	public void SaveDataAsBinary()
	{
		string fileName = textBox.text;
		dataManager.BinarySave<List<ActionData>>(actionList, fileName);
	}
}
