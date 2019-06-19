using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[RequireComponent(typeof(TextMesh))]
public class GetNotes : MonoBehaviour
{
public static List<string> textList;    //LINHAS DE TEXTO
    public int[] rowsToRead;    //EXPEXIFICAR A LINHA PRA LER
    public string fileName;     //NOME DO ARQUIVO DE TEXTO
    private TextMesh textComp;  //COMPONENTE QUE VAI RECEBER O TEXTO
    private TextAsset TextAsset;    //COMPONENTE DO TXT
    void Start()
    {
        TextAsset = Resources.Load("TextFiles/" + fileName) as TextAsset;
        TextAssetToList();
    }
    
    void TextAssetToList()
    {
        textList = TextAsset.text.Split('\n').ToList();
    }
}
