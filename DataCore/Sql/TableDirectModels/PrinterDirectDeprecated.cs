//        public void Load()
//        {
//            using (SqlConnection con = SqlConnectFactory.GetConnection())
//            {
//                con.Open();
//                string query = @"
//SELECT x.Id,x.Name,x.Ip,x.Port,x.Password,x.Mac,y.Name as PrinterType
//FROM [db_scales].[ZebraPrinter] x
//INNER JOIN[db_scales].[ZebraPrinterType] y
//ON x.[PrinterTypeId] = y.Id
//WHERE x.[Id] = @ID;
//                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
//                using (SqlCommand cmd = new(query))
//                {
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ID", Id);
//                    using SqlDataReader reader = cmd.ExecuteReader();
//                    if (reader.HasRows)
//                    {
//                        while (reader.Read())
//                        {
//                            Ip = SqlConnectFactory.GetValueAsString(reader, "Ip");
//                            Port = SqlConnectFactory.GetValue<short>(reader, "Port");
//                            Mac = SqlConnectFactory.GetValueAsString(reader, "Mac");
//                            Name = SqlConnectFactory.GetValueAsString(reader, "Name");
//                            Password = SqlConnectFactory.GetValueAsString(reader, "Password");
//                            PrinterType = SqlConnectFactory.GetValueAsString(reader, "PrinterType");
//                        }
//                    }
//                    reader.Close();
//                }
//                con.Close();
//            }

//            using (SqlConnection con = SqlConnectFactory.GetConnection())
//            {
//                con.Open();
//                string query = @"
//select [Name],MAX([ImageData]) [ImageData] 
//from [db_scales].[GetPrinterResources] (@ID,@Type)
//group by [Name]
//                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
//                using (SqlCommand cmd = new(query))
//                {
//                    Logo.Clear();
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ID", Id);
//                    cmd.Parameters.AddWithValue("@Type", "GRF");
//                    using SqlDataReader reader = cmd.ExecuteReader();
//                    if (reader.HasRows)
//                    {
//                        while (reader.Read())
//                        {
//                            Logo.Add(reader.GetString(0), reader.GetString(1));
//                        }
//                    }
//                    reader.Close();
//                }
//                con.Close();
//            }

//            using (SqlConnection con = SqlConnectFactory.GetConnection())
//            {
//                con.Open();
//                string query = @"
//select [Name], [ImageData] 
//from [db_scales].[GetPrinterResources] (@ID,@Type)
//                    ".TrimStart('\r', ' ', '\n', '\t').TrimEnd('\r', ' ', '\n', '\t');
//                using (SqlCommand cmd = new(query))
//                {
//                    Fonts.Clear();
//                    cmd.Connection = con;
//                    cmd.Parameters.AddWithValue("@ID", Id);
//                    cmd.Parameters.AddWithValue("@Type", "TTF");
//                    using SqlDataReader reader = cmd.ExecuteReader();
//                    if (reader.HasRows)
//                    {
//                        while (reader.Read())
//                        {
//                            Fonts.Add(reader.GetString(0), reader.GetString(1));
//                        }
//                    }
//                    reader.Close();
//                }
//                con.Close();
//            }
//        }
//    }
//}