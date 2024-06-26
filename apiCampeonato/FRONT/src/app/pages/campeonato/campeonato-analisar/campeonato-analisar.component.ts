import { Analise } from './../../../models/AnaliseModel';
import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
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
  analise : Analise[] = [];

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
                next: (analise) => {
                  this.analise = analise;
                },
                error: (erro) => {
                  console.log(erro);
                },
              });
          },
    });
  }
}
