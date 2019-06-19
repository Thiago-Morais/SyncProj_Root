using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceScript : MonoBehaviour
{
    public Note note;
    [Header("References")]
    public Spawn spawn;
    public Player player;
    public GameObject terminateObj;
    [Header("ReceivedInfo")]
    public float speed = 300; //DEFAULT SPEED, CHANGED IN SPAWN
    public int m_timeSample;
    [Header("InternInfo")]
    private bool hit = false;
    private float t;
    private int timeSapleAtSpawn;
    void Start()
    {
        timeSapleAtSpawn = spawn.audioSource.timeSamples;
        if(timeSapleAtSpawn == 0) Destroy(gameObject);
    }
    void Update()
    {
        t = Mathf.InverseLerp(timeSapleAtSpawn, m_timeSample, spawn.audioSource.timeSamples);

        Vector3 v3 = new Vector3(transform.position.x, transform.position.y);
        v3.z = Mathf.Lerp(spawn.instParent.transform.position.z, terminateObj.transform.position.z, t);

        transform.position = v3;

        if(t >= 1) Destroy(gameObject);
        if(timeSapleAtSpawn - spawn.audioSource.timeSamples >= spawn.audioSource.clip.frequency * 2) Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(!hit)
        {
            hit = true;
            if(other.name == player.name) player.uiHit.count++;
            else if(other.name == terminateObj.name) player.uiAvoid.count++;
        }
    }
}
