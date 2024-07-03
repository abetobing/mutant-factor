using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Assertions;

public class VirtualCamera : MonoBehaviour
{
    [Range(1f, 10f)]
    [SerializeField] private float cameraMovementSpeed = 10f;
    [SerializeField] private bool enableMouseScreenEdgeScrolling = true;
    [SerializeField] private bool enableMouseDragPanScrolling = true;
    [Range(0.1f, 1f)]
    [SerializeField] private float dragPanSpeed = 1f;
    [SerializeField] private bool enableRotation = true;
    [SerializeField] private bool enableZoom = false;
    [SerializeField] private float zoomSpeed = 10f;
    [SerializeField] public CinemachineVirtualCamera CinemachineVirtualCamera;

    private Vector3 _lastMousePosition;
    private bool _dragPanMoveStarted = false;
    private Vector3 _followOffset;
    // minimum and maximum Y amount to be clamped
    private const float MinZooom = 2.5f;
    private const float MaxZoom = 15f;

    private void Awake()
    {
        if (enableZoom)
        {
            // make sure CinemachineTransposer reference is set
            // and make sure the body is a Transposer
            Assert.IsTrue(enableZoom && CinemachineVirtualCamera != null, "CinemachineVirtualCamera reference must be assigned if zoom is enabled");
            Assert.IsNotNull(
                CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>(),
                "CinemachineVirtualCamera dont have CinemachineTransposer settings."
            );
            _followOffset = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        }
    }

    void Update()
    {
        HandleCameraMovementWASD();
        if (enableMouseScreenEdgeScrolling) HandleCameraMovementMouseScreenEdge();
        if (enableMouseDragPanScrolling) HandleCameraMovementMouseDragPan();
        if (enableRotation) HandleCameraRotationWithKeyboard();
        if (enableZoom) HandleCameraZoom();
    }

    private void HandleCameraMovementWASD()
    {
        Vector3 inputDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputDirection.z += 1f;
        if (Input.GetKey(KeyCode.S)) inputDirection.z -= 1f;
        if (Input.GetKey(KeyCode.A)) inputDirection.x -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDirection.x += 1f;

        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraMovementSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementMouseScreenEdge()
    {
        Vector3 inputDirection = Vector3.zero;
        int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize)
        {
            inputDirection.x -= 1f;
        }
        if (Input.mousePosition.y < edgeScrollSize)
        {
            inputDirection.z -= 1f;
        }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDirection.x += 1f;
        }
        if (Input.mousePosition.y > Screen.height - edgeScrollSize)
        {
            inputDirection.z += 1f;
        }
        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraMovementSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementMouseDragPan()
    {
        Vector3 inputDirection = Vector3.zero;

        if (Input.GetMouseButtonDown(1))
        {
            _dragPanMoveStarted = true;
            _lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _dragPanMoveStarted = false;
        }

        if (_dragPanMoveStarted)
        {
            Vector3 mouseMovementDelta = Input.mousePosition - _lastMousePosition;
            inputDirection.x = mouseMovementDelta.x * dragPanSpeed;
            inputDirection.z = mouseMovementDelta.y * dragPanSpeed;
            Debug.Log(mouseMovementDelta);
            _lastMousePosition = Input.mousePosition;
        }
        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += moveDirection * cameraMovementSpeed * Time.deltaTime;
    }

    private void HandleCameraRotationWithKeyboard()
    {
        float rotationDirection = 0f;
        const float rotationSpeed = 2f;
        if (Input.GetKey(KeyCode.Q)) rotationDirection = +1f;
        if (Input.GetKey(KeyCode.E)) rotationDirection = -1f;

        transform.eulerAngles += new Vector3(0f, rotationDirection * rotationSpeed, 0f);
    }

    private void HandleCameraZoom()
    {
        float zoomAmount = 2.5f;
        if (Input.mouseScrollDelta.y > 0)
        {
            _followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _followOffset.y += zoomAmount;
        }

        _followOffset.y = Mathf.Clamp(_followOffset.y, MinZooom, MaxZoom);
        
        CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(
            CinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset,
            _followOffset,
            Time.deltaTime
        );

    }
}
