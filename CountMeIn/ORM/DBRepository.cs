using System;

using System.IO;
using SQLite;


namespace CountMeIn.ORM
{
    public class DBRepository
    {
        public string CreateDB()
        {
            var output = "";
            output += "Create database if it does not exist";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            var db = new SQLiteConnection(dbPath);
            output += "\nDatabase Created...";
            return output;
        }
    }
}