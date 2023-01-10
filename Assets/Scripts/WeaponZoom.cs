using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] int ZoomedOutFOV = 40;
    [SerializeField] int ZoomedInFOV = 15;

    private Cinemachine.CinemachineVirtualCamera _followCam;
    private bool _isZoomed = false;
    public bool IsZoomed { get { return _isZoomed; } }

    void Start()
    {
        GameObject followCamGameObj = GameObject.FindGameObjectWithTag("FollowCam");
        _followCam = followCamGameObj.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    public void SetZoom(bool isZoomed)
    {
        if (_isZoomed == isZoomed) return;

        _isZoomed = isZoomed;
        int fov = _isZoomed ? ZoomedInFOV : ZoomedOutFOV;
        _followCam.m_Lens.FieldOfView = fov;
    }
}
