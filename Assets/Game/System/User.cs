[System.Serializable]
public class User
{
    public int id;
    public string username;
    public int score;

    public User()
    { }

    public User(string username)
    {
        this.username = username;
    }


    public User(int id, string username, int score)
    {
        this.id = id;
        this.username = username;
        this.score = score;
    }
}

[System.Serializable]
public class ApiResponse<T>
{
    public bool ok;
    public T data;
}