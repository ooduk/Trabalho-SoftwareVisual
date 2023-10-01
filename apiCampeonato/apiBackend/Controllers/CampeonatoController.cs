using ApiBackend.Data;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/campeonato")]

    public class CampeonatoController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public CampeonatoController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] Campeonato campeonato)
        {
            try
            {

                string retorno = $"Campeonato Cadastrado:\n\nNome {campeonato.Nome}\nPremiação: R${campeonato.Premiacao}";
                _ctx.Campeonatos.Add(campeonato);
                _ctx.SaveChanges();
                return Created("", retorno);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet, Route("listar")]

        public IActionResult Listar()
        {

            List<Campeonato> campeonatos = _ctx.Campeonatos.ToList();

            string retorno = "Campeonato(s) Listado(s):\n\n";

            if (campeonatos.Count() != 0)
            {
                foreach (var campeonato in campeonatos)
                {
                    retorno += $"Nome: {campeonato.Nome}\nPremiação: R${campeonato.Premiacao}\n\n";
                }
                return Ok(retorno);
            }

            return NotFound();

        }

        [HttpGet, Route("consultar/{id}")]

        public IActionResult Consultar([FromRoute] int id)
        {

            try
            {
                foreach (Campeonato CampeonatoCadastrado in _ctx.Campeonatos.ToList())
                {
                    if (CampeonatoCadastrado.CampeonatoId == id)
                    {
                        string retorno = $"Campeonato Consultado:\n\nNome: {CampeonatoCadastrado.Nome}\nPremiação: R${CampeonatoCadastrado.Premiacao}";
                        return Ok(retorno);
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

        public IActionResult Atualizar([FromBody] Campeonato campeonato, [FromRoute] int id)
        {
            try
            {

                Campeonato? campeonatoEncontrado = _ctx.Campeonatos.FirstOrDefault(x => x.CampeonatoId == id);

                if (campeonatoEncontrado != null)
                {
                    campeonatoEncontrado.Nome = campeonato.Nome;
                    campeonatoEncontrado.Premiacao = campeonato.Premiacao;
                    string retorno = $"Campeonato Atualizado:\n\nNome: {campeonatoEncontrado.Nome}\nPremiação: R${campeonatoEncontrado.Premiacao}";
                    _ctx.Campeonatos.Update(campeonatoEncontrado);
                    _ctx.SaveChanges();
                    return Ok(retorno);
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
                Campeonato? Campeonato = _ctx.Campeonatos.Find(id);

                if (Campeonato != null)
                {
                    _ctx.Campeonatos.Remove(Campeonato);
                    _ctx.SaveChanges();
                    return Ok("Campeonato Deletado!");
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("analisar/{id}")]

        public IActionResult Analisar([FromRoute] int id)
        {
            try
            {
                Campeonato? campeonato = _ctx.Campeonatos.Find(id);

                // Busca na tabela onde possui o id_desse campeonato, o time que possui mais vitorias, empates e derrotas
                // Busca na tabela onde possui o id_desse campeonato, o time que possui mais e menos gols marcados
                // Junta todos esses dados e retorna

                return Ok(campeonato);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}