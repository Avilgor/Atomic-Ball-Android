using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform dir;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip boost;
    [SerializeField] Material boostMaterial;
    float basespeed;
    Material baseMaterial;


    void Start()
    {
        basespeed = speed;
        baseMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime > 0)
        {
            Vector3 pos = transform.position;
            pos.x += dir.transform.forward.x * speed * Time.deltaTime;
            pos.z += dir.transform.forward.z * speed * Time.deltaTime;
            transform.position = pos;
        }
    }

    public void NextRound()
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        dir.Rotate(0, 180, 0);
        float angle = UnityEngine.Random.Range(-90, 90);
        dir.Rotate(0, angle, 0);       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9) NextRound();

        if (collision.gameObject.CompareTag("Speed"))
        {
            Destroy(collision.gameObject);
            speed += 5;
            StartCoroutine(Boost(5));
            source.PlayOneShot(boost);
            GetComponent<Renderer>().material = boostMaterial;
        }
    }

    IEnumerator Boost(float time)
    {
        yield return new WaitForSeconds(time);
        speed = basespeed;
        GetComponent<Renderer>().material = baseMaterial;
    }
}
