using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BouncyBall : MonoBehaviour
{
    public GameObject Center;

    public float forceMaxRadius = 3f;

    Vector2 forcePosition = Vector2.zero;

    private bool exertForce = false;

    private bool canPush = true;

    private float currentRadius = 0f;

    ForceVisualizer fv;

    public float forceGunCooldown = 1f;

    private float currentRadiusPercentage = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        fv = GetComponent<ForceVisualizer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(exertForce)
        {
            currentRadius = Mathf.Lerp(currentRadius, forceMaxRadius, Time.deltaTime * 30);
            ExertForce(forcePosition, currentRadius);
        }

        currentRadiusPercentage = currentRadius / forceMaxRadius * 100;

        if(currentRadiusPercentage > 70)
        {
            if (!fv.fadeForcewave)
            {
                fv.Fade();
            }
        }

        if(currentRadiusPercentage > 90)
        {
            DisableForceWave();
        }
    }

    public Vector2 GetForcePosition()
    {
        return Center.transform.position;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            ActivateForceWave();
        }
    }

    private void ActivateForceWave()
    {
        currentRadius = 0;
        forcePosition = GetForcePosition();
        exertForce = true;
        canPush = true;
    }

    private void DisableForceWave()
    {
        currentRadius = 0;
        canPush = false;
        exertForce = false;
    }

    private void ExertForce(Vector2 position, float radius)
    {
        fv.VisualizeForce(radius, forcePosition);

        if (canPush)
        {
            PushObject(position);
        }
        canPush = false;
    }


    private void PushObject(Vector2 position)
    {
        GameObject player = GameObject.Find("Player");

        Vector2 pos = new Vector2(player.transform.position.x, player.transform.position.y);

        Rigidbody2D rig = player.GetComponent<Rigidbody2D>();

        rig.velocity = Vector3.zero;

        rig.AddForce((pos - position).normalized * 25.5f, ForceMode2D.Impulse);
    }
}
