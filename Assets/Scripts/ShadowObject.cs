using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowObject : MonoBehaviour {
    [SerializeField]
    public Quaternion solveRotation = Quaternion.identity;

    [SerializeField]
    [Range(0, 180)]
    private float solveThresholdAngle = 5;

    [SerializeField]
    [Range(0,1)]
    private float solveAnimationLerpFactor = 0.3f;

    private bool isSolved = false;

    void Start() {
        // Apply solveRotation to all children, then the inverse rotation to ourselves
        for (int i = 0; i < transform.childCount; i += 1) {
            var child = transform.GetChild(i);
            child.rotation = solveRotation;
        }

        transform.rotation = Quaternion.Inverse(solveRotation);
    }

    void Update() {
        if (isSolved) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, solveAnimationLerpFactor);
        } else if (Vector3.Angle(transform.right, Vector2.right) <= solveThresholdAngle) {
            isSolved = true;
            Debug.Log("Solved!");
        }
    }
}

