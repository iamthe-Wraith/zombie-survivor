using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] int currentWeapon = 0;

    private StarterAssetsInputs _input;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        SetWeaponActive();
    }

    void Update()
    {
        if (_input.IsDisabled) return;
        
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessGamepadInput();
        
        if (previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }

    private void ProcessKeyInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.digit1Key.wasPressedThisFrame)
        {
            currentWeapon = 0;
            return;
        }
        
        if (keyboard.digit2Key.wasPressedThisFrame)
        {
            currentWeapon = 1;
            return;
        }
        
        if (keyboard.digit3Key.wasPressedThisFrame)
        {
            currentWeapon = 2;
            return;
        }
    }

    private void ProcessGamepadInput()
    {
        Gamepad gamepad = Gamepad.current;
        if (gamepad == null) return;

        if (gamepad.rightShoulder.wasPressedThisFrame)
        {
            currentWeapon += 1;
            if (currentWeapon >= weapons.Length) currentWeapon = 0;
            return;
        }
        
        if (gamepad.leftShoulder.wasPressedThisFrame)
        {
            currentWeapon -= 1;
            if (currentWeapon < 0) currentWeapon = (weapons.Length - 1);
            return;
        }
    }

    private void SetWeaponActive()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeapon);
        }
    }
}
