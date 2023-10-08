namespace apiBackend.Controllers
{
    using ApiBackend.Data;
    using ApiBackend.DTOs;
    using ApiBackend.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    [Route("api/tabela")]

    public class TabelaController : ControllerBase
    {
        private readonly AppDataContext _ctx;

        public TabelaController(AppDataContext ctx) => _ctx = ctx;

        [HttpPost, Route("cadastrar")]

        public IActionResult Cadastrar([FromBody] TabelaRequest tabelaRequest)
        {
            try
            {
                Campeonato? campeonato = _ctx.Campeonatos.Find(tabelaRequest.CampeonatoId);

                Time? time = _ctx.Times.Find(tabelaRequest.TimeId);

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

                _ctx.Tabelas.Add(tabela);
                _ctx.SaveChanges();

                string retorno = $"Tabela Cadastrada:\n\nCampeonato: {tabelaReturn.CampeonatoNome}\nTime: {tabelaReturn.TimeNome}\nPontos: {tabela.Pontos}\nGols Marcados: {tabela.Gols_marcados}\nGols Contra: {tabela.Gols_contra}\nVitórias: {tabela.Vitorias}\nDerrotas: {tabela.Derrotas}\nEmpates: {tabela.Empates}";
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
            try
            {
                List<TabelaReturn>? tabelas = _ctx.Tabelas
                .Include(t => t.Time)
                .Include(c => c.Campeonato)
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

                if (tabelas.Count() != 0)
                {
                    string retorno = $"Tabelas(s) Listada(s)\n\n";

                    foreach (var tabela in tabelas)
                    {
                        retorno += $"Campeonato: {tabela.CampeonatoNome}\nTime: {tabela.TimeNome}\nPontos: {tabela.Pontos}\nGols Marcados: {tabela.Gols_marcados}\nGols Contra: {tabela.Gols_contra}\nVitórias: {tabela.Vitorias}\nDerrotas: {tabela.Derrotas}\nEmpates: {tabela.Empates}\n\n";
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

        [HttpGet, Route("consultar/{id}")]
        public IActionResult Consultar([FromRoute] int id)
        {
            try
            {
                TabelaReturn? tabela = _ctx.Tabelas
                .Where(t => t.TabelaId == id)
                .Include(t => t.Time)
                .Include(c => c.Campeonato)
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
                }).FirstOrDefault();

                if (tabela != null)
                {
                    string retorno = $"Tabela Consultada:\n\nCampeonato: {tabela.CampeonatoNome}\nTime: {tabela.TimeNome}\nPontos: {tabela.Pontos}\nGols Marcados: {tabela.Gols_marcados}\nGols Contra: {tabela.Gols_contra}\nVitórias: {tabela.Vitorias}\nDerrotas: {tabela.Derrotas}\nEmpates: {tabela.Empates}\n\n";

                    return Ok(retorno);
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
