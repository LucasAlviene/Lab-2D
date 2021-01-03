using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : Inventory{

    private void Awake() {
        listItem = new Array2D<Slot>(columns,rows);
        for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                listItem.addSlot(new Slot(i+"x"+j,i,j,size),true);
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
        Tag tagCoal = new Tag();
        tagCoal.add("coal");
        if(Input.GetKeyDown("1"))Set(new Item("book", tagCoal),0,0,4);
        if(Input.GetKeyDown("2"))Set(new Item("carrot"),1,0);
        if(Input.GetKeyDown("3"))Set(new Item("bread"),2,0);
        if(Input.GetKeyDown("4"))Set(new Item("bow",tagCoal),3,0,5);
    }
}
