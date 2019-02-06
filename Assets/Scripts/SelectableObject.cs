using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour {

    public float minimumSize = 0.5f;
    public float maximumSize = 2f;

    [HideInInspector]
    public Vector3 maximumVector;
    [HideInInspector]
    public Vector3 minimumVector;

    private void Start()
    {
        minimumVector = new Vector3(minimumSize, minimumSize, minimumSize);
        maximumVector = new Vector3(maximumSize, maximumSize, maximumSize);
    }

}
