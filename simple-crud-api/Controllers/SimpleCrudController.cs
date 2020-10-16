using Microsoft.AspNetCore.Mvc;
using simple_crud_api.Config;
using simple_crud_api.Data;
using simple_crud_api.Models;
using simple_crud_api.Repositories;
using System;

namespace simple_crud_api.Controllers
{
    [ApiController]    
    public class SimpleCrudController : ControllerBase
    {        
        public IUserRepository _repositoryMongo;
        public IUserRepository _repositoryMSSql;

        public SimpleCrudController(IMongoDBSettings settings, DataContext context)
        {
            _repositoryMongo = new Repositories.MongoDB.UserRepository(settings);
            _repositoryMSSql = new Repositories.MSSql.UserRepository(context);
        }

        [HttpGet]
        [Route("v1/simpleCrud/{repositoryName}")]
        public ActionResult<object> Get(string repositoryName)
        {
            try
            {
                object result;

                switch (repositoryName)
                {
                    case "mongo":
                        result = _repositoryMongo.GetAll();
                        break;
                    case "mssql":
                        result = _repositoryMSSql.GetAll();
                        break;
                    default:
                        result = null;
                        break;
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpGet]
        [Route("v1/simpleCrud/{repositoryName}/{id}")]
        public ActionResult<object> GetById(string repositoryName, string id)
        {
            try
            {
                object result;

                switch (repositoryName)
                {
                    case "mongo":
                        result = _repositoryMongo.GetById(id);
                        break;
                    case "mssql":
                        result = _repositoryMSSql.GetById(id);
                        break;
                    default:
                        result = null;
                        break;
                }
                                
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpPost]
        [Route("v1/simpleCrud/{repositoryName}")]
        public ActionResult<object> Post(string repositoryName, [FromBody] User obj)
        {
            try
            {
                object result;

                switch (repositoryName)
                {
                    case "mongo":
                        result = _repositoryMongo.Create(obj);
                        break;
                    case "mssql":
                        result = _repositoryMSSql.Create(obj);
                        break;
                    default:
                        result = null;
                        break;
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpPut]
        [Route("v1/simpleCrud/{repositoryName}/{id}")]
        public ActionResult<object> Put(string repositoryName, string id, [FromBody] User obj)
        {
            try
            {
                obj.Id = id;

                object result;

                switch (repositoryName)
                {
                    case "mongo":
                        result = _repositoryMongo.Update(id, obj);
                        break;
                    case "mssql":
                        result = _repositoryMSSql.Update(id, obj);
                        break;
                    default:
                        result = null;
                        break;
                }
                                
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpDelete]
        [Route("v1/simpleCrud/{repositoryName}/{id}")]
        public ActionResult<object> Delete(string repositoryName, string id)
        {
            try
            {
                object result;

                switch (repositoryName)
                {
                    case "mongo":
                        result = _repositoryMongo.Delete(id);
                        break;
                    case "mssql":
                        result = _repositoryMSSql.Delete(id);
                        break;
                    default:
                        result = null;
                        break;
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }
    }
}
