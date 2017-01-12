using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed = 10.0f;
    private float scale = 1.0f;
    private Vector3 directionUP = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 directionRIGHT = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3[] startPosition;
    private Quaternion[] startRotation;

    private float armRotation = 0.0f;
    private float mainRotation = 0.0f;
    private float extensionRotation = 0.0f;
    private float welderUpDownRotation = 0.0f;
    private float welderRotation = 0.0f;

    public GameObject[] fullArm;
    public GameObject[] mainArm;
    public GameObject[] extensionArm;
    public GameObject[] welder;
    public GameObject[] welderUpDownRotator;
    public GameObject[] rotationBall;

    public float rotationSpeedExtension = 1.0f;
    public float rotationSpeedMain = 1.0f;
    public float rotationSpeedWelder = 1.0f;

    public string stringToEdit = "";
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 100), stringToEdit);
    }

    private void updateRotationLabel()
    {
        stringToEdit = @"Lower arm rotation(F\H) :" + (int)armRotation + @"
Upper arm rotation(T\G) :" + (int)extensionRotation + @"
Main arm rotation(U\J) :" + (int)mainRotation + @"
Welder up/down rotation(I\K) :" + (int)welderUpDownRotation + @"
Welder rotation(R\Y) :" + (int)welderRotation;
    }

    // Use this for initialization
    void Start()
    {
        startPosition = new Vector3[fullArm.Length];
        startRotation = new Quaternion[fullArm.Length];
        int i = 0;
        foreach (GameObject gameObject in fullArm)
        {
            startPosition[i] = gameObject.transform.position;
            startRotation[i] = gameObject.transform.rotation;
            i++;
        }
        updateRotationLabel();
    }

    void Reset()
    {
        int i = 0;
        foreach (GameObject gameObject in fullArm)
        {
            gameObject.transform.position = startPosition[i];
            gameObject.transform.rotation = startRotation[i];
            i++;
        }
    }

    private void updateRotation()
    {
        updateRotationLabel();
        GameObject armbase = fullArm[0];
        //GameObject armextension = fullArm[2];
        Reset();
        foreach (GameObject gameObject in welder)
        {
            gameObject.transform.RotateAround(rotationBall[1].transform.position, directionUP, welderRotation);
        }
        foreach (GameObject gameObject in welderUpDownRotator)
        {
            gameObject.transform.RotateAround(rotationBall[3].transform.position, directionRIGHT, welderUpDownRotation);
        }
        foreach (GameObject gameObject in extensionArm)
        {
            gameObject.transform.RotateAround(rotationBall[0].transform.position, directionRIGHT, extensionRotation);
        }
        foreach (GameObject gameObject in mainArm)
        {
            gameObject.transform.RotateAround(rotationBall[2].transform.position, directionRIGHT, mainRotation);
        }
        foreach (GameObject gameObject in fullArm)
        {
            gameObject.transform.RotateAround(armbase.transform.position, directionUP, armRotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool updRotation = false;
        GameObject armBase = fullArm[0];

        if (Input.GetKey(KeyCode.F))
        {
            armRotation += rotationSpeedMain;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.H))
        {
            armRotation -= rotationSpeedMain;
            updRotation = true;
        }

        if (Input.GetKey(KeyCode.T))
        {
            extensionRotation -= rotationSpeedExtension;
            if (extensionRotation < -180) extensionRotation = -180;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.G))
        {
            if (mainRotation + extensionRotation * 1.2f < 90)
                extensionRotation += rotationSpeedExtension;
            if (extensionRotation > 65) extensionRotation = 65;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.R))
        {
            welderRotation += rotationSpeedWelder;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            welderRotation -= rotationSpeedWelder;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.U))
        {
            mainRotation -= rotationSpeedMain;
            if (mainRotation < -45) mainRotation = -45;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.J))
        {
            if (mainRotation + extensionRotation*1.2f < 90)
                mainRotation += rotationSpeedMain;
            if (mainRotation > 45) mainRotation = 45;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.I))
        {
            welderUpDownRotation += rotationSpeedWelder;
            if (welderUpDownRotation > 0) welderUpDownRotation = 0;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.K))
        {
            welderUpDownRotation -= rotationSpeedWelder;
            if (welderUpDownRotation < -180) welderUpDownRotation = -180;
            updRotation = true;
        }
        if (updRotation)
        {
            updateRotation();
        }
    }
}
