using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDHighscoreList : UI_List<User>
{
    public User[] users;

    IEnumerator Start()
    {
        yield return BackendAPI.Get($"api/user/highscores/7", (req, data) =>
        {
            bool ok = data.Value<bool>("ok");
            if (ok)
            {
                List<User> topUsers = data["users"].ToObject<List<User>>();
                users = topUsers.ToArray();
            }

        });
    }

    void Update()
    {
        UpdateList(new List<User>(users));
    }
}
