using UnityEngine;

// Manager class that handles solving of all shadow objects
// Shadow objects must be children of the manager object
public class ShadowObjectManager : MonoBehaviour {
    [SerializeField]
    [Range(0, 180)]
    private float solveThresholdAngle = 5;

    [SerializeField]
    [Range(0,1)]
    private float solveAnimationLerpFactor = 0.1f;

    private bool allSolved;

    void Update() {
        var objects = GetComponentsInChildren<ShadowObject>();

        if (allSolved) {
            foreach (var obj in objects) {
                obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, Quaternion.identity, solveAnimationLerpFactor);
            }
        } else {
            allSolved = true;

            foreach (var obj in objects) {
                if (Vector3.Angle(obj.transform.right, Vector2.right) > solveThresholdAngle) {
                    allSolved = false;
                }
            }
        }
    }
}
