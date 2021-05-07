using UnityEngine;

public class MoveBoundaries : MonoBehaviour
{

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    private Painter _painter;

    private void Awake()
    {
        _painter = GetComponent<Painter>();
    }

    private void LateUpdate()
    {
        if (transform.position.z < minZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
            _painter.ResetThisLineRenderer();
        }else if (transform.position.z > maxZ)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
            _painter.ResetThisLineRenderer();
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            _painter.ResetThisLineRenderer();
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
            _painter.ResetThisLineRenderer();
        }
    }

}
