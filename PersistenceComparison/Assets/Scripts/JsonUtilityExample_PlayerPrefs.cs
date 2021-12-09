using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JsonUtilityExample_PlayerPrefs : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/JsonUtility.html
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/Serializable.html

    [SerializeField] private int hitCount = 0;

    private readonly string hitCountKey = "HitCountKeyJson";

    private void Start()
    {
        // Check if the key exists. If not, we never saved to it.
        if (PlayerPrefs.HasKey(hitCountKey))
        {
            string jsonString = PlayerPrefs.GetString(hitCountKey);
            HitCountWrapper hitCountEntity = JsonUtility.FromJson<HitCountWrapper>(jsonString);
            if (hitCountEntity != null)
            {
                hitCount = hitCountEntity.value;
            }
        }
    }

    private void OnApplicationQuit()
    {
        HitCountWrapper hitCountEntity = new();
        hitCountEntity.value = hitCount;
        string jsonString = JsonUtility.ToJson(hitCountEntity);
        PlayerPrefs.SetString(hitCountKey, jsonString);
    }

    private void OnMouseDown()
    {
        hitCount++;
    }
}
