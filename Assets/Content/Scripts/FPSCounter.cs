using UnityEngine;

/// <summary>
/// FPS counter: shows current, minimum, maximum, and average FPS without decimals.
/// Attach this script to any GameObject in your scene.
/// </summary>
public class FPSCounter : MonoBehaviour
{
    [Tooltip("Time interval over which FPS is averaged")]
    public float updateInterval = 0.5f;

    private float accumulatedTime = 0f;
    private int frames = 0;
    private float timeLeft;
    private float fps = 0f;

    private float maxFps = float.MinValue;
    private float minFps = float.MaxValue;
    private float sumFps = 0f;
    private int sampleCount = 0;

    void Start()
    {
        timeLeft = updateInterval;
    }

    void Update()
    {
        timeLeft -= Time.unscaledDeltaTime;
        accumulatedTime += Time.unscaledDeltaTime;
        frames++;

        if (timeLeft <= 0f)
        {
            fps = frames / accumulatedTime;

            // Update min, max, sum
            if (fps > maxFps) maxFps = fps;
            if (fps < minFps) minFps = fps;
            sumFps += fps;
            sampleCount++;

            // Reset counters
            timeLeft = updateInterval;
            accumulatedTime = 0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        int current = Mathf.RoundToInt(fps);
        // Show current as min before first sample to avoid zero
        int min = sampleCount > 5 ? Mathf.RoundToInt(minFps) : current;
        int max = sampleCount > 5 ? Mathf.RoundToInt(maxFps) : current;
        int avg = sampleCount > 5 ? Mathf.RoundToInt(sumFps / sampleCount) : current;

        string text = string.Format(
            "FPS: {0}\nMin: {1}\nMax: {2}\nAvg: {3}",
            current, min, max, avg
        );

        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 200, 100), text, style);
    }
}