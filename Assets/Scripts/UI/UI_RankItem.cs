using Frame;
using UnityEngine;
using UnityEngine.UI;

[Pool]
public class UI_RankItem : MonoBehaviour
{
    [SerializeField] private Text rankNum_Text;
    [SerializeField] private Text userName_Text;
    [SerializeField] private Text score_Text;
    public void Init(UserData userData,int rankNum)
    {
        rankNum_Text = this.GetUI<Text>("RankNum_Text");
        userName_Text = this.GetUI<Text>("UserName_Text");
        score_Text = this.GetUI<Text>("Score/Score_Text");

        rankNum_Text.text = rankNum.ToString();
        userName_Text.text = userData.userName;
        score_Text.text = userData.score.ToString();
    }

    public void Destroy()
    {
        this.GameObjectPushPool();
    }
}
