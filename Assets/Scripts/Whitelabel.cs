using System;
using TMPro;
using UnityEngine;

public class Whitelabel : MonoBehaviour, IGrabbable
{
    public String text = "my text";
    void OnValidate()
    {
        foreach (var comp in GetComponentsInChildren<TextMeshPro>())
        {
            comp.text = text;
        }
    }

    public Transform Grab()
    {
        return transform;
    } 
}
