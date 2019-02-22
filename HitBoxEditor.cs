using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft;
using Newtonsoft.Json.Converters;
using System;


public class Rootobject
{
    public Hitbox[] hitboxes { get; set; }
}

public class Hitbox
{
    public string name { get; set; }
    public int beginFrame { get; set; }
    public int duration { get; set; }
    public float damage { get; set; }
    public float hitStun { get; set; }
    public float[] launchVector { get; set; }
    public float[] rotation { get; set; }
    public float[] scale { get; set; }
    public float[] offset { get; set; }
}


public class HitBoxEditor : EditorWindow
{
    string nameField = "HBox";
    string pathField = @"C:\";
    int beginFrameField = 0;
    int durationField = 0;
    float damageField = 0;
    float hitStunField = 0;
    Vector3 launchVectorField = new Vector3();
    Vector3 scaleField = new Vector3(1, 1, 1);
    Vector3 offsetField = new Vector3();
    Vector3 rotationField = new Vector3();



    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/HitBoxEditor")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(HitBoxEditor));
    }

    void OnGUI()
    {
        GUILayout.Label("HBox", EditorStyles.boldLabel);
        nameField = EditorGUILayout.TextField("Name Field", nameField);
        launchVectorField = EditorGUILayout.Vector3Field("LaunchVector", launchVectorField);
        beginFrameField = EditorGUILayout.IntField("Begin Frame", beginFrameField);
        durationField = EditorGUILayout.IntField("Duration", durationField);
        damageField = EditorGUILayout.FloatField("Damage", damageField);
        hitStunField = EditorGUILayout.FloatField("HitStun", hitStunField);
        scaleField = EditorGUILayout.Vector3Field("scaleVector", scaleField);
        offsetField = EditorGUILayout.Vector3Field("offsetVector", offsetField);
        rotationField = EditorGUILayout.Vector3Field("rotationVector", rotationField);
        if (GUILayout.Button("Creat HBox"))
        {
            CreateHitbox(nameField, launchVectorField, beginFrameField, durationField, damageField, hitStunField, scaleField, offsetField, rotationField);
            Debug.Log("hitbox created");
        }
        pathField = EditorGUILayout.TextField("Path Field", pathField);
        if (GUILayout.Button("Export JSON"))
        {
            var hboxArray = FindObjectsOfType<HBox>();
            string json = "{\"hitboxes\":[";
            foreach (var hbox in hboxArray)
            {
                json += hbox.ToString() + ", ";
            }
            if (hboxArray.Length > 0)
            {
                json = json.Substring(0, json.Length - 2);
            }
            json += "]}";
            File.WriteAllText(pathField, json);
        }
        if (GUILayout.Button("Import JSON"))
        {
            string json = File.ReadAllText(pathField);
            Rootobject root = JsonConvert.DeserializeObject<Rootobject>(json);
            foreach(var hbox in root.hitboxes)
            {
                CreateHitbox(hbox.name,new Vector3(hbox.launchVector[0], hbox.launchVector[1], hbox.launchVector[2]),hbox.beginFrame,hbox.duration,hbox.damage, hbox.hitStun, new Vector3(hbox.scale[0], hbox.scale[1], hbox.scale[2]), new Vector3(hbox.offset[0], hbox.offset[1], hbox.offset[2]), new Vector3(hbox.rotation[0], hbox.rotation[1], hbox.rotation[2]));
            }

        }
    }

    void CreateHitbox(String name, Vector3 launchVector, int beginFrame, int duration, float damage, float hitstun, Vector3 scale, Vector3 offset, Vector3 rotation)
    {
        GameObject instance = Instantiate(Resources.Load("HBox", typeof(GameObject))) as GameObject;
        instance.name = name;
        instance.GetComponent<HBox>().launchVector = launchVector;
        instance.GetComponent<HBox>().beginFrame = beginFrame;
        instance.GetComponent<HBox>().duration = duration;
        instance.GetComponent<HBox>().damage = damage;
        instance.GetComponent<HBox>().hitStun = hitstun;
        instance.transform.localScale = scale;
        instance.transform.position = offset;
        instance.transform.eulerAngles = rotation;
    }
}