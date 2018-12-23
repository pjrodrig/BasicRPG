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

    public Stats(int atk, int mag, int def, int spd, int hp) {
        this.atk = atk;
        this.mag = mag;
        this.def = def;
        this.spd = spd;
        this.hp = hp;
    }

    public Stats copy() {
        return new Stats(atk, mag, def, spd, hp);
    }
    
}