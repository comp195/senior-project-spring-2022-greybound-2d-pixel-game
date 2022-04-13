using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer (UserController2D player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        } 
    }

    public static void SaveEnemy(EnemyAI enemy)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/enemy.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(enemy);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadEnemy()
    {
        string path = Application.persistentDataPath + "/enemy.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        
        else 
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        } 
    }



}
