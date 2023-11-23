import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";
import { Campeonato } from './../../../models/CampeonatoModel';

@Component({
  selector: "app-campeonato-cadastrar",
  templateUrl: "./campeonato-cadastrar.component.html",
  styleUrls: ["./campeonato-cadastrar.component.css"],
})
export class CampeonatoCadastrarComponent {
  campeonatoId: number = 0;
  nome: string = "";
  premiacao: string = "";
  campeonatos: Campeonato[] = [];

  constructor(
    private client: HttpClient,
    private router: Router,
    private snackBar: MatSnackBar
  ) {}

  cadastrar(): void {
    let campeonato: Campeonato = {
      campeonatoId: this.campeonatoId,
      nome: this.nome,
      premiacao: this.premiacao,
    };

    this.client
      .post<Campeonato>(
        "https://localhost:7195/api/campeonato/cadastrar",
        campeonato
      )
      .subscribe({
        next: (campeonato: any) => {
          console.log('Resposta do servidor:', campeonato);
          this.snackBar.open(
            "Campeonato cadastrado com sucesso!!",
            "E-commerce",
            {
              duration: 1500,
              horizontalPosition: "right",
              verticalPosition: "top",
            }
          );
          this.router.navigate(["pages/campeonato/listar"]);
        },
        error: (erro: any) => {
          console.error('Erro na requisição:', erro);
        },
      });
  }
}