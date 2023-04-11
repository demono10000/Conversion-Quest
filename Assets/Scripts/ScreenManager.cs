using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private const float TargetAspect = 16f / 9f;
    private bool _isFullscreen = false;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        if (Mathf.Abs(currentAspect - TargetAspect) > 0.01)
        {
            float targetWidth = Screen.height * TargetAspect;
            Screen.SetResolution((int)targetWidth, Screen.height, false);
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            _isFullscreen = !_isFullscreen;

            if (_isFullscreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.SetResolution(1920, 1080, false);
            }
        }
    }
}