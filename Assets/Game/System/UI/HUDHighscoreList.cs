using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HUDHighscoreList : UI_List<User>
{
    public User[] users;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return BackendAPI.Get($"api/user/highscores/7", (req, data) =>
        {
            Debug.Log(data.ToString());

            bool ok = data.Value<bool>("ok");
            if (ok)
            {
                List<User> topUsers = data["users"].ToObject<List<User>>();
                Debug.Log(topUsers.ToString());
                users = topUsers.ToArray();
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        UpdateList(new List<User>(users));
    }
}
