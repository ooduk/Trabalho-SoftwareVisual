using ApiBackend.Data;
using ApiBackend.Models;


namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/confronto")]

    public class ConfrontoController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public ConfrontoController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] Confronto confronto)
        {
            try
            {
                confronto.CampeonatoId = _ctx.Campeonatos.Find(confronto.CampeonatoId);
                confronto.TimeCasaId = _ctx.Times.Find(confronto.TimeCasaId);
                confronto.TimeForaId = _ctx.Times.Find(confronto.TimeForaId);
                _ctx.Times.Add(confronto);
                _ctx.SaveChanges();
                return Created("", confronto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {
            List<Confronto> confrontos = _ctx.Confrontos.ToList();
            return confrontos.Count == 0 ? NotFound() : Ok(confrontos);
        }

        [HttpGet, Route("consultar/{id}")]
        public IActionResult Consultar([FromRoute] int id)
        {
            try
            {
                foreach (Confronto ConfrontoCadastrado in _ctx.Confrontos.ToList())
                {
                    if (ConfrontoCadastrado.ConfrontoIdId == id)
                    {
                        return Ok(ConfrontoCadastrado);
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
        public IActionResult Atualizar([FromBody] Confronto confronto, [FromRoute] int id)
        {
            try
            {

                Confronto? ConfrontoEncontrado = _ctx.Confrontos.FirstOrDefault(x => x.ConfrontoId == id);

                if (ConfrontoEncontrado != null)
                {
                    ConfrontoEncontrado.Nome = Confronto.Nome;
                    _ctx.Confrontos.Update(ConfrontoEncontrado);
                    _ctx.SaveChanges();
                    return Ok(Confronto);
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
                Confronto? confronto = _ctx.Confrontos.Find(id);

                if (confronto != null)
                {
                    _ctx.Confrontos.Remove(confronto);
                    _ctx.SaveChanges();
                    return Ok("Confronto Deletado!");
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
