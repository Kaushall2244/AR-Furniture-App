using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;

    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    void Start()
    {
        if (arCam == null)
        {
            arCam = Camera.main;
        }

        if (_raycastManager == null)
        {
            _raycastManager = FindObjectOfType<ARRaycastManager>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverUI())
                return;

            Ray ray = arCam.ScreenPointToRay(Input.mousePosition);

            if (_raycastManager != null && _raycastManager.Raycast(ray, _hits, TrackableType.Planes))
            {
                Pose pose = _hits[0].pose;

                if (DataHandler.Instance != null && DataHandler.Instance.furniture != null)
                {
                    Instantiate(DataHandler.Instance.furniture, pose.position, pose.rotation);
                }
            }
        }
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return EventSystem.current.IsPointerOverGameObject(touch.fingerId);
        }

        return EventSystem.current.IsPointerOverGameObject();
    }
}