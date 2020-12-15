using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot{

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

    public Slot(Item item,int amount, Vector2Int position, int size){
        this.item = item;
        this.position = position;
        this.amount = amount;
        this.size = size;
        this.x = position.x;
        this.y = position.y;
    }
    public Slot(Item item,int amount, int x,int y,Vector2Int position, int size){
        this.item = item;
        this.position = position;
        this.amount = amount;
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
        GUI.Label( new Rect(w,h + 40, size , size),""+getAmount());
        GUI.DrawTexture(p,Resources.Load<Texture2D>("Inventory/"+item.getID()));
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
    
    public Item getItem(){
        return item;
    }
    public void removeItem(){
        this.item = null;
        this.amount = 0;
    }
    public bool isItem(string id){
        return id.Equals(item.getID());
    }
    public void setFilter(string filter){
        this.filter = filter;
    }
    public int getAmount(){
        return amount;
    }
    public void addAmount(int amount){
        this.amount += amount;
    }
    public Vector2Int getPosition(){
        return position;
    }
    public Vector2Int getXY(){
        return new Vector2Int(x,y);
    }

    public bool allowItem(){
        if(!itemExists) return false;
        Debug.Log(filter);
        return filter == "any" || item.getID().Equals(filter);
    }
}

public static class slotExtension{
    public static bool addSlot(this Array2D<Slot> listItem, Slot slot, bool ignore = false) {
        if(ignore || slot.allowItem()){
            Vector2Int position = slot.getXY();
            listItem.Add(slot,position.x,position.y);
            return true;
        }
        return false;
    }
} 