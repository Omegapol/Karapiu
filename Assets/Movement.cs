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
    private float extensionRotation = 0.0f;

    public GameObject[] fullArm;
    public GameObject[] rotationBall;
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

    void updateRotation()
    {
        GameObject armbase = fullArm[0];
        GameObject armextension = fullArm[3];
        Reset();
        armextension.transform.RotateAround(rotationBall[0].transform.position, directionRIGHT, extensionRotation);
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
        foreach (GameObject gameObject in fullArm)
        {
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                gameObject.transform.position += Vector3.forward * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                gameObject.transform.position += Vector3.back * speed * Time.deltaTime;
            }
            /*
            if (Input.GetKey(KeyCode.F))
            {
                if (Input.GetKey(KeyCode.LeftControl))
                    scale += 3.0f * Time.deltaTime;
                else
                    scale -= 3.0f * Time.deltaTime;

                gameObject.transform.localScale = new Vector3(scale, scale, scale);
            }*/
            if (Input.GetKey(KeyCode.E))
            {
                armRotation += 1.0f;
                updRotation = true;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                armRotation -= 1.0f;
                updRotation = true;
            }
        }
        if (Input.GetKey(KeyCode.T))
        {
            extensionRotation += 1.0f;
            updRotation = true;
        }
        if (Input.GetKey(KeyCode.G))
        {
            extensionRotation -= 1.0f;
            updRotation = true;
        }
        if (updRotation)
        {
            updateRotation();
        }
    }
}
