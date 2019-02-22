using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HBox : MonoBehaviour {
    public int beginFrame = 0;
    public int duration = 0;
    public float damage = 0;
    public float hitStun = 0;
    public Vector3 launchVector = new Vector3();

    // Use this for initialization
    void Start () {
		
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
