import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRoute } from "@angular/router";
import { Confronto } from "src/app/models/ConfrontoModels";
import { Time } from "src/app/models/TimeModel";

@Component({
  selector: "app-time-historico",
  templateUrl: "./time-historico.component.html",
  styleUrls: ["./time-historico.component.css"],
})
export class TimeHistoricoComponent {
  colunasTabela: string[] = [
    "campeonatoNome",
    "resultado"
  ];
  confrontos: Confronto[] = [];

  constructor(
    private client: HttpClient,
    private route: ActivatedRoute
  ) {
    
  }

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (parametros) => {
        let { id } = parametros;  
            this.client
              .get<Confronto[]>(
                `https://localhost:7021/api/time/historico/${id}`
              )
              .subscribe({
                next: (confrontos) => {
                  this.confrontos = confrontos;
                },
                error: (erro) => {
                  console.log(erro);
                },
              });
          },
    });
  }
}
