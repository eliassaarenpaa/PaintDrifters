#region
using UnityEngine;
#endregion

public class Rotator : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private int direction;

    // Update is called once per frame
    private void Update() {

        var euler = transform.eulerAngles;
        euler.y += speed * direction * Time.deltaTime;
        transform.eulerAngles = euler;

    }

}