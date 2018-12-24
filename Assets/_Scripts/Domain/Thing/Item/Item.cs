public abstract class Item : Thing {
    
    public enum Type {
        STAT_MODIFIER,
        MOVEMENT
    }

    public abstract string GetName();
    
}