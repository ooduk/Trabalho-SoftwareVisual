import { Analise } from './../../../models/AnaliseModel';
import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Campeonato } from './../../../models/CampeonatoModel';
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-campeonato-analisar",
  templateUrl: "./campeonato-analisar.component.html",
  styleUrls: ["./campeonato-analisar.component.css"],
})
export class CampeonatoAnalisarComponent {
  colunasTabela: string[] = [
    "timeVitorias",
    "timeDerrotas",
    "timeEmpates",
    "timeMaisGolsMarcados",
    "timeMenosGolsMarcados",
    "timeMaisGolsSofridos",
    "timeMenosGolsSofridos"
  ];
  analises : Analise[] = [];

  constructor(
    private client: HttpClient,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  )
  {
  }

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (parametros) => {
        let { id } = parametros;  
            this.client
              .get<Analise[]>(
                `https://localhost:7021/api/campeonato/analisar/${id}`
              )
              .subscribe({
                next: (analises) => {
                  console.table(analises)
                  this.analises = analises;
                  console.table(this.analises);
                },
                error: (erro) => {
                  console.log(erro);
                },
              });
          },
    });
  }
}
