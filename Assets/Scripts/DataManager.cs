using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager
{
	public void BinarySave<T>(T target, string filePath)
	{
		if (filePath.Equals("")) filePath = "noname";
		Debug.Log("File Access to " + filePath);

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(Application.dataPath + "/Datas/Test/" + filePath, FileMode.Create);

		formatter.Serialize(stream, target);
		stream.Close();
	}

	public void BinaryLoad<T>(string filePath, out T target)
	{
		if (filePath.Equals("")) filePath = "noname";
		Debug.Log("File Access to " + filePath);

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(Application.dataPath + "/Datas/Test/" + filePath, FileMode.Open);
		target = (T)formatter.Deserialize(stream);
		stream.Close();
	}
}
