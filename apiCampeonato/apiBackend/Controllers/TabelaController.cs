using ApiBackend.Data;
using ApiBackend.Models;


namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/tabela")]

    public class TabelaController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public TabelaController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] Tabela tabela)
        {
            try
            {
                tabela.CampeonatoId = _ctx.Campeonatos.Find(tabela.CampeonatoId);
                tabela.TimeId = _ctx.Times.Find(tabela.TimeId);
                _ctx.Times.Add(tabela);
                _ctx.SaveChanges();
                return Created("", tabela);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {
            List<Tabela> tabelas = _ctx.Tabelas.ToList();
            return tabelas.Count == 0 ? NotFound() : Ok(tabelas);
        }

        [HttpGet, Route("consultar/{id}")]
        public IActionResult Consultar([FromRoute] int id)
        {
            try
            {
                foreach (Tabela TabelaCadastrado in _ctx.Tabelas.ToList())
                {
                    if (TabelaCadastrado.TabelaId == id)
                    {
                        return Ok(TabelaCadastrado);
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
        public IActionResult Atualizar([FromBody] Tabela tabela, [FromRoute] int id)
        {
            try
            {

                Tabela? TabelaEncontrado = _ctx.Tabelas.FirstOrDefault(x => x.TabelaId == id);

                if (TabelaEncontrado != null)
                {
                    TabelaEncontrado.Nome = tabela.Nome;
                    _ctx.Tabelas.Update(TabelaEncontrado);
                    _ctx.SaveChanges();
                    return Ok(tabela);
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
                Tabela? tabela = _ctx.Tabelas.Find(id);

                if (tabela != null)
                {
                    _ctx.Tabelas.Remove(tabela);
                    _ctx.SaveChanges();
                    return Ok("Tabela Deletado!");
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
