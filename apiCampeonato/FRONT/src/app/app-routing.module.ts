import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { TimeListarComponent } from "./pages/time/time-listar/time-listar.component";
import { TimeCadastrarComponent } from "./pages/time/time-cadastrar/time-cadastrar.component";
import { TimeAlterarComponent } from "./pages/time/time-alterar/time-alterar.component";
import { CampeonatoCadastrarComponent } from "./pages/campeonato/campeonato-cadastrar/campeonato-cadastrar.component";
import { CampeonatoListarComponent } from "./pages/campeonato/campeonato-listar/campeonato-listar.component";
import { ConfrontoListarComponent } from "./pages/confronto/confronto-listar/confronto-listar.component";
import { TimeHistoricoComponent } from "./pages/time/time-historico/time-historico.component";
import { CampeonatoAlterarComponent } from "./pages/campeonato/campeonato-alterar/campeonato-alterar.component";

const routes: Routes = [
  {
    path: "",
    component: TimeListarComponent,
  },
  {
    path: "pages/time/listar",
    component: TimeListarComponent,
  },
  {
    path: "pages/time/cadastrar",
    component: TimeCadastrarComponent,
  },
  {
    path: "pages/time/alterar/:id",
    component: TimeAlterarComponent,
  },
  {
    path: "pages/campeonato/cadastrar",
    component: CampeonatoCadastrarComponent,
  },
  {
    path: "pages/campeonato/listar",
    component: CampeonatoListarComponent,
  },
  {
    path: "pages/campeonato/alterar/:id",
    component: CampeonatoAlterarComponent,
  },
  {
    path: "pages/confronto/listar",
    component: ConfrontoListarComponent,
  },
  {
    path: "pages/time/historico/:id",
    component: TimeHistoricoComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
