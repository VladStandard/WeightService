// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDirectModels;

namespace DataCore.Sql;

public static partial class SqlUtils
{
	#region Public and private methods

	public static ushort GetPluCount(long scaleId)
	{
		ushort result = 0;
		using SqlConnection con = SqlConnect.GetConnection();
		con.Open();
		using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Plu.GetCount))
		{
			cmd.Connection = con;
			cmd.Parameters.Clear();
			cmd.Parameters.AddWithValue("@SCALE_ID", scaleId);
			using SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				if (reader.Read())
				{
					result = SqlConnect.GetValueAsNotNullable<ushort>(reader, "COUNT");
				}
			}
			reader.Close();
		}
		con.Close();
		return result;
	}

	#endregion
}
