﻿using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[Serializable]
public class DataManager
{
    public DataToSave dataToSave = new DataToSave();

    public void SaveData()
    {
        dataToSave.MYFLOAT = 99;
        dataToSave.MYNIT = 73;

        //Create the serializer
        var serializer = new XmlSerializer(typeof(DataToSave));
        //Create the stream
        var stream = new FileStream(Application.dataPath + "//SaveData.xml", FileMode.Create);
        //Saves the data
        serializer.Serialize(stream, dataToSave);
        //Clkeses the stream
        stream.Close();

        Debug.Log(Application.dataPath);
    }

    public void LoadData()
    {

    }
}