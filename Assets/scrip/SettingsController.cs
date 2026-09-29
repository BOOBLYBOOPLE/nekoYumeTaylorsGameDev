using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }
    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, FullScreenMode.ExclusiveFullScreen);
    }
}
