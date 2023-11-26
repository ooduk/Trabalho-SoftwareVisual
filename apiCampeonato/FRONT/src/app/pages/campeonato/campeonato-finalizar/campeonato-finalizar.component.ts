import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRoute } from "@angular/router";
import { Finalizar } from "src/app/models/FinalizarModel";

@Component({
  selector: "app-campeonato-finalizar",
  templateUrl: "./campeonato-finalizar.component.html",
  styleUrls: ["./campeonato-finalizar.component.css"],
})
export class CampeonatoFinalizarComponent {
  colunasTabela: string[] = [
    "ganhador",
    "ultimoColocado",
  ];
  finalizar : Finalizar[] = []

  constructor(
    private client: HttpClient,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {
    
  }

  ngOnInit(): void {
    this.route.params.subscribe({
      next: (parametros) => {
        let { id } = parametros;  
            this.client
              .delete<Finalizar[]>(
                `https://localhost:7021/api/campeonato/finalizar/${id}`
              )
              .subscribe({
                next: (finalizar) => {
                  console.table(finalizar)
                  this.finalizar = finalizar;
                },
                error: (erro) => {
                  console.log(erro);
                },
              });
          },
    });
  }

}
