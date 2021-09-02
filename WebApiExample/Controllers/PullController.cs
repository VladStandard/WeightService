// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NHibernate;
using WebApiExample.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PullController : ControllerBase
    {

        private readonly ILogger<PullController> _logger;
        private readonly ISessionFactory _sessionFactory;
        private ErrorContainer _errors = new ErrorContainer();

        public PullController(ILogger<PullController> logger, ISessionFactory sessionFactory)
        {
            _logger = logger;
            _sessionFactory = sessionFactory;
        }

        // GET: api/<PullController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Not supported. Use POST-method." };
        }


        // POST api/<PullController>
        [HttpPost]
        public IActionResult Post()
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        IDbCommand command = new SqlCommand();
                        command.Connection = session.Connection;

                        transaction.Enlist((System.Data.Common.DbCommand)command);

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "[BRO].[PullReplyMessage]";

                        // Set output parameter
                        var outputHandle = new SqlParameter("@HandleOutput", SqlDbType.UniqueIdentifier);
                        outputHandle.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputHandle);

                        // Set output parameter
                        var outputReceivedMessage = new SqlParameter("@MessageOutput", SqlDbType.Xml);
                        outputReceivedMessage.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputReceivedMessage);

                        // Set a return value
                        var returnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        command.Parameters.Add(returnParameter);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();
                        transaction.Commit();

                        if (outputHandle.Value != DBNull.Value) {
                            return Ok(
                                JsonConvert.SerializeObject(new ResponseItem() {
                                    Id = (Guid)(outputHandle.Value),
                                    Status = StateRequest.ProcessCompleted,
                                    Message = outputReceivedMessage.Value.ToString() })
                            ); 
                        }
                        else
                        {
                            return Ok("Queue is empty");
                        }

                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number==266) 
                    return Ok("Queue is empty");
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
