using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour{
    [SerializeField]
    public int length;
    public string path;

    public override string ToString()
    {
        
        bool isNotEmpty = false;
        string json = "{\"moveLength\":" + length + "\"hitboxes\":[";
        foreach (Transform child in transform)
        {
            HBox hbox = child.GetComponent<HBox>();
            if (hbox) {
                isNotEmpty = true;
                json += hbox.ToString() + ", ";
            }
        }
        if (isNotEmpty)
        {
            json = json.Substring(0, json.Length - 2); //remove last semicolon and space
        }
        return json;
    }
}
