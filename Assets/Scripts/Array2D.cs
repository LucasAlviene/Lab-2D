using System;
using UnityEngine;

[SerializeField]
public class Array2D<T>{

    private int columns;
    private int rows;
    [SerializeField]
    private T[] list; // Não tenho cratividade para nomes

    /// <summary>
    /// Número de Colunas e Linhas da Matriz.
    /// </summary>
    public Array2D(int columns, int rows){
        list = new T[columns * rows];
        this.columns = columns; // X
        this.rows = rows; // Y
    }

    /// <summary>
    /// Retornar a matriz em forma de array
    /// </summary>
    public T[] Get(){
        return list;
    }

    /// <summary>
    /// Retornar o item conforme a posição da coluna e linha
    /// </summary>
    public T Get(int x,int y){
        return list[x + y * rows];
    }

    /// <summary>
    /// Adicionar Matriz com a posição da coluna e linha
    /// </summary>
    public void Add(T item, int x,int y){
        if(columns > y && rows > x){
            list[x + y * rows] = item;
        }else{
            Debug.LogWarning("O número máximo é: "+(columns - 1)+" x "+(rows - 1)+" - "+x+" x "+y);
        }
    }

    public void Remove(int x,int y){
        int index = x + y * rows; // Calcula o Index

        int length = columns * rows; // Calcula o Tamanho
        T[] dest = new T[length];

        if(index > 0)
            Array.Copy(list, 0, dest, 0, index); // Copia do 0 até o index

        if(index < length-1)
            Array.Copy(list, index + 1, dest, index+1, length - index  - 1); // Copia o index+1 até length - index - 1
        
        list = dest; // Substitui
    }
}
