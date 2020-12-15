using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Inventory{

    private void Awake() {
        listItem = new Array2D<Slot>(columns,rows);
        for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                listItem.addSlot(new Slot(null,0, new Vector2Int(i,j),size),true);
            }
        }
    }

    private void OnGUI() {
        if(!open) return;
        Rect pos = new Rect(position.x, position.y, size * rows,size * columns);
        GUI.Box(pos, BoxTexture2);
       
       for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                listItem.Get(i,j).renderSlot(position);
            }
       }
        for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                listItem.Get(i,j).renderItem(position);
            }
       } 
    }


    
    private void Update() {
        if(!open) return;
        if(Input.GetKeyDown("1"))Add(new Item("book"),0,0);
        if(Input.GetKeyDown("2"))Add(new Item("carrot"),1,0);
        if(Input.GetKeyDown("3"))Add(new Item("bread"),2,0);
        if(Input.GetKeyDown("4"))Add(new Item("bow"),3,0);
    }
}
