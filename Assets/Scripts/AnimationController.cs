using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private CharacterController _charController;
    private Animator _animator;

    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private float moveSpeed;

    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");

        if (tempPlayer == null)
        {
            Debug.LogError("No GameObject with tag 'Player' found!");
            return;
        }

        _charController = tempPlayer.GetComponent<CharacterController>();
        if (_charController == null)
        {
            Debug.LogError("CharacterController component missing on 'Player' GameObject.");
            return;
        }

        _animator = tempPlayer.GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator component missing on 'Player' GameObject or its children.");
            return;
        }

        moveSpeed = 4f;
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        float speed = new Vector2(inputX, inputZ).magnitude;

        if (speed > 0.1f)
        {
            if (inputZ > 0.5f)
            {
                _animator.SetBool("isRun", true);
                _animator.SetBool("isWalk", false);
            }
            else
            {
                _animator.SetBool("isRun", false);
                _animator.SetBool("isWalk", true);
            }
        }
        else
        {
            _animator.SetBool("isRun", false);
            _animator.SetBool("isWalk", false);
        }

        Debug.Log($"InputX: {inputX}, InputZ: {inputZ}, Speed: {speed}, isRun: {_animator.GetBool("isRun")}, isWalk: {_animator.GetBool("isWalk")}");
    }

    private void FixedUpdate()
    {
        v_movement = _charController.transform.forward * inputZ;

        _charController.transform.Rotate(Vector3.up * inputX * (100f * Time.deltaTime));
        _charController.Move(v_movement * moveSpeed * Time.deltaTime);
    }
}
