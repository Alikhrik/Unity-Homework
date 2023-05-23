using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static bool _isMuted;
    private const string SettingsFileName = "Settings.txt";
    public static bool IsMuted
    {
        get => _isMuted;
        set { _isMuted = value;
            SaveSettings();
        }
    }

    private static float _backgroundVolume;

    public static float BackgroundVolume
    {
        get => _backgroundVolume;
        set { _backgroundVolume = value;
            SaveSettings();
        }
    }
    public static void SaveSettings()
    {
        string path = Application.persistentDataPath + "/" + SettingsFileName;
        string data = $"{_isMuted}\n{_backgroundVolume}";
        File.WriteAllText(path, data);
    }
    public static void LoadSettings()
    {
        string path = Application.persistentDataPath + "/" + SettingsFileName;
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            _isMuted = (lines.Length > 0) && Convert.ToBoolean(lines[0]);
            _backgroundVolume = (lines.Length > 1) ? Convert.ToSingle(lines[1]) : 0;
        }
        
    }
}
