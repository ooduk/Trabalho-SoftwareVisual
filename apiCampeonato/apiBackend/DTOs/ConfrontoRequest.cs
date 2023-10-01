namespace ApiBackend.DTOs;

public class ConfrontoRequest
{
    public int TimeCasaId { get; set; }
    public int TimeForaId { get; set; }
    public int CampeonatoId { get; set; }
    public int Gols_time_casa{ get; set; }
    public int Gols_time_fora{ get; set; }
}