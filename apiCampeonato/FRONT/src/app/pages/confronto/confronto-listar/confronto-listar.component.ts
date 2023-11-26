import { Confronto } from './../../../models/ConfrontoModels';
import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";

@Component({
  selector: "app-confronto-listar",
  templateUrl: "./confronto-listar.component.html",
  styleUrls: ["./confronto-listar.component.css"],
})
export class ConfrontoListarComponent {
  colunasTabela: string[] = [
    "campeonatoNome",
    "resultado"
  ];
  confrontos: Confronto[] = [];

  constructor(
    private client: HttpClient
  ) {
    
  }

  ngOnInit(): void {
    this.client
      .get<Confronto[]>("https://localhost:7021/api/confronto/listar")
      .subscribe({
      
        next: (confrontos: Confronto[]) => {
          console.table(confrontos);
          this.confrontos = confrontos;
        },
    
        error: (erro: any) => {
          console.log(erro);
        },
      });
  }
}
