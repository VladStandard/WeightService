// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableDirectModels;

[Serializable]
public class TaskTypeDirect : SerializeModel, ISerializable
{
    #region Public and private fields, properties, constructor

    public Guid Uid { get; set; }
    public string Name { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public TaskTypeDirect()
    {
        Uid = Guid.Empty;
        Name = string.Empty;
    }

    public TaskTypeDirect(Guid uid, string name)
    {
        Uid = uid;
        Name = name;
    }

    #endregion

    #region Public and private methods

    public void Save(string name)
    {
        using SqlConnection con = SqlConnect.GetConnection();
        con.Open();
        using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.AddTaskType))
        {
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@name", name);
            cmd.ExecuteNonQuery();
        }
        con.Close();
    }

    public void Save()
    {
        Save(Name);
    }

    #endregion
}
