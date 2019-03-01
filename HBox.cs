using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class HBox : MonoBehaviour {
    [SerializeField]
    public int beginFrame = 0;
    [SerializeField]
    public int duration = 0;
    [SerializeField]
    public float damage = 0;
    [SerializeField]
    public float hitStun = 0;
    public Vector3 launchVector = new Vector3();
    [SerializeField]
    public Vector3 modeChangePosition;
    [SerializeField]
    public Vector3 modeChangeRotation;
    [SerializeField]
    public Vector3 modeChangeScale;
    // Use this for initialization
    void Start () {
		
	}

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + launchVector);
    }

    // Update is called once per frame
    void Update () {
	}
    public override string ToString() 
    {
        return "{\"name\": \"" + transform.name + "\",\"beginFrame\": " + beginFrame + ",\"duration\": " + duration + ",\"damage\": " + damage + ",\"hitStun\": " + hitStun + ",\"launchVector\": [" + 
            launchVector.x + ", " + launchVector.y + ", " + launchVector.z + "]" + ",\"rotation\": [" +
            transform.rotation.eulerAngles.x + ", " + transform.rotation.eulerAngles.y + ", " + transform.rotation.eulerAngles.z + "]" + ",\"scale\": [" +
            transform.localScale.x + ", " + transform.localScale.y + ", " + transform.localScale.z + "]" + ",\"offset\": [" +
            transform.position.x + ", " + transform.position.y + ", " + transform.position.z + "]}";
    }
}
