using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInfo : MonoBehaviour
{
    [Header("ScriptableObject")]
    public Note note;
    [Header("WorldSpaceInfo")]
    public float rotationSpeed;
    [Header("SyncInfo")]
    public int spawnTimeSample;
    public float hitTime = 2;
    [Header("GameplayInfo")]
    public bool shouldHit;
    [Header("VisualInfo")]
    public Material material;
    public Mesh noteMesh;

    void Start()
    {
        transform.position = note.spawnPosition;
        //transform.rotation = note.rotationAxis as Quaternion;
        transform.localScale = note.size;
    }
}
