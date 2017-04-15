﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public static class GameDirector
{
    public static GD_SceneManager SceneManager = new GD_SceneManager();
    public static GD_InputManager InputManager = new GD_InputManager();
    public static GD_LevelManager LevelManager = new GD_LevelManager();
    public static DataManager dataManager = new DataManager();
}

public class GD_SceneManager
{
    public string CurrentSceneName
    {
        get
        {
            return SceneManager.GetActiveScene().name;
        }
    }

    //Created to extract the scene name of a currently loaded level when it is not the only scene current loaded
    public string CurrenLevelSceneName
    {
        get
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).name.Contains("Level_"))
                {
                    return SceneManager.GetSceneAt(i).name;
                }
            }

            return "No Level Loaded";
        }
    }

    public bool CheckIfSceneActive(string _SceneName)
    {
        Scene checkScene = SceneManager.GetSceneByName(_SceneName);
        return checkScene.isLoaded;
    }

    public enum PrimarySceneList
    {
        Splash,
        MainMenu,
        LevelSelect
    }

    public void ChangeScene(PrimarySceneList _Scene)
    {
        SceneManager.LoadScene((int)_Scene, LoadSceneMode.Single);
    }

    public void ChangeScene(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName, LoadSceneMode.Single);
    }

    public void LoadScene(string _SceneName)
    {
        SceneManager.LoadScene(_SceneName, LoadSceneMode.Additive);
    }

    public void LoadScene(int _SceneBuildID)
    {
        SceneManager.LoadScene(_SceneBuildID, LoadSceneMode.Additive);
    }

    public void UnloadScene(string _SceneName)
    {
        SceneManager.UnloadSceneAsync(_SceneName);
    }

    public void UnloadScene(int _SceneBuildID)
    {
        SceneManager.UnloadSceneAsync(_SceneBuildID);
    }
}

public class GD_InputManager
{
    public bool DebugKeyDown
    {
        get
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public bool LeftClickDown
    {
        get
        {
            return Input.GetMouseButtonDown(0);
        }
    }
}

public class GD_LevelManager
{
    public int CurrentLevelID;

    public bool LevelListPopulated = false;

    public LevelController CurrentLevel;

    public LevelUIController levelUIController;

    //List of persistant level data that is used by the level selector and updated by indervidual level controllers
    List<LevelData> LevelDataList = new List<LevelData>();

    public int GetLevelIDFromScene(string _SceneName)
    {
        return Int32.Parse(System.Text.RegularExpressions.Regex.Match(_SceneName, @"\d+$").Value);
    }

    //Scans scenes and populates a list of level data based on level scenes
    public void PopulateLevelList()
    {
        if(LevelListPopulated == false)
        {
            //TODO remove: Debug code that always loads from the scriptable object, effectily disabling persistant data. Need to instante this with an instance or it will overwirte the data in the scriptable object
            LevelDataList = GameObject.Instantiate<LevelDataCollection>(Resources.Load<LevelDataCollection>("ScriptableObjects/Level Collection")).LevelList;
            LevelListPopulated = true;
        }

        #region Proper load and saving code
        //if (GameDirector.dataManager.SaveDataFound)
        //{
        //    LoadLevelData();
        //}
        //else
        //{
        //    LevelDataList = Resources.Load<LevelDataCollection>("ScriptableObjects/Level Collection").LevelList;
        //    SaveLevelData();
        //}
        #endregion
    }

    //Returns the level data from the level data list that corrispondes to the level id
    public LevelData GetLevelData(int _LevelID)
    {
        foreach(LevelData levelData in LevelDataList)
        {
            if(levelData.LevelID == _LevelID)
            {
                return levelData;
            }
        }

        return null;
    }

    public void LoadLevel(int _LevelID)
    {
        //Hard coded prefix that corispondes with scene naming convention
        GameDirector.SceneManager.ChangeScene("Level_" + _LevelID);
    }

    public void LoadLevelAdditive(int _LevelID)
    {
        //Hard coded prefix that corispondes with scene naming convention
        GameDirector.SceneManager.LoadScene("Level_" + _LevelID);
    }

    public void UnloadLevel(int _LevelID)
    {
        //Hard coded prefix that corispondes with scene naming convention
        GameDirector.SceneManager.UnloadScene("Level_" + _LevelID);
    }

    public void SaveLevelData()
    {
        GameDirector.dataManager.SaveLevelData(LevelDataList);
    }

    public void LoadLevelData()
    {
        LevelDataList = GameDirector.dataManager.LoadLevelData();
    }
}

