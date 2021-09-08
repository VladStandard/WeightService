using System;
using System.Data.SqlClient;
using System.Text;
using System.Xml.Linq;

namespace TokenWrite
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid tokenSalt = Guid.NewGuid();
            string connectionString = args[0];

            string filePathFromSaveDialog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
            XDocument document = new XDocument();

            XElement root = new XElement("root");
            root.Add (
                new XElement("ID", tokenSalt),
                new XElement("EncryptConnectionString", new XCData( EncryptDecryptUtil.Encrypt(connectionString)))
                );
            document.Add(root);
            document.Save(filePathFromSaveDialog);

            string name = Environment.MachineName;
            string id = tokenSalt.ToString();
            string sqlExpression = $"INSERT INTO dbo.HOSTS (ID, NAME) VALUES ('{id}', '{name}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int n = command.ExecuteNonQuery();
            }

            Console.WriteLine();
            Console.WriteLine("GetFolderPath: {0}", filePathFromSaveDialog);

        }
    }
}
