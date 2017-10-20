using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager
{
	string basicPath = Application.dataPath + "/Datas/";

	public void BinarySave<T>(T target, string filePath)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(basicPath + filePath, FileMode.Create);

		formatter.Serialize(stream, target);
		stream.Close();
	}

	public T BinaryLoad<T>(string filePath)
	{
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(filePath, FileMode.Open);
		T target = (T)formatter.Deserialize(stream);
		stream.Close();

		return target;
	}
}
