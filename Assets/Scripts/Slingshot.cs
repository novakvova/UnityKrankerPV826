﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    // Поля, устанавливаемые в инспекторе Unity
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    // Поля, устанавливаемые динамически
    [Header("Set Dynamically")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMood;
    private Rigidbody projectileRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        // если рогатка не в режиме прицеливанияб не выполнять этот код
        if (!aimingMood) return;

        // Получить текущие экранные кор. указателя мыши
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // Найти разность коорд. между launchPos и mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;

        // Ограничить mouseDelta радиусом коллайдера обьекта SLINGSHOT 
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Передвинуть снаряд в новую позицию
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Input.GetMouseButtonUp(0))
        {
            // Кнопка мыщи отпущена
            aimingMood = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }
    private void OnMouseEnter()
    {
        print("Mouse Enter");
        launchPoint.SetActive(true);
    }
    private void OnMouseExit()
    {
        print("Mouse Exit");
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMood = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        // Сделать его кинематическим
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

}
