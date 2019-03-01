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
    private Vector3 launchVectorField;
    private int beginFrameField;
    private int durationField;
    private float damageField;
    private float hitStunField;
    private Vector3 scaleField = new Vector3(1,1,1);
    private Vector3 offsetField;
    private Vector3 rotationField;
    string pathField = @"C:\";
    [SerializeField]
    Transform charecterField;
    int time = 0;
    int maxTimeField = 1;
    bool playMode = false;

    public void Update()
    {
        if (playMode) { 
            time++;
            if (time > maxTimeField)
            {
                time = 0;
            }
            UpdateHitboxesToTime();
        }
    }

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
            var hboxArray = Resources.FindObjectsOfTypeAll(typeof(HBox));
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
            Debug.Log("hitbox exported");
        }
        if (GUILayout.Button("Import JSON"))
        {
            string json = File.ReadAllText(pathField);
            Rootobject root = JsonConvert.DeserializeObject<Rootobject>(json);
            foreach(var hbox in root.hitboxes)
            {
                CreateHitbox(hbox.name,
                    new Vector3( hbox.launchVector[0], hbox.launchVector[1], hbox.launchVector[2]),
                    hbox.beginFrame,
                    hbox.duration,
                    hbox.damage, 
                    hbox.hitStun, 
                    new Vector3(hbox.scale[0], hbox.scale[1], hbox.scale[2]), 
                    new Vector3(hbox.offset[0], hbox.offset[1], hbox.offset[2]), 
                    new Vector3(hbox.rotation[0], hbox.rotation[1], hbox.rotation[2]));
            }
            Debug.Log("hitbox imported");

        }
        maxTimeField = EditorGUILayout.IntField(maxTimeField);
        var tempTIme = time;
        time = EditorGUILayout.IntSlider(time,0,maxTimeField);
        if (tempTIme != time)
        {
            UpdateHitboxesToTime();
        }
        playMode = EditorGUILayout.Toggle(playMode);
        charecterField = (Transform) EditorGUILayout.ObjectField(charecterField, typeof(Transform), true);
        
    }
    void UpdateHitboxesToTime()
    {
        var hboxArray = Resources.FindObjectsOfTypeAll(typeof(HBox));
        foreach (HBox hbox in hboxArray)
        {
            if (hbox.GetComponent<HBox>().beginFrame <= time && time - hbox.GetComponent<HBox>().beginFrame <= hbox.GetComponent<HBox>().duration)
            {
                Debug.Log("enabled");
                hbox.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("disabled");
                hbox.gameObject.SetActive(false);
            }
        }
        if (charecterField != null) {
            charecterField.GetComponent<animateInEditor>().SetAnimPercent((float)time/ (float)maxTimeField);
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
    /*int getMaxTime()
    {
        var hboxArray = FindObjectsOfType<HBox>();
        int maxTime = 0;
        foreach (var hbox in hboxArray)
        {
            maxTime = (hbox.beginFrame + hbox.duration > maxTime) ? (hbox.beginFrame + hbox.duration) : (maxTime);
        }
        return maxTime;
    }*/

}