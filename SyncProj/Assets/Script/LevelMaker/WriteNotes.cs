using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[RequireComponent(typeof(TextMesh))]
public class WriteNotes : MonoBehaviour
{
    public List<string> textList = new List<string>();    //LINHAS DE TEXTO
    public static List<string> textList_i = new List<string>();    //LINHAS DE TEXTO
    public  int[] rowsToRead;    //EXPEXIFICAR A LINHA PRA LER
    public static int[] rowsToRead_i;    //EXPEXIFICAR A LINHA PRA LER
    public string fileName;     //NOME DO ARQUIVO DE TEXTO
    public static string fileName_i;     //NOME DO ARQUIVO DE TEXTO
    public  TextMesh textComp;  //COMPONENTE QUE VAI RECEBER O TEXTO
    public static TextMesh textComp_i;  //COMPONENTE QUE VAI RECEBER O TEXTO
    private static TextAsset textAsset;    //COMPONENTE DO TXT
    void Awake()
    {
        textList_i = textList;
        rowsToRead_i = rowsToRead;
        fileName_i = fileName;
        textComp_i = textComp;
        textAsset = Resources.Load("TextFiles/" + fileName_i) as TextAsset;
        TextAssetToList();/* 
        foreach (var item in textList_i)
        {
            print(GetVector3(item));
        } */
        
    }
    public static Vector3 GetVector3(int index)
    {
        char[] delimeters = "(), ".ToCharArray();
        string[] splitedValues = textList_i[index].Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);     //TIRA OS PARNTESES, AS VIRGULAS E ESPAÇOS

        string[] vetor3Str = new string[splitedValues.Length/2];    //CRIA UMA STRING COM O TAMANHO DA METADE DA QUANTDADE DOS VALORES SEPARADOS
        for(int i = 0; i < vetor3Str.Length; i++) vetor3Str[i] = string.Concat(splitedValues[i*2], ",", splitedValues[(i*2)+1]); //JUNTA AOS PARES COM VIRGULA NO MEIO
        
        float[] vetor3Float = new float[vetor3Str.Length];          //CRIA UMA STRING COM O TAMANHO DE UM VECTOR3
        for(int i = 0; i < vetor3Float.Length; i++) vetor3Float[i] = float.Parse(vetor3Str[i]);     //TRANSFORMA DE STRING PRA FLOAT
        return new Vector3(vetor3Float[0], vetor3Float[1], vetor3Float[2]);     //TRANSFORMA DE FLOAT PRA VECTOR3 E RETORNA
    }
    public static Vector3 GetVector3(string text)
    {
        char[] delimeters = "(), \r\n".ToCharArray();
        string[] splitedValues = text.Split(delimeters, System.StringSplitOptions.RemoveEmptyEntries);     //TIRA OS PARNTESES, AS VIRGULAS E ESPAÇOS
        string[] vetor3Str = new string[splitedValues.Length/2];    //CRIA UMA STRING COM O TAMANHO DA METADE DA QUANTDADE DOS VALORES SEPARADOS
        for(int i = 0; i < vetor3Str.Length; i++) vetor3Str[i] = string.Concat(splitedValues[i*2], ",", splitedValues[(i*2)+1]); //JUNTA AOS PARES COM VIRGULA NO MEIO
        
        float[] vetor3Float = new float[vetor3Str.Length];          //CRIA UMA STRING COM O TAMANHO DE UM VECTOR3
        for(int i = 0; i < vetor3Float.Length; i++) vetor3Float[i] = float.Parse(vetor3Str[i]);     //TRANSFORMA DE STRING PRA FLOAT
        if(vetor3Float.Length<3) return Vector2.zero;       //VERIFICA SE O VALOR TERMINA COM 3 ELEMENTOS
        return new Vector3(vetor3Float[0], vetor3Float[1], vetor3Float[2]);     //TRANSFORMA DE FLOAT PRA VECTOR3 E RETORNA
    }
    public static void TextAssetToList()
    {
        textList_i = textAsset.text.Split('\n').ToList();
        
        /* 
        foreach (var item in textList_i)
        {
            textComp_i.text = textComp_i.text+item+'\n';
            print(item);
        } */
    }/* 
    public static string GetTextFile()
    {
        //TextAssetToList();
        return textComp_i.text;
    } */
    
    public static void WriteInText(string text, bool overrideText)
    {
        StreamWriter File = new StreamWriter("Assets/Resources/TextFiles/" + fileName_i+".txt", !overrideText);
        File.WriteLine(text);
        File.Close();
    }
    public static void WriteInText(string text)
    {
        StreamWriter File = new StreamWriter("Assets/Resources/TextFiles/" + fileName_i+".txt", true);
        File.WriteLine(text);
        File.Close();
    }
    public static void WriteInText(Vector3 text)
    {
        StreamWriter File = new StreamWriter("Assets/Resources/TextFiles/" + fileName_i+".txt", true);
        File.WriteLine(text);
        File.Close();
    }
}
