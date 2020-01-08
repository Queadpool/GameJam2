using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QQ.Utils;

public class DatabaseManager : Singleton<DatabaseManager>
{
    [SerializeField] private Database _database = null;

    public Database Database { get { return _database; } }
}
