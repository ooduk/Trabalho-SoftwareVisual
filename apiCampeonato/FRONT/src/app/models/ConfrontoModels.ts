import { Campeonato } from "./CampeonatoModel";

export interface Confronto {
  ConfrontoId?: number;
  TimeCasaId: number;
  TimeForaId: number;
  
  
  CampeonatoId: number;
  campeonato?: Campeonato;
  Gols_time_casa: number;
  Gols_fora_casa: number;
}
