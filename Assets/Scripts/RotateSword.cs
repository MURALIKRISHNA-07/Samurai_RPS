using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSword : MonoBehaviour
{
    private Quaternion initialRotation;
    public EnemyController eController;
    private bool isRotating;
    private Quaternion targetRotation;

    public float L_Rotation = 25.0f;
    public float M_Rotation = 0f;
    public float H_Rotation = -25.0f;

    public float rotationSpeed=5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void OnEnable()
    {
        initialRotation = this.gameObject.transform.rotation;

        int hintValue = eController.ShowHint();

        switch (hintValue)
        {
            case 0:
                // Sword off, stop rotating and moving up/down
                isRotating = false;
                gameObject.SetActive(false);
                break;
            case 1:
                SetTargetRotation(L_Rotation);
                break;
            case 2:
                SetTargetRotation(M_Rotation);
                break;
            case 3:
                SetTargetRotation(H_Rotation);
                break;
            default:
                SetTargetRotation(0f); // Default rotation
                break;
        }
    }

    void SetTargetRotation(float targetRotationValue)
    {
        isRotating = true;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetRotationValue);
    }


    void Update()
    {
        if (isRotating)
        {
            // Interpolate towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void OnDisable()
    {
        // Stop rotating and reset rotation when disabled
        isRotating = false;
        transform.rotation = initialRotation;
    }



}
