import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatListModule } from "@angular/material/list";
import { MatTableModule } from "@angular/material/table";
import { MatCardModule } from "@angular/material/card";
import { MatSelectModule } from "@angular/material/select";
import { MatInputModule } from "@angular/material/input";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { FormsModule } from "@angular/forms";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { TimeAlterarComponent } from './pages/time/time-alterar/time-alterar.component';
import { TimeCadastrarComponent } from "./pages/time/time-cadastrar/time-cadastrar.component";
import { TimeListarComponent} from "./pages/time/time-listar/time-listar.component";
import { CampeonatoListarComponent} from "./pages/campeonato/campeonato-listar/campeonato-listar.component";
import { CampeonatoCadastrarComponent} from "./pages/campeonato/campeonato-cadastrar/campeonato-cadastrar.component";
import { CampeonatoAlterarComponent} from "./pages/campeonato/campeonato-alterar/campeonato-alterar.component";
import { ConfrontoListarComponent } from "./pages/confronto/confronto-listar/confronto-listar.component";
import { TimeHistoricoComponent } from "./pages/time/time-historico/time-historico.component";
import { CampeonatoClassificacaoComponent } from "./pages/campeonato/campeonato-classificacao/campeonato-classificacao.component";
import { CampeonatoClassificacaoDetalhadaComponent } from "./pages/campeonato/campeonato-classificacao-detalhada/campeonato-classificacao-detalhada.component";
import { CampeonatoAnalisarComponent } from "./pages/campeonato/campeonato-analisar/campeonato-analisar.component";
import { ConfrontoCadastrarComponent } from "./pages/confronto/confronto-cadastrar/confronto-cadastrar.component";
import { CampeonatoFinalizarComponent } from "./pages/campeonato/campeonato-finalizar/campeonato-finalizar.component";

@NgModule({
  declarations: [
    AppComponent,
    TimeListarComponent,
    TimeCadastrarComponent,
    TimeAlterarComponent,
    TimeHistoricoComponent,
    CampeonatoAlterarComponent,
    CampeonatoCadastrarComponent,
    CampeonatoListarComponent,
    CampeonatoClassificacaoComponent,
    CampeonatoClassificacaoDetalhadaComponent,
    CampeonatoAnalisarComponent,
    CampeonatoFinalizarComponent,
    ConfrontoListarComponent,
    ConfrontoCadastrarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatTableModule,
    MatCardModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule,
    MatSnackBarModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
