using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ISlot{

    [SerializeField]
    public bool open = false;
    [SerializeField]
    internal int rows = 5;
    [SerializeField]
    internal int columns = 4;
    [SerializeField]
    internal int width = 0;
    [SerializeField]
    internal int height = 0;
    internal int size = 50;
    [SerializeField]
    public Array2D<Slot> listItem;
    public Vector2Int position;

    public Texture2D BoxTexture2;

    public bool Set(Item item, Vector2Int coord, int amount = 1){
        return Set(item,coord.x,coord.y,amount);
    }
    public bool Set(Item item, int x, int y, int amount = 1){
        Slot slot = listItem.Get(x,y);
        Item current = slot.getItem();
        if(slot.itemExists && current.isItem(item.getID())){
            slot.addAmount(amount);
        }else{
            Slot newSlot = (Slot) slot.Clone();
            newSlot.addItem(item,amount);
            return listItem.addSlot(newSlot);
        }
        return true;
    }

    public Slot Get(int x, int y){
        return listItem.Get(x,y);
    }
}