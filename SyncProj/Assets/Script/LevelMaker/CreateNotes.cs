using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNotes : MonoBehaviour
{
    public float distanceToCamera;
    public Transform notesParent;
    public AudioSource audiosource;
    public List<GameObject> dummys;
    public List<Vector3> notesPosition;
    public int noteIndex;
    public bool createNotes = false;
    void Start()
    {
        
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) SpawnNote(noteIndex);
        else if(Input.GetKeyDown(KeyCode.Space))
            if (!audiosource.isPlaying) audiosource.Play();
            else audiosource.Pause();
        else if(Input.GetKeyDown(KeyCode.LeftControl)) audiosource.Stop();
        else if(Input.GetKey(KeyCode.Z)) SpawnNote(noteIndex);

    }
    void SpawnNote(int noteIndex)
    {
        Vector3 v3 = new Vector3(
            Mathf.Clamp(Input.mousePosition.x, 0, Camera.main.pixelWidth), 
            Mathf.Clamp(Input.mousePosition.y, 0, Camera.main.pixelHeight), 
            Camera.main.transform.position.z + distanceToCamera);
        v3 = Camera.main.ScreenToWorldPoint(v3);
        notesPosition.Add(v3);
        
        if(createNotes) WriteNotes.WriteInText(v3);
        GameObject noteInst = Instantiate(dummys[noteIndex], notesParent);
        noteInst.transform.position = v3;
        NoteVisualizationScript noteScript = (NoteVisualizationScript)noteInst.AddComponent(typeof(NoteVisualizationScript));
    }
}
