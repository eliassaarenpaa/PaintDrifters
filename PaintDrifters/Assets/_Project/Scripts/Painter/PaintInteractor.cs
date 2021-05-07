#region
using UnityEngine;
#endregion

[RequireComponent( typeof( PainterUI ) )]
[RequireComponent( typeof( Painter ) )]
public class PaintInteractor : MonoBehaviour {

    [SerializeField] [Range( 0, 1 )] private float depleateSpeed;
    [SerializeField] private LayerMask paintReplenishLayer;
    [SerializeField] private Material paintMaterial;
    
    private PainterUI _painterUI;
    private Painter _painter;
    private float _paintPercentage;

    private void Awake() {
        _painter = GetComponent<Painter>();
        _painterUI = GetComponent<PainterUI>();
    }

    private void Start() {
        _paintPercentage = 0;
    }

    private void Update() {
        var hasPaint = _paintPercentage > 0;

        if ( hasPaint ) _paintPercentage -= Time.deltaTime * depleateSpeed;

        _painter.isActive = hasPaint;
        
        _painterUI.UpdatePercentageUI( _paintPercentage );
    }

    private void OnTriggerEnter( Collider other ) {
        if ( 1 << other.gameObject.layer != paintReplenishLayer ) return;

        // Check if has entered paint replenish area with same material
        if ( other.gameObject.GetComponent<MeshRenderer>().sharedMaterial == paintMaterial )
            ReplenishPaint();
    }

    public void ReplenishPaint() {
        _paintPercentage = 1;
    }

}