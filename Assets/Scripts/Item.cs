using UnityEngine;

[SerializeField]
public class Item{
    private string id;
    public Item(string id){
        this.id = id;
    }
    public string getID(){
        return id;
    }

    public bool isItem(string name){
        return name.Equals(this.id);
    }
}
