using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HeadBobbing : MonoBehaviour
{
    //private CharacterController characterController;
    //public GameObject cameraObj;

    //public float horizontalInput;
    //public float verticalInput;

    //void Start()
    //{
    //    characterController = GetComponent<CharacterController>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    horizontalInput = Input.GetAxis("Horizontal");
    //    verticalInput = Input.GetAxis("Vertical");

    //    if ((horizontalInput != 0 || verticalInput != 0) && characterController.isGrounded)
    //    {

    //        if (Input.GetButton("Sprint"))
    //        {
    //            StartBobbing();
    //        }
    //        else
    //        {

    //            StartBobbing();
    //        }

    //    }
    //    else
    //    {
    //        StopBobbing();
    //    }
    //}

    //public void StartBobbing()
    //{
    //    cameraObj.GetComponent<Animator>().Play("Bobbing");
    //}
    //public void StopBobbing()
    //{
    //    cameraObj.GetComponent<Animator>().Play("Stop");
    //}



    [Header("Configuration")]
    [SerializeField] private bool _enable = true;
    [SerializeField, Range(0, 0.1f)] private float _Amplitude = 0.015f;
    [SerializeField, Range(0, 30)] private float _frequency = 10.0f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }

    void Update()
    {
        if (!_enable) return;
        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _Amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _Amplitude * 2;
        return pos;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }



}
