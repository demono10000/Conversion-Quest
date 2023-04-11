using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private const float TargetAspect = 16f / 9f; // stała proporcja 16:9

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void FixedUpdate()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        if (Mathf.Abs(currentAspect - TargetAspect) > 0.01)
        {
            float targetWidth = Screen.height * TargetAspect;
            Screen.SetResolution((int)targetWidth, Screen.height, false);
        }
    }
}