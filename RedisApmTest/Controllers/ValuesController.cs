using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RedisApmTest.Controllers
{
    public class ValuesController : ApiController
    {
        static ConnectionMultiplexer connection;
        public static ConnectionMultiplexer GetRedisCon()
        {
            if ( connection == null)
            {
                var constring = ConfigurationManager.AppSettings["redisCon"];
                if ( constring == null)
                {
                    throw new InvalidOperationException("missing redisCon from config");
                }
                var con  = ConnectionMultiplexer.Connect(constring);
                con.PreserveAsyncOrder = false;
                connection = con;
            }
            return connection;
        }
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            var db = GetRedisCon().GetDatabase();
            var key =(RedisKey) Guid.NewGuid().ToString();
            var res = await db.HashKeysAsync(key);
            
            return new string[] { "value1"
                , "value2" 
                , res == null ? "apm oops" : "correct result",
                 $"result:{res}, size is:{res?.Length},isnull:{res == null}"  };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
