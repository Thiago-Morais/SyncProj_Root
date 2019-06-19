using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteVisualizationScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyInSeconds(2));
    }
    void Update()
    {
        transform.Translate(Vector3.forward);
    }
    IEnumerator DestroyInSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        Destroy(gameObject);
    }
}
