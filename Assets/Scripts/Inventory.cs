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

    public bool Add(Item item, int x, int y,int amount = 1){
        return Add(item,x,y,new Vector2Int(x,y),amount);
    }
    public bool Add(Item item, Vector2Int coord, Vector2Int position,int amount = 1){
        return Add(item,coord.x,coord.y,position,amount);
    }    
    public bool Add(Item item, int x, int y, Vector2Int position,int amount = 1){
        Slot slot = listItem.Get(x,y);
        Item current = slot.getItem();
        if(slot.itemExists && current.isItem(item.getID())){
            slot.addAmount(amount);
        }else{
            Slot newslot = new Slot(item,amount,x,y,position,size);
            return listItem.addSlot(newslot);
        }
        return true;
    } 
    public Slot Get(int x, int y){
        return listItem.Get(x,y);
    }
    public void Remove(int x,int y){
        listItem.addSlot(new Slot(null,0,new Vector2Int(x,y),size));
    }
    public void Remove(Vector2Int coord,Vector2Int position){
        /*
        Slot slot = listItem.Get(coord.x,coord.y);
        Slot novo = slot;
        novo.removeItem();
        //new Slot(null,0,coord.x,coord.y,position,size)*/
        listItem.addSlot(new Slot(null,0,coord.x,coord.y,position,size),true);
    }
}
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    [SerializeField]
    internal int rows = 5;
    [SerializeField]
    internal int columns = 4;
    [SerializeField]
    public Array2D<Item> listItem;
    public Texture2D BoxTexture;
    public Texture2D BoxTexture2;

    private Vector2 margin = new Vector2(50,50);

    private Item GameController.currentHand{get;set;}

    private void Awake() {
        listItem = new Array2D<Item>(columns,rows);
    }

    private void Start(){

    }

    public void Add(Item item, int x, int y){
        Item current = listItem.Get(x,y);
        if(current != null && current.isItem(item.getName())){
            current.addAmount(item.getAmount());
        }else{
            listItem.Add(item,x,y);
        }
    } 

    public int toolbarInt = 0;
    public string[] toolbarStrings = new string[] {"Toolbar1", "Toolbar2", "Toolbar3"};

    private void OnGUI() {
        Rect pos = new Rect(50, 50, 50 * rows,50 * columns);
        GUI.Box(pos, BoxTexture2);
       
       // GUILayout.BeginArea(pos);

       for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                GUI.Box(new Rect(50 + (50 * i),50 + (50 * j), 50 , 50),""); //i+" x "+j+" \n "+(i + j * rows
            }
       }
        for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                Item item = listItem.Get(i,j);
                int w = 50 + (50 * i);
                int h = 50 + (50 * j);

                Rect p = new Rect(w,h, 50 , 50);
                if(item != null){
                    GUI.Label( new Rect(w,h + 40, 50 , 50),""+item.getAmount());
                    GUI.DrawTexture(p,Resources.Load<Texture2D>("Inventory/"+item.getName()));
                }
            }
        }
        if(GameController.currentHand != null){
            string name = GameController.currentHand.getName();
            Vector2 mousePosition = new Vector2(Input.mousePosition.x - 20,Screen.height - Input.mousePosition.y - 20);
            GUI.Label( new Rect(mousePosition.x,mousePosition.y + 40, 50 , 50),""+GameController.currentHand.getAmount());
            GUI.DrawTexture(new Rect(mousePosition.x,mousePosition.y,50,50),Resources.Load<Texture2D>("Inventory/"+name));
        }
        toolbarInt = GUI.Toolbar(new Rect(25, 25, 250, 30), toolbarInt, toolbarStrings);
    }

    private void Update() {
        if(Input.GetKeyDown("1"))Add(new Item("book"),0,4);
        if(Input.GetKeyDown("2"))Add(new Item("carrot"),1,4);
        if(Input.GetKeyDown("3"))Add(new Item("bread"),2,4);
        if(Input.GetKeyDown("4"))Add(new Item("bow"),3,4);
        
        if(Input.GetKeyDown(KeyCode.Mouse0)){
        
            Vector2Int position = FindItem();
            Item item = listItem.Get(position.x,position.y);
            
            // Não tem item no Slot && Tem item na mão
            if(item == null && GameController.currentHand != null){
                listItem.Add(GameController.currentHand,position.x,position.y);
                GameController.currentHand = null;
            }else
            // Tem item no Slot && Não tem item na mão
            if(item != null && GameController.currentHand == null){
                GameController.currentHand = item;
                listItem.Remove(position.x,position.y);
            }else
            // Tem item no slot && Tem item na mão
            if(item != null && GameController.currentHand != null){
                if(item.isItem(GameController.currentHand.getName())){
                    item.addAmount(GameController.currentHand.getAmount());
                    GameController.currentHand = null;
                }else{
                    Item temp = GameController.currentHand;
                    GameController.currentHand = item;
                    listItem.Add(temp,position.x,position.y);
                }
            }

        }
    }


    private Vector2Int FindItem(){
        Vector2 positionCurrent = new Vector2(Input.mousePosition.x - 50,Screen.height - Input.mousePosition.y - 50);
        for(int i=0;i<rows;i++){
            for(int j=0;j<columns;j++){
                
                Vector2 lastSize = new Vector2(50 + 50 * (i - 1),50 + 50 * (j - 1));
                Vector2 currentSize = new Vector2(50 + 50 * i,50 + 50 * j);

                if(positionCurrent.x >= lastSize.x && positionCurrent.x < currentSize.x){
                    if(positionCurrent.y >= lastSize.y && positionCurrent.y < currentSize.y){
                        return new Vector2Int(i,j);
                    }
                }
            }
        }
        return new Vector2Int(0,0);
    }
}

*/