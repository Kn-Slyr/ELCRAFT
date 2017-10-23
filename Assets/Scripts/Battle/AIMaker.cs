using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIMaker : MonoBehaviour {
	public List<ActionData> actionList = new List<ActionData>();
	private DataManager dataManager = new DataManager();
	public InputField textBox;

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
