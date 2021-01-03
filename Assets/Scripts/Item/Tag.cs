using System.Collections.Generic;
public class Tag{

    List<string> list = new List<string>();

    public void add(string name){
        list.Add(name);
    }

    public bool isTag(string key){
        foreach(string name in list){
            if(key.Equals(name)) return true;
        }
        return false;
    }
}
