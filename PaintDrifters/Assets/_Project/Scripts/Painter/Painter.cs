#region
using System.Collections.Generic;
using UnityEngine;
#endregion

public class Painter : MonoBehaviour {

    [SerializeField] private LayerMask paintLayer;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Material paintMaterial;
    [SerializeField] private MeshCollider paintMeshCollider;
    [SerializeField] private float minMoveDistToGenerateLine;
    [SerializeField] private float paintYoffset;
    
    private Vector3 _lastPos;
    private Rigidbody _rb;
    
    public bool isActive;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        isActive = false;
    }

    private void Start() {
        ResetThisLineRenderer();
    }

    private void Update() {
        if ( !isActive ) {
            
            ResetThisLineRenderer();
            return;
        }

        // Check for rigidbody movement & that painter transform has moved a certain min distance
        if ( _rb.velocity.magnitude > 0.1f && Vector3.Distance( transform.position, _lastPos ) > minMoveDistToGenerateLine ) {
            AddPointToLineRenderer( GetTargetPaintPos(), lineRenderer, paintMeshCollider );
            _lastPos = transform.position;
        }
    }

    /// <summary>
    ///     Check for intersecting line & make lätäkkö
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter( Collider other ) {
        if ( !isActive ) return;
        if ( 1 << other.gameObject.layer != paintLayer ) return;

        // Touching other line renderer
        if ( other != paintMeshCollider ) {

            var otherLinerend = other.GetComponent<LineRenderer>();
            var nearestIndexToOther = GetNearestLineRenderIndex( otherLinerend );
            var newOtherLinePoints = GetLinerendPositionsFrom( nearestIndexToOther, otherLinerend );
            ConstructLinerend( otherLinerend, newOtherLinePoints );

            return;
        }

        // Touching own line renderer
        var meshgen = FindObjectOfType<MeshGenerator>();
        var nearestIndex = GetNearestLineRenderIndex( lineRenderer );
        var points = GetLinerendPositionsFrom( nearestIndex, lineRenderer );
        meshgen.GenerateMesh( points, paintMaterial );

        ResetThisLineRenderer();
    }

    private Vector3 GetTargetPaintPos() {
        var point = _lastPos;
        point.y = 0;
        point.y += paintYoffset;

        return point;
    }

    /// <summary>
    ///     Extend line renderer by a position
    /// </summary>
    /// <param name="point"></param>
    private void AddPointToLineRenderer( Vector3 point, LineRenderer renderer, MeshCollider collider ) {
        var positionCount = renderer.positionCount;
        positionCount++;
        renderer.positionCount = positionCount;

        renderer.SetPosition( positionCount - 1, point );

        var mesh = new Mesh();
        renderer.BakeMesh( mesh, true );
        collider.sharedMesh = mesh;
    }

    /// <summary>
    ///     Clear the line renderer & mesh collider
    ///     Set the last position as current
    /// </summary>
    public void ResetThisLineRenderer( ) {
        lineRenderer.positionCount = 0;
        paintMeshCollider.sharedMesh = null;
        _lastPos = transform.position;
    }

    /// <summary>
    ///     Get every point on line renderer
    /// </summary>
    /// <returns></returns>
    private Vector3[] GetLinerendPositions( LineRenderer renderer ) {
        var positionCount = renderer.positionCount;
        var positions = new Vector3[positionCount];
        renderer.GetPositions( positions );

        return positions;
    }

    /// <summary>
    ///     Get the index of line renderer point by closest to painter transform
    /// </summary>
    /// <returns></returns>
    private int GetNearestLineRenderIndex( LineRenderer renderer ) {
        var positions = GetLinerendPositions( renderer );
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

    /// <summary>
    ///     Get every point on line renderer starting from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private Vector3[] GetLinerendPositionsFrom( int index, LineRenderer renderer ) {
        var newPosList = new List<Vector3>();
        var positions = GetLinerendPositions( renderer );

        for ( var i = index; i < renderer.positionCount; i++ )
            newPosList.Add( positions[i] );

        return newPosList.ToArray();
    }

    /// <summary>
    ///     Set the points of a line renderer
    /// </summary>
    /// <param name="renderer"></param>
    /// <param name="newPoints"></param>
    private void ConstructLinerend( LineRenderer renderer, Vector3[] newPoints ) {
        renderer.positionCount = newPoints.Length;

        for ( var i = 0; i < renderer.positionCount; i++ ) renderer.SetPosition( i, newPoints[i] );
    }

}