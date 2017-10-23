using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager
{
	public void BinarySave<T>(T target, string filePath)
	{
		Debug.Log("File Access to " + filePath);

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(Application.dataPath + "/Datas/" + filePath, FileMode.Create);

		formatter.Serialize(stream, target);
		stream.Close();
	}

	public T BinaryLoad<T>(string filePath)
	{
		Debug.Log("File Access to " + filePath);

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(Application.dataPath + "/Datas/" + filePath, FileMode.Open);
		T target = (T)formatter.Deserialize(stream);
		stream.Close();

		return target;
	}
}
