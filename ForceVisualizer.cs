using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class ForceVisualizer : MonoBehaviour
{
    int size;
    float thetaScale = 0.01f;
    public bool fadeForcewave = false;
    LineRenderer lineRenderer;
    Color defaultColor = Color.white;
    float currentAlpha = Color.white.a;

    // Start is called before the first frame update
    void Start()
    {
        InitializeRenderer();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeForcewave)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, 0, Time.deltaTime * 20);
            Color newColor = defaultColor;
            newColor.a = currentAlpha;
            lineRenderer.material.SetColor("_TintColor", newColor);

            float currentAlphaPercentage = currentAlpha / defaultColor.a * 100;

            if (currentAlphaPercentage < 0.1)
            {
                newColor.a = 0;
                lineRenderer.material.SetColor("TintColor", newColor);
                StopFading();
            }
        }
    }

    private void InitializeRenderer()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));

        lineRenderer.startWidth = 0.06f;
        lineRenderer.endWidth = 0.06f;

        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        lineRenderer.receiveShadows = false;
    }


    public void VisualizeForce(float radius, Vector2 position)
    {
        lineRenderer.material.SetColor("_TintColor", Color.white);

        float sizeValue = 10 * ((2.0f * Mathf.PI) / thetaScale);

        size = (int)sizeValue;

        size++;

        float theta = 0f;

        lineRenderer.positionCount = size;

        DrawCircle(radius, position, theta);
    }

    private void DrawCircle(float radius, Vector2 position, float theta)
    {
        Vector3 pos;

        for (int i = 0; i < size; i++)
        {
            theta += (2.0f * Mathf.PI * thetaScale);

            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            x += position.x;
            y += position.y;

            pos = new Vector3(x, y, 0);

            lineRenderer.SetPosition(i, pos);
        }
    }

    public void Fade()
    {
        fadeForcewave = true;
    }

    public void StopFading()
    {
        fadeForcewave = false;
        currentAlpha = Color.white.a;
    }
}