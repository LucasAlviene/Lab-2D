using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Inventory, ISizeFixed{

    private int progress = 0;
    private bool isProccess = false;

    private void Awake() {
        listItem = new Array2D<Slot>(columns,rows);

        Slot input = new Slot("input_1",0,0, new Vector2Int(250,57),size);
        listItem.Add(input,0,0);// Input
        Slot input2 = new Slot("input_2",0,1, new Vector2Int(250,114),size);
        input2.setFilter("bow");
        listItem.Add(input2,0,1); // Input 2
        Slot output = new Slot("output",0,2, new Vector2Int(350,175/2 - size/2),size);
        output.setFilter("none");
        listItem.Add(output,0,2); // Output
    }

    public Slot GetSlot(string name){
        return listItem.FindWithName(name);
    }


    private void OnGUI() {
        if(!open) return;
        Rect pos = new Rect(position.x, position.y, width,height);
        GUI.Box(pos, BoxTexture2);
        GUI.Label(pos,progress+"%");
    
        GetSlot("input_1").renderSlot(position,true);
        GetSlot("input_2").renderSlot(position,true);
        GetSlot("output").renderSlot(position,true);

        for(int i=0;i<3;i++){
            listItem.Get(0,i).renderItem(position,true);
        }
    }

    private void Update(){
        Proccess();
    }


    private void Proccess(){
        if(progress == 0 && !isProccess){
            Slot input1 = GetSlot("input_1");
            Slot input2 = GetSlot("input_2");
            string outputid  = "carrot";
            if(input1.itemExists && input2.itemExists){
                Slot output = GetSlot("output");

                if((!output.itemExists || output.isItem(outputid)) && input1.subAmount(4)){
                    if(input1.getAmount() == 0) input1.removeItem();
                    input2.subAmount(1);
                    progress = 0;
                    isProccess = true;
                    StartCoroutine(ProccessItem(outputid,5));
                }
            }
        }
    }

    private IEnumerator ProccessItem(string id, int amount){
        while (progress < 100){
            yield return new WaitForSeconds(0.08f);
            progress++;
        }
        Slot output = GetSlot("output");
        if(output.itemExists){
            output.addAmount(amount);
        }else{
            output.addItem(new Item(id),amount);
        }
        progress = 0;
        isProccess = false;
    }
}
