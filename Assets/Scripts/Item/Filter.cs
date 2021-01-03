public enum FilterType{
    id,
    tag
}
public class Filter{

    private string name;
    private FilterType type;
    public Filter(string filter, FilterType type = FilterType.id){
        name = filter;
        this.type = type;
    }
    public string getName(){
        return name;
    }
    public FilterType getType(){
        return type;
    }

    public bool check(Item item){
        if(name.Equals("any")) return true;
        if(name.Equals("none")) return false;
        if(type == FilterType.tag) return item.isTag(name);
        return item.isItem(name);
    }
}
