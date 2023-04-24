using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum Difficulty { EASY, NORMAL, HARD };

/// <summary>
/// Настройки игры, доступные для всех скриптов
/// </summary>
public class GameSettings : MonoBehaviour
{
    public class LeaderRecord
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public static LeaderRecord Parse(string text)
        {
            string[] parts = text.Split(';', 2);
            try
            {
                return new()
                {
                    Name = parts[0],
                    Score = int.Parse(parts[1])
                };
            }
            catch
            {
                throw new System.ArgumentException($"Invalid string: '{text}'");
            }
        }
    }

    #region Camera settings
    private static bool _inverceMouseVertical;
    public static bool InverceMouseVertical
    {
        get => _inverceMouseVertical;
        set
        {
            _inverceMouseVertical = value;
            SaveSettings();
        }
    }

    private static bool _inverseWheelZoom;
    public static bool InverseWheelZoom
    {
        get => _inverseWheelZoom;
        set
        {
            _inverseWheelZoom = value;
            SaveSettings();
        }
    }

    private static float _sencitivity;
    public static float Sencitivity
    {
        get => _sencitivity;
        set
        {
            _sencitivity = value;
            SaveSettings();
        }
    }
    #endregion
    #region Sound settings
    private static float _sounds;
    public static float Sounds
    {
        get => _sounds;
        set
        {
            _sounds = value;
            SaveSettings();
        }
    }

    private static float _music;
    public static float Music
    {
        get => _music;
        set
        {
            _music = value;
            SaveSettings();
        }
    }

    private static bool _muteAllSounds;
    public static bool MuteAllSounds
    {
        get => _muteAllSounds;
        set
        {
            _muteAllSounds = value;
            SaveSettings();
        }
    }
    #endregion
    #region Best board
    private static List<LeaderRecord> _leaderRecorders = new(); // save amount of catched coins
    public static List<LeaderRecord> LeaderRecords { get => _leaderRecorders; }
    #endregion
    #region Difficulty settings
    private static bool _displayGameTimer;
    public static bool DisplayGameTimer
    {
        get => _displayGameTimer;
        set
        {
            _displayGameTimer = value;
            SaveSettings();
        }
    }

    private static bool _displayCoinDistance;
    public static bool DisplayCoinDistance
    {
        get => _displayCoinDistance;
        set
        {
            _displayCoinDistance = value;
            SaveSettings();
        }
    }

    private static bool _displayDirectionHint;
    public static bool DisplayDirectionHint
    {
        get => _displayDirectionHint;
        set
        {
            _displayDirectionHint = value;
            SaveSettings();
        }
    }

    private static bool _displayDisaparTimer;
    public static bool DisplayDisaparTimer
    {
        get => _displayDisaparTimer;
        set
        {
            _displayDisaparTimer = value;
            SaveSettings();
        }
    }

    private static bool _displayStamina;
    public static bool DisplayStamina
    {
        get => _displayStamina;
        set
        {
            _displayStamina = value;
            SaveSettings();
        }
    }

    public static Difficulty Difficulty
    {
        get
        {
            int enabledDisplays = 0;
            if (DisplayGameTimer) ++enabledDisplays;
            if (DisplayCoinDistance) ++enabledDisplays;
            if (DisplayDirectionHint) ++enabledDisplays;
            if (DisplayDisaparTimer) ++enabledDisplays;
            if (DisplayStamina) ++enabledDisplays;

            if (enabledDisplays < 2) return Difficulty.HARD;
            if (enabledDisplays < 4) return Difficulty.NORMAL;

            return Difficulty.EASY;
        }
    }

    public static int CoinPrice
    {
        get
        {
            return Difficulty switch
            {
                Difficulty.HARD => 50,
                Difficulty.NORMAL => 25,
                _ => 10
            };
        }
    }
    #endregion

    public static void RestoreDefaults()
    {
        _inverseWheelZoom = false;
        _inverceMouseVertical = false;
        _sencitivity = 0.5f;
        _sounds = 0.5f;
        _music = 0.5f;
        _muteAllSounds = false;
        _displayGameTimer = true;
        _displayCoinDistance = true;
        _displayDirectionHint = true;
        _displayDisaparTimer = true;
        _displayStamina = true;

        SaveSettings();
    }

    private static readonly string fileSettings = "Assets/Files/settings.txt";

    private static void SaveSettings()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(_inverseWheelZoom).Append("\n");
        stringBuilder.Append(_inverceMouseVertical).Append("\n");
        stringBuilder.Append(_sencitivity).Append("\n");
        stringBuilder.Append(_displayGameTimer).Append("\n");
        stringBuilder.Append(_displayCoinDistance).Append("\n");
        stringBuilder.Append(_displayDirectionHint).Append("\n");
        stringBuilder.Append(_displayDisaparTimer).Append("\n");
        stringBuilder.Append(_displayStamina).Append("\n");
        stringBuilder.Append(_sounds).Append("\n");
        stringBuilder.Append(_music).Append("\n");
        stringBuilder.Append(_muteAllSounds).Append("\n");

        _leaderRecorders.Clear();
        foreach (var item in _leaderRecorders)
        {
            stringBuilder.Append(item.Name).Append(';')
                .Append(item.Score).Append("\n");
        }

        System.IO.File.WriteAllText(fileSettings, stringBuilder.ToString());
    }

    public static void LoadSettings()
    {
        try
        {
            string content = System.IO.File.ReadAllText(fileSettings);
            string[] lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            _inverseWheelZoom = Convert.ToBoolean(lines[0]);
            _inverceMouseVertical = Convert.ToBoolean(lines[1]);
            _sencitivity = Convert.ToSingle(lines[2]);
            _displayGameTimer = Convert.ToBoolean(lines[3]);
            _displayCoinDistance = Convert.ToBoolean(lines[4]);
            _displayDirectionHint = Convert.ToBoolean(lines[5]);
            _displayDisaparTimer = Convert.ToBoolean(lines[6]);
            _displayStamina = Convert.ToBoolean(lines[7]);
            _sounds = Convert.ToSingle(lines[8]);
            _music = Convert.ToSingle(lines[9]);
            _muteAllSounds = Convert.ToBoolean(lines[10]);

            _leaderRecorders = new();
            for (int i = 11; i < lines.Length; i++)
            {
                try
                {
                    _leaderRecorders.Add(LeaderRecord.Parse(lines[i]));
                }
                catch (ArgumentException ex)
                {
                    Debug.LogError(ex.Message);
                }
            }
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }
}