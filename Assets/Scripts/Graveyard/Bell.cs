using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bell : MonoBehaviour
{
    [SerializeField] AnimationCurve bellCurve;

    [SerializeField] Transform bellModel;
    [SerializeField] float tollingTime;
    [SerializeField] AudioSource bell;

    private float tollCurvePoint;

    bool isTolling = false;

    private void Start()
    {
        DayNightCycle.dayStart += StartTolling;
    }

    private void OnDestroy()
    {
        DayNightCycle.dayStart -= StartTolling;
    }

    private void Update()
    {
        if (!isTolling) return;

        TollTheBell();
    }

    void StartTolling()
    {
        bell.Play();

        if (isTolling) return; isTolling = true;

        tollCurvePoint = 0;

        bellModel.localRotation = Quaternion.Euler(0, 0, bellCurve.Evaluate(tollCurvePoint));
    }

    void TollTheBell()
    {
        tollCurvePoint += Time.fixedDeltaTime / tollingTime;

        if (tollCurvePoint >= 1)
        {

            tollCurvePoint = 0;
            isTolling = false;
        }

        bellModel.localRotation = Quaternion.Euler(0, 0, bellCurve.Evaluate(tollCurvePoint));
    }

}
