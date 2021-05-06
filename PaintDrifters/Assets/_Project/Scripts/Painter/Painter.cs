#region
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Painter : MonoBehaviour {

    [SerializeField] private LayerMask paintLayer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private MeshCollider paintMeshCollider;
    [SerializeField] private float minMoveDistToGenerateLine;
    
    [Header("test controls... delete later")]
    [SerializeField] private float speed; // TODO: delete speed variable
    
    private Vector3 _lastPos;
    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        // TODO: delete movement logic
        var vel = _rb.velocity;
        vel.x = Input.GetAxisRaw( "Horizontal" );
        vel.z = Input.GetAxisRaw( "Vertical" );
        _rb.velocity = vel * speed;
        
        // Check for rigidbody movement & that painter transform has moved a certain min distance
        if ( _rb.velocity.magnitude > 0.1f && Vector3.Distance( transform.position, _lastPos ) > minMoveDistToGenerateLine ) {
            var point = _lastPos + Vector3.up * 0.1f;
            AddPointToLineRenderer( point );        
            _lastPos = transform.position;
        }
    }

    private void AddPointToLineRenderer(Vector3 point) {
        var positionCount = lineRenderer.positionCount;
        positionCount++;
        lineRenderer.positionCount = positionCount;

        lineRenderer.SetPosition( positionCount - 1, point );
        
        var mesh = new Mesh();
        lineRenderer.BakeMesh( mesh, true );
        paintMeshCollider.sharedMesh = mesh;
    }
    
    /// <summary>
    /// Clear line renderer
    /// </summary>
    private void ClearPoints() {
        lineRenderer.positionCount = 0;
    }

    /// <summary>
    /// Check for intersecting line & make lätäkkö 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter( Collider other ) {
        if ( 1 << other.gameObject.layer != paintLayer ) return;

        var meshgen = FindObjectOfType<MeshGenerator>();
        meshgen.GenerateMesh( GetLinerendPositionsFrom( GetNearestLineRenderIndex() ) );

        ClearPoints();
    }

    /// <summary>
    /// Get every point on line renderer
    /// </summary>
    /// <returns></returns>
    private Vector3[] GetLinerendPositions() {
        var positionCount = lineRenderer.positionCount;
        var positions = new Vector3[positionCount];
        lineRenderer.GetPositions( positions );

        return positions;
    }

    // Get the index of line renderer point by closest to painter transform 
    private int GetNearestLineRenderIndex() {
        var positions = GetLinerendPositions();
        var distance = 999f;
        var nearest = 0;

        for ( var i = 0; i < positions.Length; i++ ) {
            var dist = Vector3.Distance( positions[i], transform.position );

            if ( dist < distance ) {
                distance = dist;
                nearest = i;
            }
        }

        return nearest;
    }

    // Get every point on line renderer starting from index
    private Vector3[] GetLinerendPositionsFrom( int index ) {
        var newPosList = new List<Vector3>();
        var positions = GetLinerendPositions();

        for ( var i = index; i < lineRenderer.positionCount; i++ ) newPosList.Add( positions[i] );

        return newPosList.ToArray();
    }

}