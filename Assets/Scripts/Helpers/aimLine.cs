using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimLine : MonoBehaviour
{
    public LineRenderer myLineRenderer; 
    public Transform startHere;
    public Transform endHere;

    void Update()
    {
        if (!myLineRenderer) return;
        myLineRenderer.SetPosition(0, startHere.position);
        myLineRenderer.SetPosition(1, endHere.position);        
    }
}
