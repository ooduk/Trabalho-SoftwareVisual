using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/time")]

    public class TimeController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public TimeController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] Time time)
        {
            try
            {
                _ctx.Times.Add(time);
                _ctx.SaveChanges();
                return Created("", time);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {
            List<Time> times = _ctx.Times.ToList();
            return times.Count == 0 ? NotFound() : Ok(times);
        }

        [HttpGet, Route("consultar/{id}")]
        public IActionResult Consultar([FromRoute] int id)
        {
            try
            {
                foreach (Time TimeCadastrado in _ctx.Times.ToList())
                {
                    if (TimeCadastrado.TimeId == id)
                    {
                        return Ok(TimeCadastrado);
                    }
                }

                return NotFound();
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut, Route("atualizar/{id}")]
        public IActionResult Atualizar([FromBody] Time time, [FromRoute] int id)
        {
            try
            {

                Time? TimeEncontrado = _ctx.Times.FirstOrDefault(x => x.TimeId == id);

                if (TimeEncontrado != null)
                {
                    TimeEncontrado.Nome = time.Nome;
                    _ctx.Times.Update(TimeEncontrado);
                    _ctx.SaveChanges();
                    return Ok(time);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete, Route("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {

            try
            {
                Time? time = _ctx.Times.Find(id);

                if (time != null)
                {
                    _ctx.Times.Remove(time);
                    _ctx.SaveChanges();
                    return Ok("Time Deletado!");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
