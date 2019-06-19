using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScaleToView : MonoBehaviour
{
    [Header("ViewAjust")]
    public GameObject note;
    public Spawn spawn;
    public TypeOfView TypeOfScenario;
    public enum TypeOfView {Plane, Width, Heigth}
    public Transform mesh;
    public bool isObjLocPos;
    public float distanceToView;
    public Transform ball;
    [Header("Audio")]
    public AudioSource audioSource;
    private bool playAudio = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 v3 = new Vector3(
            Camera.main.pixelWidth,
            Camera.main.pixelHeight,
            6);
        v3 = Camera.main.ScreenToWorldPoint(v3) * 2;

        if(TypeOfScenario == TypeOfView.Plane)  //SE FOR OS PLANOS DE PROFUNDIDADE
        {
            transform.position = new Vector3(
                Camera.main.transform.position.x,
                Camera.main.transform.position.y,
                ball.position.z + distanceToView);
            if(mesh != null) mesh.transform.localScale = v3;
        } 
        else if(TypeOfScenario == TypeOfView.Width) //SE FOR OS PLANOS LATERAIS
        {
            transform.localPosition = new Vector3(0, v3.y / (isObjLocPos ? 2 : -2), 0);
            v3.y = 106;
            mesh.transform.localScale = v3;
        } 
        else if(TypeOfScenario == TypeOfView.Heigth)    //SE FOR OS PLANOS SUPERIOR E INFERIOR
        {
            transform.localPosition = new Vector3(v3.x / (isObjLocPos ? 2 : -2), 0, 0);
            v3.x = v3.y;
            v3.y = 106;
            mesh.transform.localScale = v3;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == note.name) spawn.printTime = false;
    } 
}
