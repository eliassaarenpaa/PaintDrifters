#region
using UnityEngine;
#endregion

[RequireComponent( typeof( Rigidbody ) )]
public class CarController : MonoBehaviour {

    [Header( "Base Settings" )]
    [SerializeField] private float defaultMoveSpeed = 15;
    [SerializeField] private float defaultRotateSpeed = 10;
    [SerializeField] private float driftRotateSpeed = 20;
    [SerializeField] private float rotateSmooth = 8;
    
    private IPlayerInputs _playerInputs;
    private Rigidbody _rb;

    private void Awake() {
        _playerInputs = GetComponent<IPlayerInputs>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        _playerInputs.OnAbilityUsedAction += OnAbilityUsed;
    }

    private void OnDisable() {
        _playerInputs.OnAbilityUsedAction -= OnAbilityUsed;
    }

    private void OnAbilityUsed() { }

    private void FixedUpdate() {
        _rb.velocity = transform.forward.normalized * defaultMoveSpeed;

        var rotation = transform.eulerAngles;

        var rotateSpeed = _playerInputs.IsDrifting ? driftRotateSpeed : defaultRotateSpeed;

        var targetYRotation = rotation.y + rotateSpeed * _playerInputs.InputDirection;

        rotation.y = Mathf.Lerp( rotation.y, targetYRotation, Time.fixedDeltaTime * rotateSmooth );
        
        transform.eulerAngles = rotation;
    }

}