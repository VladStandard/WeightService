using System;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;

namespace TokenWriteRead
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePathFromSaveDialog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
            XDocument doc = XDocument.Load(filePathFromSaveDialog);

            var Id = doc.Root.Elements("ID").First().Value;
            var EncryptConnectionString = doc.Root.Elements("EncryptConnectionString").First().Value;
            string connectionString = EncryptDecryptUtil.Decrypt(EncryptConnectionString);
            string sqlExpression = $"SELECT ID,NAME,MAC,IP FROM dbo.HOSTS WHERE ID='{Id}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        object id = reader.GetValue(0);
                        object name = reader.GetValue(1);
                        Console.WriteLine("{0} \t{1}", id, name);
                    }
                }
                reader.Close();
            }
        }
    }
}
