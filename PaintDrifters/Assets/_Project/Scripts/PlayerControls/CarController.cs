#region
using System.Collections;
using UnityEngine;
#endregion

[RequireComponent( typeof( Rigidbody ) )]
public class CarController : MonoBehaviour {

    [Header( "Base Settings" )]
    [SerializeField] private float defaultMoveSpeed = 15;
    [SerializeField] private float currentMoveSpeed = 0;
    [SerializeField] private float defaultRotateSpeed = 10;
    [SerializeField] private float driftRotateSpeed = 20;
    [SerializeField] private float rotateSmooth = 8;
    
    private IPlayerInputs _playerInputs;
    private Rigidbody _rb;

    private void Awake() {
        _playerInputs = GetComponent<IPlayerInputs>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentMoveSpeed = defaultMoveSpeed;
    }

    private void OnEnable() {
        _playerInputs.OnAbilityUsedAction += OnAbilityUsed;
    }

    private void OnDisable() {
        _playerInputs.OnAbilityUsedAction -= OnAbilityUsed;
    }

    private void OnAbilityUsed() { }

    private void FixedUpdate() {

        _rb.velocity = transform.forward.normalized * currentMoveSpeed;

        // Change car move dir
        var rotation = transform.eulerAngles;

        var rotateSpeed = _playerInputs.IsDrifting ? driftRotateSpeed : defaultRotateSpeed;

        var targetYRotation = rotation.y + rotateSpeed * _playerInputs.InputDirection;

        rotation.y = Mathf.Lerp( rotation.y, targetYRotation, Time.fixedDeltaTime * rotateSmooth );
        
        transform.eulerAngles = rotation;

    }

    public void SetSpeed(float moveSpeed)
    {
        currentMoveSpeed = moveSpeed;
    }

    public void ResetSpeed()
    {
        currentMoveSpeed = defaultMoveSpeed;
    }

}