using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [Header("InstanceInfo")]

    public GameObject instPrefab;
    public Transform instParent;
    public Player player;
    public GameObject terminateObj;
    public Spawn spawn;
    public float speed;
    [Header("Audio")]
    public AudioSource audioSource;
    public bool printTime = true;
    public int bpm;
    [Header("NotesSync")]
    public List<int> notesWaitBeatDivision;         //DIVISÃO DO BEAT PARA SPAWNAR A NOTA
    public List<int> notesSpawnWaitTime;            //PARTE DO BEAT DIVIDIDO A SER TOCADO
    [SerializeField] private List<float> samplesNotesWaitTimes;     //TEMPOS QUE CADA PROXIMA NOTA TEM QUE ESPERAR EM TIMESAMPLES
    [SerializeField] private int totalTimeSample;
    [SerializeField] private int sampleWaitTimeIndex;               //QUAL TEMPO ESTA SENDO USADO
    [SerializeField] private float nextSpawnTimeSample;             //QUANDO A PROXIMA NOTA SERA SPAWNADA
    [SerializeField] private float noteOffsetSec;                      //QUANTO TEMPO A NOTA VAI APARECER ANTES DO MOMENTO DO HIT (EM SEGUNDOS)
    private float bps, tspb;     //BEATS POR SEGUNDO E TIMESAMPLES POR BEAT
    private bool keepNotesPlaying;  //BOOL PARA CONTROLAR QUANDO PARAR
    private int lastTimeSample = -1;    //TIMESAMPLE PRA CONTROLAR QUANDO A MUSICA REPETIR
    [Header("NotesPlacement")]
    public Notes notesInfo = new Notes();
    [System.Serializable] public class Notes
    {
        public List<Note> notes;
    }
    [System.Serializable] public class Note
    {
        public int WaitBeatDivision;
        public int SpawnWaitTime;
        public float SpawnTime;
        public Vector2 SpawnPosition;
        public bool NeedToHit;
    }
    [SerializeField] private Note n_Default;
    void Start()
    {
        bps = bpm / 60.0f;                          //BEATS POR SEGUNDO
        tspb = audioSource.clip.frequency / bps;     //TIMESAMPLES POR BEATS

        while(notesSpawnWaitTime.Count < notesWaitBeatDivision.Count) notesSpawnWaitTime.Add(4);        //IGUALA O TAMANHO DAS LISTAS AO TAMANHO DA LISTA DE TEMPO DE ESPERA EM SAMPLES
        while(samplesNotesWaitTimes.Count < notesWaitBeatDivision.Count) samplesNotesWaitTimes.Add(0);  //IGUALA O TAMANHO DAS LISTAS AO TAMANHO DA LISTA DE TEMPO DE ESPERA EM SAMPLES
        while(notesInfo.notes.Count < notesWaitBeatDivision.Count) notesInfo.notes.Add(n_Default);

        for(int i = 0; i < samplesNotesWaitTimes.Count; i++)
        {
            notesInfo.notes[i].SpawnPosition = WriteNotes.GetVector3(i);
            notesInfo.notes[i].WaitBeatDivision = notesWaitBeatDivision[i];
            notesInfo.notes[i].SpawnWaitTime = notesSpawnWaitTime[i];
            samplesNotesWaitTimes[i] = (tspb / 4) * notesInfo.notes[i].WaitBeatDivision/* DIVIDIR O BEAT */ * notesInfo.notes[i].SpawnWaitTime/* QUANTAS DESSAS DIVISOES TEM QUE ESPERAR */;
            totalTimeSample += (int)samplesNotesWaitTimes[i];
            notesInfo.notes[i].SpawnTime = samplesNotesWaitTimes[i];
        }

        instParent.position = transform.position;
        objectPooler = ObjectPooler.Instance;
    }
    ObjectPooler objectPooler;
    void FixedUpdate()
    {
        objectPooler.SpawnFromPool("Note", transform.position, Quaternion.identity);
    }
    [EasyButtons.Button]
    public void StartNotes()
    {
        keepNotesPlaying = true;
        StartCoroutine(NoteChart());
    }
    IEnumerator NoteChart()
    {
        float noteOffsetTimeSample = noteOffsetSec * audioSource.clip.frequency;            //CALCULA QUANTO TEMPO A NOTA VAI APARECER ANTES DO MOMENTO DO HIT

        nextSpawnTimeSample = samplesNotesWaitTimes[sampleWaitTimeIndex];
        sampleWaitTimeIndex++;
        
        bool keySwitch = true;
        while (keySwitch)
        {
            if(keepNotesPlaying)                                                            //VERIFICA SE CONTINUA SPAWNANDO NOTA
                if(AudioLooped()){keySwitch = false; StartNotesAgain();}                    //QUANDO FAZ O LOOP, REINICIA AS NOTAS
                else if(audioSource.timeSamples >= nextSpawnTimeSample - noteOffsetTimeSample)                     //SPAWNA UMA NOTA QUANDO CHEGAR NO TEMPO DELA 
                {
                    SpawnInstance();                                                        //SPAWNA A NOTA
                    if(sampleWaitTimeIndex < samplesNotesWaitTimes.Count)                   //VERIFICA SE JA PASSARAM TODAS AS NOTAS SALVAS
                    {
                        nextSpawnTimeSample += samplesNotesWaitTimes[sampleWaitTimeIndex];  //AUMENTA O TEMPO QUE OUTRA NOTA VAI APARECER 
                        sampleWaitTimeIndex++;                                              //VAI PRA O TEMPO DE ESPERA DA PROXIMA NOTA
                    }
                    else break;
                }
            yield return null;
        }
    }
    bool AudioLooped()      //FUNÇÃO PRA VERIFICAR SE A MUSICA RECOMEÇOU
    {
        bool looped = false;
        if (audioSource.timeSamples >= lastTimeSample) lastTimeSample = audioSource.timeSamples;
        else {lastTimeSample = -1; looped = true;}
        return looped;
    }
    void StartNotesAgain()
    {
        sampleWaitTimeIndex = 0;
        StartCoroutine(NoteChart());
    }
    public void SpawnInstance()
    {
        GameObject instance = GameObject.Instantiate(instPrefab, instParent);
        /* Vector3 v3 = new Vector3(
            Random.Range(0, Camera.main.pixelWidth),
            Random.Range(0, Camera.main.pixelHeight),
            6); */
        //v3 = Camera.main.ScreenToWorldPoint(v3);
        Vector3 v3 = new Vector3(
            notesInfo.notes[sampleWaitTimeIndex].SpawnPosition.x,
            notesInfo.notes[sampleWaitTimeIndex].SpawnPosition.y,
            6);
        v3.z = 0;
        instance.transform.localPosition = v3;
        instance.name = instPrefab.name;

        InstanceScript instScript = instance.AddComponent<InstanceScript>();
        instScript.spawn = spawn;
        instScript.player = player;
        instScript.terminateObj = terminateObj;
        instScript.speed = speed;
        instScript.m_timeSample = (int)nextSpawnTimeSample;
    }

    public void StopNotes()
    {
        keepNotesPlaying = false;
    }
    [EasyButtons.Button]
    public void SetWaitBeatDivision()
    {
        while(notesWaitBeatDivision.Count > notesInfo.notes.Count) notesInfo.notes.Add(n_Default);
        for(int i = 0; i < notesWaitBeatDivision.Count; i++) 
            notesInfo.notes[i].WaitBeatDivision = notesWaitBeatDivision[i];
    }
    [EasyButtons.Button]
    public void SetSpawnWaitTime()
    {
        while(notesSpawnWaitTime.Count > notesInfo.notes.Count) notesInfo.notes.Add(n_Default);
        for(int i = 0; i < notesSpawnWaitTime.Count; i++) notesInfo.notes[i].SpawnWaitTime = notesSpawnWaitTime[i];
    }
}
