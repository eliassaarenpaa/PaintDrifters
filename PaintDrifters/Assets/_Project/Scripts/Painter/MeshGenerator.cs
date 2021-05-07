#region
using System.Collections.Generic;
using UnityEngine;
#endregion

public class MeshGenerator : MonoBehaviour {

    private readonly List<GameObject> _paintStack = new List<GameObject>();

    public void GenerateMesh( Vector3[] points, Material mat ) {

        // Create a new instance & add components
        var instance = new GameObject( "Lätäkkö" );
        var meshFilter = instance.AddComponent<MeshFilter>();
        var meshRend = instance.AddComponent<MeshRenderer>();

        // New mesh
        var mesh = new Mesh();
        var verticies = points;
        var triangles = new List<int>();

        // Loop trough vertices / points except the last 3...
        var verticiesCount = verticies.Length - 3;

        for ( var i = 0; i < verticiesCount; i++ ) {
            // first triangle ( 0: from the other end of the array, 1: second point, 2: first point) so that it goes from left to right correctly... 
            triangles.Add( verticiesCount - i );
            triangles.Add( i + 1 );
            triangles.Add( i );

            // same but other way around so that it fills the other half the same way
            triangles.Add( i );
            triangles.Add( i + 1 );
            triangles.Add( verticiesCount - i );
        }

        // Supply mesh
        mesh.vertices = verticies;
        mesh.triangles = triangles.ToArray();

        // Setup mesh
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();

        // Assign variables
        meshFilter.mesh = mesh;
        meshRend.material = mat;

        _paintStack.Add( instance );

        UpdatePaintStack();
    }

    private void UpdatePaintStack() {

        for ( var i = 0; i < _paintStack.Count; i++ ) {

            var instance = _paintStack[i];

            var pos = instance.transform.position;
            pos.y = i * 0.001f;
            instance.transform.position = pos;

        }

    }

}