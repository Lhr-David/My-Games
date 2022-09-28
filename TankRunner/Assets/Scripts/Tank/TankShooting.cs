﻿using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public enum ShootState
    {
        Disabled,
        Normal,
        Charge,
    }
    public ShootState shootState;

    public Rigidbody m_Shell;                   // Prefab of the shell.
    public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;                // Audio that plays when each shot is fired.

    public float launchForce;         // The force that will be given to the shell when the fire button is released
    public WinBullet prefab;

    private void Start()
    {
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            Fire();
        }
    }

    private void Fire()
    {
        if (shootState == ShootState.Disabled)
        {
            return;
        }
        if (shootState == ShootState.Charge)
        {
            Debug.Log("shoot win bullet");
            var value = ChargeSystem.instance.ShowFiredInformationAndGetFireValue();
            Debug.Log(value);

            ShootWinBullet(value);
            return;
        }

        Rigidbody shellInstance =
            Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = launchForce * m_FireTransform.forward;
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

    void ShootWinBullet(float v)
    {
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        var winBullet = Instantiate(prefab, m_FireTransform.position, prefab.transform.rotation);
        winBullet.Init(v);
    }
}