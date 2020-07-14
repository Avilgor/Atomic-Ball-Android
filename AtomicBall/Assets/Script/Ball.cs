using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject ballMesh;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip ballHit;
    [SerializeField] AudioClip stunned;
    [SerializeField] AudioClip boost;
    [SerializeField] AudioClip goal;
    [SerializeField] Material boostMaterial;
    [SerializeField] Material stunnedMaterial;
    public float speed = 0.01f;
    float count;
    [SerializeField] float angle;
    float rotationAngle;
    bool locked;
    float time;
    float basespeed;
    Material baseMaterial;

    void Start()
    {
        count = 0;
        locked = false;
        time = 1.0f;
        basespeed = speed;
        baseMaterial = ballMesh.GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (Time.deltaTime > 0 && !locked)
        {
            transform.Rotate(0, rotationAngle, 0);

            Vector3 pos = transform.position;        
            pos.z += transform.forward.z * speed * Time.deltaTime;
            pos.x += transform.forward.x * speed * Time.deltaTime;
            pos.y = 0.5f;
            transform.position = pos;
            ballMesh.transform.Rotate(0, 0, speed);
            

            if (count < time) count += Time.deltaTime;           
            else
            {
                count = 0;
                rotationAngle = UnityEngine.Random.Range(-angle, angle);
            }
        }
    }

    public void NextRound(float sp,float t,float ag)
    {
        locked = false;
        if(speed < 20f) speed += sp;
        if (time > 0.5f) time -= t;
        if (angle < 3f) angle += ag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Goal"))
        {
            GameObject.Find("GameController").GetComponent<GameController>().GoalReached();
            source.PlayOneShot(goal);
            locked = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))        
        {
            if(!source.isPlaying) source.PlayOneShot(ballHit);
        }

        if (collision.gameObject.CompareTag("Triangle"))
        {
            locked = true;
            StartCoroutine(Stunned(3.0f));
            source.PlayOneShot(stunned);
            collision.gameObject.SetActive(false);
            ballMesh.GetComponent<Renderer>().material = stunnedMaterial;
        }

        if (collision.gameObject.CompareTag("Speed"))
        {
            basespeed = speed;
            speed += 5;
            Destroy(collision.gameObject);
            source.PlayOneShot(boost);
            StartCoroutine(Boost(5));
            ballMesh.GetComponent<Renderer>().material = boostMaterial;
        }
    }

    IEnumerator Stunned(float time)
    {
        yield return new WaitForSeconds(time);
        locked = false;
        ballMesh.GetComponent<Renderer>().material = baseMaterial;
    }

    IEnumerator Boost(float time)
    {
        yield return new WaitForSeconds(time);
        speed = basespeed;
        ballMesh.GetComponent<Renderer>().material = baseMaterial;
    }
}
