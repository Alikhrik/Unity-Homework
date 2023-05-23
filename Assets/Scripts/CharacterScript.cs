using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterScript : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    private Vector3 _moveVector3;
    [SerializeField]
    private float walkSpeed = 1000f;
    [SerializeField]
    private float runSpeed = 1000f;

    private static readonly int MoveState = Animator.StringToHash("MoveState");

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var ix = Input.GetAxis("Horizontal");
        var iy = Input.GetAxis("Vertical");
        // _moveVector3 = new Vector3(ix, 0, iy);
        var transform1 = this.transform;
        _moveVector3 =
            transform1.forward * iy
            + transform1.right * ix;
        if (_moveVector3.magnitude > 1)
        {
            _moveVector3 = _moveVector3.normalized;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            var factor = runSpeed * Time.deltaTime;
            _moveVector3 *= factor;
            _animator.SetInteger(MoveState, _moveVector3.magnitude > _characterController.minMoveDistance ? 2 : 0);
        }
        else
        {
            var factor = walkSpeed * Time.deltaTime;
            _moveVector3 *= factor;
            _animator.SetInteger(MoveState, _moveVector3.magnitude > _characterController.minMoveDistance ? 1 : 0);
        }
        _characterController.SimpleMove(_moveVector3);
    }
}
