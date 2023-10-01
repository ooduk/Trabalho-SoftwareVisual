namespace ApiBackend.Models;

public class Confronto
{
    public int ConfrontoId{ get; set; }
    public int TimeCasaId { get; set; }
    public int TimeForaId { get; set; }
    public Time TimeCasa { get; set; }
    public  Time TimeFora { get; set; }
    public Campeonato Campeonato { get; set; }
    public int CampeonatoId { get; set; }
    public int Gols_time_casa{ get; set; }
    public int Gols_time_fora{ get; set; }
}