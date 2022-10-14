// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate;
using WebApiExample.Common;

namespace WebApiExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PushController : ControllerBase
{

	private readonly ISessionFactory _sessionFactory;
	
	public PushController(ISessionFactory sessionFactory)
	{
		_sessionFactory = sessionFactory;
	}


	// GET: api/Push
	[HttpGet]
	public IEnumerable<string> Get()
	{
		return new string[] { "Not supported. Use POST-method." };
	}

	[HttpPost]
	public IActionResult Post([FromBody] JsonElement json)
	{
		var value = System.Text.Json.JsonSerializer.Serialize(json);
		try
		{
			using (ISession session = _sessionFactory.OpenSession())
			{
				using (ITransaction transaction = session.BeginTransaction())
				{
					IDbCommand command = new SqlCommand();
					command.Connection = session.Connection;

					transaction.Enlist((System.Data.Common.DbCommand)command);

					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = "[BRO].[PushSendingMessage]";

					// Set input parameters
					SqlParameter parm = new SqlParameter("@RequestMessage", SqlDbType.Xml);
					parm.Value = value;
					command.Parameters.Add(parm);

					// Set output parameter
					SqlParameter outputParameter = new SqlParameter("@InitDlgHandle", SqlDbType.UniqueIdentifier);
					outputParameter.Direction = ParameterDirection.Output;
					command.Parameters.Add(outputParameter);

					// Set a return value
					SqlParameter returnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
					returnParameter.Direction = ParameterDirection.ReturnValue;
					command.Parameters.Add(returnParameter);

					// Execute the stored procedure
					command.ExecuteNonQuery();
					transaction.Commit();

					//return Ok(outputParameter);
					return Ok(
						JsonConvert.SerializeObject(new ResponseItem() { Id = (Guid)(outputParameter.Value) , Status = StateRequest.InQueue })
					);


				}
			}
		}
		catch (SqlException ex)
		{
			if (ex.Number == 266)
				return Ok("Queue is empty");
			return BadRequest(ex.Message);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}



	}

}