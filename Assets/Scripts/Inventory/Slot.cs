using System;
using System.Collections.Generic;
using UnityEngine;

public class Slot : ICloneable{

    private string name;
    private int size;
    private Vector2Int position; // Posição na tela
    private int x; // Posição x do Array2D
    private int y;// Posição y do Array2D
    private Item item {get;set;}
    private int amount;
    public bool itemExists{
        get{
            return item != null;
        }
    }
    private string filter = "any";

    public Slot(string name,int x,int y, int size){
        this.name = name;
        this.position = new Vector2Int(x,y);
        this.size = size;
        this.x = x;
        this.y = y;
    }
    public Slot(String name,int x,int y,Vector2Int position, int size){
        this.name = name;
        this.item = item;
        this.position = position;
        this.size = size;
        this.x = x;
        this.y = y;
    }
    
    public void renderSlot(Vector2 pos, bool isFixed = false){
        int x = position.x;
        int y = position.y;
        if(!isFixed){
            x *= size;
            y *= size;
        }
        GUI.Box(new Rect(pos.x + x,pos.y + y, size , size),"");
    }

    public void renderItem(Vector2 pos,bool isFixed = false){
        if(!itemExists) return;
        int x = position.x;
        int y = position.y;
        if(!isFixed){
            x *= size;
            y *= size;
        }

        float w = pos.x + x;
        float h = pos.y + y;
        
        Rect p = new Rect(w,h, size , size);
        GUI.DrawTexture(p,Resources.Load<Texture2D>("Inventory/"+item.getID()));
        GUI.Label( new Rect(w,h + 40, size , size),""+getAmount());
    }

    public bool check(Vector2 current,Vector2 pos,bool isFixed = false){
        int x = position.x;
        int y = position.y;
        if(!isFixed){
            x *= size;
            y *= size;
        }
        if(current.x >= pos.x + x && current.x < pos.x + x + size){
            if(current.y >= pos.y + y && current.y < pos.y + y + size){
                return true;
            }
        }
        return false;
    }
    
    public bool isName(string name){
        return this.name.Equals(name);
    }

    public Item getItem(){
        return item;
    }
    public void removeItem(){
        this.item = null;
        this.amount = 0;
    }
    public bool isItem(string id){
        return item.isItem(id);
    }
    public void addItem(Item item, int amount){
        this.item = item;
        this.amount = amount;
    }
    public void setFilter(string filter){
        this.filter = filter;
    }
    public string getFilter(){
        return filter;
    }
    public int getAmount(){
        return amount;
    }

    public bool subAmount(int amount, bool removeItem = true){
        if(amount <= this.amount){
            this.amount -= amount;
            if(this.amount == 0) item = null;
            return true;
        }
        return false;
    }
    public void addAmount(int amount){
        this.amount += amount;
    }
    public Vector2Int getPosition(){
        return position;
    }
    public void setPosition(Vector2Int position){
        this.position = position;
    }
    public Vector2Int getXY(){
        return new Vector2Int(x,y);
    }

    public bool allowItem(){
        if(!itemExists) return false;
        return filter == "any" || item.getID().Equals(filter);
    }

    #region ICloneable Members

    public object Clone(){
        return this.MemberwiseClone();
    }

    #endregion
}

public static class slotExtension{
    public static bool addSlot(this Array2D<Slot> listItem, Slot slot, bool ignore = false) {
        if(ignore || slot.allowItem()){
            Vector2Int coord = slot.getXY();
            listItem.Add(slot,coord.x,coord.y);
            return true;
        }
        return false;
    }

    public static Slot FindWithName(this Array2D<Slot> listItem, string name){
        for(int i=0;i<listItem.Size();i++){
            Slot slot = listItem.Get(i);
            if(slot != null && slot.isName(name)){
                return slot;
            }
        }
        return null;
    }
} 