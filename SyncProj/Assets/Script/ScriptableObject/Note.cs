using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Note")]
public class Note : ScriptableObject
{
    [Header("WorldSpaceInfo")]
    public Vector3 spawnPosition;
    public Vector3 size = Vector3.one;
    public Vector3 rotationAxis;
    public float rotationSpeed;
    [Header("SyncInfo")]
    public int spawnTimeSample;
    public float hitTime = 2;
    [Header("GameplayInfo")]
    public bool shouldHit;
    [Header("VisualInfo")]
    public Material material;
    public Mesh noteMesh;
}
