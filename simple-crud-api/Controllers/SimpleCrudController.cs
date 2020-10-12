using Microsoft.AspNetCore.Mvc;
using simple_crud_api.Config;
using simple_crud_api.Models;
using simple_crud_api.Repositories.MongoDB;
using System;

namespace simple_crud_api.Controllers
{
    [ApiController]    
    public class SimpleCrudController : ControllerBase
    {        
        public IUserRepository _repository;

        public SimpleCrudController(IMongoDBSettings settings)
        {
            _repository = new UserRepository(settings);
        }

        [HttpGet]
        [Route("v1/simpleCrud")]
        public ActionResult<object> Get()
        {
            try
            {
                var result = _repository.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpGet]
        [Route("v1/simpleCrud/{id}")]
        public ActionResult<object> GetById(string id)
        {
            try
            {
                var result = _repository.GetById(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpPost]
        [Route("v1/simpleCrud")]
        public ActionResult<object> Post([FromBody] User obj)
        {
            try
            {
                var result = _repository.Post(obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpPut]
        [Route("v1/simpleCrud/{id}")]
        public ActionResult<object> Put(string id, [FromBody] User obj)
        {
            try
            {
                // aqui o ideal seria ler as propriedades enviadas no request e atualizar apenas as que se alteraram... TODO
                obj.Id = id;

                var result = _repository.Update(id, obj);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }

        [HttpDelete]
        [Route("v1/simpleCrud/{id}")]
        public ActionResult<object> Delete(string id)
        {
            try
            {
                var result = _repository.Delete(id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Problem(title: e.Message, detail: e.StackTrace);
            }
        }
    }
}
