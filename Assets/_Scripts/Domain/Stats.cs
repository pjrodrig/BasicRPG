using System;

[Serializable]
public class Stats {

    public int hp;
    public int def;
    public int atk;
    public int mag;
    public int spd;

    public Stats() {
        hp = 100;
        def = 1;
        atk = 1;
        mag = 1;
        spd = 1;
    }
    
}