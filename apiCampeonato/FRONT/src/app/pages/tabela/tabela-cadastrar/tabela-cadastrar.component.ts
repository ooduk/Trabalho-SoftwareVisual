// import { HttpClient } from "@angular/common/http";
// import { Component } from "@angular/core";
// import { MatSnackBar } from "@angular/material/snack-bar";
// import { Router } from "@angular/router";
// import { Tabela } from "src/app/models/TabelaModels";
// import { Time } from "src/app/models/TimeModel";
// import { Campeonato } from './../../../models/CampeonatoModel';

// @Component({
//   selector: "app-tabela-cadastrar",
//   templateUrl: "./tabela-cadastrar.component.html",
//   styleUrls: ["./tabela-cadastrar.component.css"],
// })
// export class TabelaCadastrarComponent {
//   CampeonatoId: number;
//   TimeId: number;
//   Pontos: number;
//   Gols_marcados: number;
//   Gols_contra: number;
//   Vitorias: number;
//   Empates: number;
//   Derrotas: number;

//   constructor(
//     private client: HttpClient,
//     private snackBar: MatSnackBar,
//     private router: Router
//   ) {
//     this.CampeonatoId = 0;
//     this.TimeId = 0;
//     this.Pontos = 0;
//     this.Gols_marcados = 0;
//     this.Gols_contra = 0;
//     this.Vitorias = 0;
//     this.Empates = 0;
//     this.Derrotas = 0;
//   }

//   cadastrar(): void {
//     let tabela = new Tabela();
//     tabela.CampeonatoId = this.CampeonatoId;
//     tabela.TimeId = this.TimeId;
//     tabela.Pontos = this.Pontos;
//     tabela.Gols_marcados = this.Gols_marcados;
//     tabela.Gols_contra = this.Gols_contra;
//     tabela.Vitorias = this.Vitorias;
//     tabela.Empates = this.Empates;
//     tabela.Derrotas = this.Derrotas;

//     this.client
//       .post<Tabela>(
//         "https://localhost:7195/api/tabela/cadastrar",
//         tabela
//       )
//       .subscribe({
//         next: (tabela: Tabela) => {
//           console.log('Resposta do servidor:', tabela);
//           this.snackBar.open(
//             `Tabela cadastrada com sucesso!!`,
//             "E-commerce",
//             {
//               duration: 1500,
//               horizontalPosition: "right",
//               verticalPosition: "top",
//             }
//           );
//           this.router.navigate(["pages/tabela/listar"]);
//         },
//         error: (erro: any) => {
//           console.error('Erro na requisição:', erro);
//         },
//       });
//   }
// }