using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsExample : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

    [SerializeField] private int hitCount = 0;

    private readonly string hitCountKey = "HitCountKey";

    private void Start()
    {
        // Check if the key exists. If not, we never saved the hit count before.
        if (PlayerPrefs.HasKey(hitCountKey))
        {
            // Read the hit count from the PlayerPrefs.
            hitCount = PlayerPrefs.GetInt(hitCountKey);
        }
    }

    private void OnApplicationQuit()
    {
        // Set and save the hit count before ending the game.
        PlayerPrefs.SetInt(hitCountKey, hitCount);
        PlayerPrefs.Save();
    }
    private void OnMouseDown()
    {
        hitCount++;
    }

}
