using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour{

    public static Slot currentHand{get;set;}

    public static Inventory[] InventoryOpen() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Inventory");    
        List<Inventory> novo = new List<Inventory>();
        foreach(GameObject ob in objects){
            Inventory inv = ob.GetComponent<Inventory>();
            if((inv as ISlot) != null && inv.open){
                novo.Add(inv);
            }
        }
        return novo.ToArray();
    }

    public static Inventory FindInventory(){
        Inventory[] opens = GameController.InventoryOpen();
        
        Vector2 current = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);
        foreach(Inventory inv in opens){
            //Vector2 current = mousePosition;// - new Vector2(inv.size*2,inv.size*2);

            int width = inv.rows * inv.size;
            int height = inv.columns * inv.size;

            ISizeFixed isFixed = inv as ISizeFixed;
            if(isFixed != null){
                width = inv.width;
                height = inv.height;
            }

            if(current.x >= inv.position.x && current.x < inv.position.x + width){
                if(current.y >= inv.position.y && current.y < inv.position.y + height){
                    return inv;
                }
            }
        }

        return null;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Inventory inv = GameController.FindInventory();
            if(inv != null){
                Vector2Int coord = FindItem(inv); // Posição no Array2D
                Slot slot = inv.Get(coord.x,coord.y);
                if(slot != null){
                    Item item = slot.getItem();
                    Vector2Int position = slot.getPosition();

                    Slot current = GameController.currentHand;
                    // Não tem item no Slot && Tem  item na mão
                    if(item == null && current != null){
                        if(inv.Set(current.getItem(),coord,current.getAmount())){
                            GameController.currentHand = null;
                        }
                    }else
                    // Tem item no Slot && Não tem item na mão
                    if(slot.itemExists && current == null){
                        GameController.currentHand = (Slot) slot.Clone();
                        slot.removeItem();
                    }else
                    // Tem item no slot && Tem item na mão
                    if(slot.itemExists && current != null){
                        if(item.isItem(current.getItem().getID())){
                            slot.addAmount(current.getAmount());
                            GameController.currentHand = null;
                        }else{
                            if(inv.Set(current.getItem(),coord,current.getAmount())){
                                GameController.currentHand = slot;
                            }
                        }
                    }
                }
            }
        }
    }

    private Vector2Int FindItem(Inventory inv){
        Vector2 mousePosition = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y);
        Slot slot = null;
        bool isFixed = (inv as ISizeFixed) != null;
        for(int i=0;i<inv.rows;i++){
            for(int j=0;j<inv.columns;j++){
                slot = inv.Get(i,j);
                if(slot.check(mousePosition,inv.position,isFixed)){
                    return new Vector2Int(i,j);
                }
            }
        }
        return Vector2Int.zero;
    }

    private void OnGUI() {
        GUI.depth = -1;
        if(GameController.currentHand != null){
            Item item = GameController.currentHand.getItem();
            if(item != null){
                string name = item.getID();
                Vector2 mousePosition = new Vector2(Input.mousePosition.x - 20,Screen.height - Input.mousePosition.y - 20);
                GUI.Label( new Rect(mousePosition.x,mousePosition.y + 40, 50 , 50),""+GameController.currentHand.getAmount());
                GUI.DrawTexture(new Rect(mousePosition.x,mousePosition.y,50,50),Resources.Load<Texture2D>("Inventory/"+name));
            }
        } 
    }
/*
    private Vector2Int FindItem(Inventory inv){
        Vector2 positionCurrent = new Vector2(Input.mousePosition.x - inv.size,Screen.height - Input.mousePosition.y - inv.size);
        for(int i=0;i<inv.rows;i++){
            for(int j=0;j<inv.columns;j++){
                
                Vector2 lastSize = new Vector2(inv.position.x + inv.size * (i - 1),inv.position.y + inv.size * (j - 1));
                Vector2 currentSize = new Vector2(inv.position.x + inv.size * i,inv.position.y + inv.size * j);

                if(positionCurrent.x >= lastSize.x && positionCurrent.x < currentSize.x){
                    if(positionCurrent.y >= lastSize.y && positionCurrent.y < currentSize.y){
                        return new Vector2Int(i,j);
                    }
                }
            }
        }
        return new Vector2Int(0,0);
    }*/
}