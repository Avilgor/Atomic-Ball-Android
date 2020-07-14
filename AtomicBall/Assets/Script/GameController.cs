using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject goal;
    [SerializeField] GameObject goalGreen;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject cube;
    [SerializeField] GameObject cone;
    [SerializeField] GameObject controls;
    [SerializeField] GameObject speedPower;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] TextMeshProUGUI roundText;
    [SerializeField] float speedIncrease;
    [SerializeField] AudioClip[] songs;
    AudioSource source;
    int roundcounter;
    bool goalReached;
    float counter;
    bool boostSpawn;
    float boostCD;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        source.clip = songs[UnityEngine.Random.Range(0, songs.Length)];
        source.Play();

        Application.targetFrameRate = 60;
        Vector3 point = transform.position;
        point.x = UnityEngine.Random.Range(minX,maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 1;
        ball.transform.position = point;

        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.2f;
        goal.transform.position = point;
        roundcounter = 0;
        goalReached = false;
        counter = 0;
        roundText.SetText(roundcounter.ToString());

        if (cube.activeSelf) cube.SetActive(false);
        if (goalGreen.activeSelf) goalGreen.SetActive(false);
        if (cone.activeSelf) cone.SetActive(false);

        boostSpawn = false;
        boostCD = 30f;
        controls.SetActive(true);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) GoalReached();
        if (goalReached)
        {
            if (counter < 1.0f) counter += Time.deltaTime;
            else Reposition();
        }

        if (roundcounter > 5)
        {
            if (!boostSpawn)
            {
                StartCoroutine(SpawnBoost(UnityEngine.Random.Range(boostCD - 2f, boostCD + 2f)));
                boostSpawn = true;
            }          
        }

        if (roundcounter > 9 && !cone.activeSelf) StartCoroutine(SpawnCone(10f));        

        if (Time.deltaTime == 0) source.Pause();
        else if(!source.isPlaying) source.Play();
    }

    public void GoalReached()
    {
        goalReached = true;
        goal.SetActive(false);
        goalGreen.transform.position = goal.transform.position;
        goalGreen.SetActive(true);
    }

    private void Reposition()
    {
        Vector3 point = transform.position;
        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.5f;
        ball.transform.position = point;

        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.2f;
        goal.transform.position = point;

        roundcounter++;
        roundText.SetText(roundcounter.ToString());

        if (roundcounter % 3 == 0) ball.GetComponent<Ball>().NextRound(speedIncrease, 1f, 0.2f); 
        else ball.GetComponent<Ball>().NextRound(0, 0, 0);

        if (roundcounter > 6 && roundcounter % 2 == 0 && boostCD > 10) boostCD--;

        if (roundcounter == 4) cube.SetActive(true);
        if (cube.activeSelf)
        {
            point.x = UnityEngine.Random.Range(minX, maxX);
            point.z = UnityEngine.Random.Range(minY, maxY);
            point.y = 1.0f;
            cube.transform.position = point;

            if (roundcounter == 7) cube.GetComponent<Cube>().ActivateMovement();
            cube.GetComponent<Cube>().NextRound();
        }

        if (roundcounter > 9)
        {
            point.x = UnityEngine.Random.Range(minX, maxX);
            point.z = UnityEngine.Random.Range(minY, maxY);
            point.y = 0.5f;                      
            cone.transform.position = point;
            cone.SetActive(true);
            cone.GetComponent<Triangle>().NextRound();
        }

        if (roundcounter == 1) controls.SetActive(false);

        goal.SetActive(true);
        goalGreen.SetActive(false);
        counter = 0;
        goalReached = false;
    }

    IEnumerator SpawnBoost(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(speedPower, new Vector3(UnityEngine.Random.Range(minX, maxX), 48, UnityEngine.Random.Range(minY, maxY)),Quaternion.identity);
        boostSpawn = false;
    }

    IEnumerator SpawnCone(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 point = transform.position;
        point.x = UnityEngine.Random.Range(minX, maxX);
        point.z = UnityEngine.Random.Range(minY, maxY);
        point.y = 0.5f;
        cone.transform.position = point;
        cone.SetActive(true);
        cone.GetComponent<Triangle>().NextRound();
    }
}
