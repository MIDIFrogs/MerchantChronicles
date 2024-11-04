using UnityEngine;

public class GameSettingsLoader : MonoBehaviour
{
    private float volume;
    private bool isFullScreen;
    [SerializeField] private MusicManager manager;

    public float Volume
    {
        get => volume;
        set
        {
            volume = value;
            Debug.Log($"New volume: {value}");
            manager.SetMusic(volume);
            Save();
        }
    }

    public bool IsFullScreen
    {
        get => isFullScreen;
        set
        {
            isFullScreen = value;
            Screen.fullScreen = isFullScreen;
        }
    }

    private void Awake()
    {
        isFullScreen = Screen.fullScreen;
        LoadMusicSettings();
        SetupMusic();
    }

    private void SetupMusic()
    {
        manager.SetMusic(Volume);
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("volume", Volume);
    }

    private void LoadMusicSettings()
    {
        Volume = PlayerPrefs.GetFloat("volume", Volume);
    }
}