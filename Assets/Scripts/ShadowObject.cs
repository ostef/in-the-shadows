using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowObject : MonoBehaviour {
    [SerializeField]
    public Quaternion solveRotation = Quaternion.identity;

    void Start() {
        // Apply solveRotation to all children, then the inverse rotation to ourselves
        for (int i = 0; i < transform.childCount; i += 1) {
            var child = transform.GetChild(i);
            child.rotation = solveRotation;
        }

        transform.rotation = Quaternion.Inverse(solveRotation);
    }
}

