using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class animateInEditor : MonoBehaviour {

    // Use this for initialization
    public Animator a;
    private void OnEnable()
    {
        Debug.Log("Started");
        
        a = transform.GetComponent<Animator>();
            a.StartPlayback();
    }
	
	// Update is called once per frame
	public void SetAnimPercent (float moment) {
        a.Play("uppercut", 0,moment);
        a.Update(0);
        print(moment);
    }
}
