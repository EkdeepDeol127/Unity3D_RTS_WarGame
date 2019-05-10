using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XmlManagerScript : MonoBehaviour
{
    public ObjectEntry tempHold;
    private string PathString;

    public void SaveData(float MusicVol, float SFXVol)
    {
        tempHold.MusicVolume = MusicVol;
        tempHold.SoundEffectsVolume = SFXVol;
        XmlSerializer serializer = new XmlSerializer(typeof(ObjectEntry));
        FileStream _stream = new FileStream("Objects_Data.xml", FileMode.Create);//overrides current file
        serializer.Serialize(_stream, tempHold);
        _stream.Close();
    }

    public bool LoadData()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(ObjectEntry));
        if (File.Exists("Objects_Data.xml"))
        {
            PathString = ("Objects_Data.xml");//opens file browser and returns a xml type file, the user opened/clicked
            FileStream _stream = new FileStream(PathString, FileMode.Open);
            tempHold = serializer.Deserialize(_stream) as ObjectEntry;
            _stream.Close();
            return true;
        }
        else
        {
            Debug.Log("No Saved Data...");
            return false;
        }
        return false;
    }
}

[System.Serializable]
public class ObjectEntry
{
    public float MusicVolume;
    public float SoundEffectsVolume;
}
