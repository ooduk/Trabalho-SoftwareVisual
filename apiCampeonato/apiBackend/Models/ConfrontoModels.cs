namespace ApiBackend.Models;

public class Confronto
{
    public int ConfrontoId{ get; set; }
    public Time? Time { get; set; }
    public int Id_time_casa{ get; set; }
    public int Id_time_fora{ get; set; }
    public Campeonato? Campeonato { get; set; }
    public int CampeonatoId { get; set; }
    public int Gols_time_casa{ get; set; }
    public int Gols_time_fora{ get; set; }
}