using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Inventory, ISizeFixed{


    private void Awake() {
        listItem = new Array2D<Slot>(columns,rows);

        Slot input = new Slot(null,0, new Vector2Int(250,57),size);
        listItem.Add(input,0,0);// Input
        Slot input2 = new Slot(null,0, new Vector2Int(250,114),size);
        input2.setFilter("bow");
        listItem.Add(input2,0,1); // Input 2
        Slot output = new Slot(null,0, new Vector2Int(350,175/2 - size/2),size);
        listItem.Add(output,0,2); // Output
    }


    private void OnGUI() {
        if(!open) return;
        Rect pos = new Rect(position.x, position.y, width,height);
        GUI.Box(pos, BoxTexture2);
    
        listItem.Get(0,0).renderSlot(position,true);
        listItem.Get(0,1).renderSlot(position,true);
        listItem.Get(0,2).renderSlot(position,true);

        for(int i=0;i<3;i++){
            listItem.Get(0,i).renderItem(position,true);
        }
    }
}
