using ApiBackend.Data;
using ApiBackend.DTOs;
using ApiBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiBackend.Controllers
{
    [ApiController]
    [Route("api/tabela")]

    public class TabelaController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public TabelaController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] TabelaRequestPost tabelaRequestPost)
        {
            try
            {

                Campeonato? campeonato = _ctx.Campeonatos.Find(tabelaRequestPost.CampeonatoId);

                Time? time = _ctx.Times.Find(tabelaRequestPost.TimeId);

                if ((campeonato == null) || (time == null))
                {
                    return NotFound();
                }

                Tabela tabela = new Tabela
                {
                    Campeonato = campeonato,
                    Time = time
                };

                TabelaReturn tabelaReturn = new TabelaReturn
                {
                    TimeNome = tabela.Time.Nome,
                    CampeonatoNome = tabela.Campeonato.Nome
                };

                string retorno = $"Tabela Cadastrada:\n\nCampeonato: {tabelaReturn.CampeonatoNome}\nTime: {tabelaReturn.TimeNome}\nPontos: {tabela.Pontos}\nGols Marcados: {tabela.Gols_marcados}\nGols Contra: {tabela.Gols_contra}\nVitórias: {tabela.Vitorias}\nDerrotas: {tabela.Derrotas}\nEmpates: {tabela.Empates}";

                _ctx.Tabelas.Add(tabela);
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
            // Retorna todas as tabelas
            List<Tabela> tabelas = _ctx.Tabelas
            .Include(t => t.Time)
            .Include(t => t.Campeonato)
            .ToList();

            if (tabelas.Count() != 0)
            {

                // Agrupa as tabelas retornadas 
                var campeonatos = tabelas
                .OrderByDescending(t => t.Pontos)
                .GroupBy(t => t.CampeonatoId)
                .ToList();

                // Adiciona o titúlo ao retorno
                string retorno = "Tabelas(s) Listada(s):\n\n";

                // Itera cada campeonato do retorno
                foreach (var campeonato in campeonatos)
                {
                    // Recebe o nome do campeonato e adiciona ela na string de retorna
                    var campeonatoNome = campeonato.First().Campeonato.Nome;
                    retorno += $"Campeonato: {campeonatoNome}\n\n";

                    // Itera as tabelas daquele campeonato específico e formata elas elas
                    List<TabelaReturn> campeonatoTabelas = campeonato
                    .Select(t => new TabelaReturn
                    {
                        TimeNome = t.Time.Nome,
                        CampeonatoNome = t.Campeonato.Nome,
                        Pontos = t.Pontos,
                        Gols_marcados = t.Gols_marcados,
                        Gols_contra = t.Gols_contra,
                        Vitorias = t.Vitorias,
                        Empates = t.Empates,
                        Derrotas = t.Derrotas
                    })
                    .ToList();

                    // Itera as tabelas já formatadas e adiciona elas ao retorno
                    foreach (var tabela in campeonatoTabelas)
                    {
                        retorno += $"Time: {tabela.TimeNome}\nPontos: {tabela.Pontos}\nGols Marcados: {tabela.Gols_marcados}\nGols Contra: {tabela.Gols_contra}\nVitórias: {tabela.Vitorias}\nDerrotas: {tabela.Derrotas}\nEmpates: {tabela.Empates}\n\n";
                    }
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
                List<TabelaReturn> times = _ctx.Tabelas
                .Where(t => t.CampeonatoId == id)
                .Include(t => t.Time)
                .Include(c => c.Campeonato)
                .OrderByDescending(t => t.Pontos)
                .Select(t => new TabelaReturn
                {
                    TimeNome = t.Time.Nome,
                    CampeonatoNome = t.Campeonato.Nome,
                    Pontos = t.Pontos,
                    Gols_marcados = t.Gols_marcados,
                    Gols_contra = t.Gols_contra,
                    Vitorias = t.Vitorias,
                    Empates = t.Empates,
                    Derrotas = t.Derrotas
                })
                .ToList();

                // Adiciona o titúlo ao retorno
                string retorno = "Tabelas(s) Listada(s):\n\n";

                if (times.Count() != 0)
                {

                    var campeonatoNome = times.First().CampeonatoNome;

                    retorno += $"Campeonato: {campeonatoNome}\n\n";

                    foreach (var time in times)
                    {
                        retorno += $"Time: {time.TimeNome}\nPontos: {time.Pontos}\nGols Marcados: {time.Gols_marcados}\nGols Contra: {time.Gols_contra}\nVitórias: {time.Vitorias}\nDerrotas: {time.Derrotas}\nEmpates: {time.Empates}\n\n";
                    }
                    return Ok(retorno);
                }

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut, Route("atualizar/{id}")]
        public IActionResult Atualizar([FromBody] TabelaRequestPut tabelaRequestPut, [FromRoute] int id)
        {
            try
            {

                Tabela? TabelaEncontrado = _ctx.Tabelas.FirstOrDefault(x => x.TabelaId == id);

                Campeonato? campeonato = _ctx.Campeonatos.Find(tabelaRequestPut.CampeonatoId);

                Time? time = _ctx.Times.Find(tabelaRequestPut.TimeId);

                if ((campeonato == null) || (time == null))
                {
                    return NotFound();
                }

                if (TabelaEncontrado != null)
                {

                    TabelaEncontrado.Campeonato = campeonato;
                    TabelaEncontrado.Time = time;
                    TabelaEncontrado.Pontos = tabelaRequestPut.Pontos;
                    TabelaEncontrado.Gols_marcados = tabelaRequestPut.Gols_marcados;
                    TabelaEncontrado.Gols_contra = tabelaRequestPut.Gols_contra;
                    TabelaEncontrado.Vitorias = tabelaRequestPut.Vitorias;
                    TabelaEncontrado.Empates = tabelaRequestPut.Empates;
                    TabelaEncontrado.Derrotas = tabelaRequestPut.Derrotas;

                    TabelaReturn tabelaReturn = new TabelaReturn
                    {
                        TimeNome = TabelaEncontrado.Time.Nome,
                        CampeonatoNome = TabelaEncontrado.Campeonato.Nome,
                        Pontos = TabelaEncontrado.Pontos,
                        Gols_marcados = TabelaEncontrado.Gols_marcados,
                        Gols_contra = TabelaEncontrado.Gols_contra,
                        Vitorias = TabelaEncontrado.Vitorias,
                        Empates = TabelaEncontrado.Empates,
                        Derrotas = TabelaEncontrado.Derrotas
                    };

                    string retorno = $"Tabela Atualizada:\n\nCampeonato: {tabelaReturn.CampeonatoNome}\nTime: {tabelaReturn.TimeNome}\nPontos: {tabelaReturn.Pontos}\nGols Marcados: {tabelaReturn.Gols_marcados}\nGols Contra: {tabelaReturn.Gols_contra}\nVitórias: {tabelaReturn.Vitorias}\nDerrotas: {tabelaReturn.Derrotas}\nEmpates: {tabelaReturn.Empates}\n\n";

                    _ctx.Tabelas.Update(TabelaEncontrado);
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
                Tabela? tabela = _ctx.Tabelas.Find(id);

                if (tabela != null)
                {
                    _ctx.Tabelas.Remove(tabela);
                    _ctx.SaveChanges();
                    return Ok("Tabela Deletada!");
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
