using UnityEngine;
using UnityEngine.UI;

public class SettingsOperator : MonoBehaviour
{
    [SerializeField] private GameSettingsLoader gameSettingsLoader;
    [SerializeField] private Toggle toggle;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider slider;

    bool suppressToggle;

    private void Start()
    {
        suppressToggle = true;
        fullScreenToggle.isOn = gameSettingsLoader.IsFullScreen;
        slider.value = gameSettingsLoader.Volume;
        suppressToggle = false;
    }

    public void ToggleFullScreen()
    {
        if (suppressToggle)
            return;
        gameSettingsLoader.IsFullScreen = !gameSettingsLoader.IsFullScreen;
    }

    public void ToggleMusic()
    {
        if (suppressToggle)
            return;
        if (toggle.isOn)
        {
            gameSettingsLoader.Volume = 1;
        }
        else
        {
            gameSettingsLoader.Volume = 0;
        }
    }

    public void OnSlider()
    {
        if (suppressToggle)
            return;
        gameSettingsLoader.Volume = slider.value;
        toggle.isOn = gameSettingsLoader.Volume > 0;
    }
}