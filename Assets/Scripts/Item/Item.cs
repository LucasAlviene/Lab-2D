using UnityEngine;

[SerializeField]
public class Item{
    private string id;
    private Tag tag = new Tag();
    public Item(string id, Tag tag = null){
        this.id = id;
        if(tag != null) this.tag = tag;
    }
    public string getID(){
        return id;
    }

    public bool isItem(string name){
        return name.Equals(this.id);
    }
    
    public bool isTag(string name){
        return tag.isTag(name);
    }
}
