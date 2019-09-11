/*
 Author: Aaron Hines
 Edits By: 
 Description:
 */
using UnityEngine;

public class DebugComponent : MonoBehaviour
{
    public void Log(string message)
    {
        Debug.Log(message);
    }

    public void Error(string message)
    {
        Debug.LogError(message);
    }

    public void Warning(string message)
    {
        Debug.LogWarning(message);
    }
}
