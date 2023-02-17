// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace terra.Common;

public static class SqlHelper
{
    #region Public properties

    public static SqlConnection GetConnection(string connectionString)
    {
        SqlConnection connection = new();
        try
        {
            connection.ConnectionString = connectionString;
            connection.Open();
        }
        catch (Exception)
        {
            throw;
        }
        return connection;
    }

    public static SqlConnection GetConnection()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;
        SqlConnection connection = new();
        try
        {
            connection.ConnectionString = connectionString;
            connection.Open();
        }
        catch (Exception)
        {
            throw;
        }
        return connection;
    }


    public static SqlCommand GetCommand(string commandText, SqlConnection conn, List<SqlParameter> sqlParameters = null, int commandTimeout = 30)
    {
        SqlCommand command = new();
        try
        {
            command.Connection = conn;
            command.CommandTimeout = commandTimeout;
            command.CommandText = commandText;

            command.Parameters.Clear();
            if (sqlParameters != null)
            {
                foreach (SqlParameter sqlParameter in sqlParameters)
                {
                    command.Parameters.Add(sqlParameter);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return command;
    }

    public static SqlCommand GetCommand(string commandText, SqlConnection conn, List<SqlParameter> sqlParameters = null)
    {
        int commandTimeout = int.Parse(ConfigurationManager.AppSettings["dbCommandTimeout"].ToString());

        SqlCommand command = new();
        try
        {
            command.Connection = conn;
            command.CommandTimeout = commandTimeout;
            command.CommandText = commandText;

            command.Parameters.Clear();
            if (sqlParameters != null)
            {
                foreach (SqlParameter sqlParameter in sqlParameters)
                {
                    command.Parameters.Add(sqlParameter);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return command;
    }

    #endregion
}