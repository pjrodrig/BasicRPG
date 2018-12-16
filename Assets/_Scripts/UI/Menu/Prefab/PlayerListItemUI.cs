using UnityEngine;
using UnityEngine.UI;

public class PlayerListItemUI : MonoBehaviour {

    Player player;

    public Image image;
    public Text playerName;
    public Text playerJob;

    public void Init(Player player) {
        this.player = player;
        if(player.isInitialized) {
            image.color = GetColor(player.appearance.color);
            playerName.text = player.name;
            playerJob.text = player.clazz.title;
        } else {
            image.color = Color.HSVToRGB(0, 0, 0.56F);
            playerName.text = player.username;
            playerJob.text = "";
        }
    }

    Color GetColor(Appearance.Color color) {
        switch(color) {
            case Appearance.Color.RED:
                return Color.red;
            case Appearance.Color.BLUE:
                return Color.blue;
            case Appearance.Color.GREEN:
                return Color.green;
            case Appearance.Color.YELLOW:
                return Color.yellow;
            case Appearance.Color.PURPLE:
                return Color.magenta;
            default:
                return Color.HSVToRGB(0, 0, 0.56F);
        }
    }

}
