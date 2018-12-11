using UnityEngine;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour {

    Player player;

    public Image image;
    public Text playerName;
    public Text playerJob;

    public void Init(Player player) {
        this.player = player;
        if(player.name != "") {
            image.color = player.appearance.color;
            playerName.text = player.name;
            playerJob.text = player.clazz.title;
        } else {
            image.color = Color.HSVToRGB(0, 0, 0.56F);
            playerName.text = player.username;
            playerJob.text = "";
        }
    }

}
