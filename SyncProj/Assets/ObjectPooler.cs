using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    #region Singleton
        public static ObjectPooler Instance;
        private void Awake()
        {
            Instance = this;
        }
    #endregion
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();       //Cria o Dicionario

        foreach (Pool pool in pools)                                        //Adiciona as Piscinas no Dicionario 
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();             //Cria a Piscina de Objetos

            for(int i = 0; i < pool.size; i++)                                  //Enche a piscina com Objetos
            {
                GameObject obj =  Instantiate(pool.prefab);                         //Cria o Objeto
                obj.SetActive(false);                                               //Desativa
                objectPool.Enqueue(obj);                                            //Joga no final da piscina
            }
            poolDictionary.Add(pool.tag, objectPool);                           //Coloca a piscina no Dicionario
        }
    }
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(tag)) return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();           //Tira o Primeiro elemento da Piscina
                                                                            //Usa
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);                         //Coloca ele no Final da Piscina

        return objectToSpawn;
    }
}
