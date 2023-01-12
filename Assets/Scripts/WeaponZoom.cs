using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] int ZoomedOutFOV = 40;
    [SerializeField] int ZoomedInFOV = 15;

    private bool _isZoomed = false;
    public bool IsZoomed { get { return _isZoomed; } }

    public void Zoom()
    {
        _isZoomed = !_isZoomed;
        int fov = _isZoomed ? ZoomedInFOV : ZoomedOutFOV;
        playerFollowCamera.m_Lens.FieldOfView = fov;
    }
}
