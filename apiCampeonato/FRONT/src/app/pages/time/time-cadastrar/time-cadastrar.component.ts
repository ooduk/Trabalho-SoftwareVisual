import { Campeonato } from './../../../models/CampeonatoModel';
import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Time } from "src/app/models/TimeModel";

@Component({
  selector: "app-time-cadastrar",
  templateUrl: "./time-cadastrar.component.html",
  styleUrls: ["./time-cadastrar.component.css"],
})
export class TimeCadastrarComponent {
  nome: string = "";
  id: number = 0;
  times: Time[] = [];

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}


  cadastrar(): void {
    let time: Time = {
      timeId: this.id,
      nome: this.nome
    };
  
    this.client
      .post<Time>(
        "https://localhost:7021/api/time/cadastrar",
        time
      )
      .subscribe({
        next: (time: any) => {
     console.log("uwgfe");
          console.log('Resposta do servidor:', time);
          this.snackBar.open(
            "Time cadastrado com sucesso!!",
            "CampManager",
            {
              duration: 1500,
              horizontalPosition: "right",
              verticalPosition: "top",
            }
          );
          this.router.navigate(["pages/time/listar"]);
        },
    
        error: (erro: any) => {
          console.error('Erro na requisição:', erro);
        },
      });
  }
}
