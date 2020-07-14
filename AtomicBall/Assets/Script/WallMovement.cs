using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [SerializeField] KeyCode Up;
    [SerializeField] KeyCode Down;
    [SerializeField] KeyCode Right;
    [SerializeField] KeyCode Left;
    [SerializeField] float speed;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip boost;
    [SerializeField] Material boostMaterial;
    public bl_Joystick Joystick;
    float baseSpeed;
    Material baseMaterial;

    private void Start()
    {
        baseSpeed = speed;
        baseMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {         
        if (Time.deltaTime > 0)
        {
            float v = Joystick.Vertical;
            float h = Joystick.Horizontal;
            if (Math.Abs(v) > Math.Abs(h))
            {
                if (v > 0)
                {
                    Vector3 pos = transform.position;
                    pos.z += speed * Time.deltaTime;
                    transform.position = pos;
                }
                else if (v < 0)
                {
                    Vector3 pos = transform.position;
                    pos.z -= speed * Time.deltaTime;
                    transform.position = pos;
                }
            }          
            else 
            {
                if (h < 0)
                {
                    Vector3 pos = transform.position;
                    pos.x -= speed * Time.deltaTime;
                    transform.position = pos;
                }
                else if (h > 0)
                {
                    Vector3 pos = transform.position;
                    pos.x += speed * Time.deltaTime;
                    transform.position = pos;
                }
            }           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Speed"))
        {
            Destroy(collision.gameObject);
            speed += 10;
            StartCoroutine(Boost(5));
            GetComponent<Renderer>().material = boostMaterial;
            source.PlayOneShot(boost);
        }
    }

    IEnumerator Boost(float time)
    {
        yield return new WaitForSeconds(time);
        speed = baseSpeed;
        GetComponent<Renderer>().material = baseMaterial;
    }
}
