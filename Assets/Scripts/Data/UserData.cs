using System;

[Serializable]
public class UserData
{
    public string userName;
    public int score = 0;

    public UserData(string userName)
    {
        this.userName = userName;
    }
}
