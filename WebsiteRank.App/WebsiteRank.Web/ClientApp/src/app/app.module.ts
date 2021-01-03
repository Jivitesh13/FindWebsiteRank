import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatProgressSpinnerModule, MatTableModule, MatPaginatorModule, MatSortModule, MatButtonModule, MatInputModule, MatSelectModule, MatRadioModule, MatCardModule, MatCheckboxModule } from '@angular/material';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { SearchComponent } from './components/search/search.component';
import { SearchResultComponent } from './components/search-result/search-result.component';
import { SearchTrendComponent } from './components/search-trend/search-trend.component';
import { DataService } from './shared/dataservice';

import { GoogleChartsModule } from 'angular-google-charts';
import { ScriptLoaderService } from 'angular-google-charts';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    SearchComponent,
    SearchResultComponent,
    SearchTrendComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatCardModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    GoogleChartsModule,
  
    RouterModule.forRoot([
     
      { path: 'search', component: SearchComponent },
      { path: 'search-result', component: SearchResultComponent },
      { path: 'search-trend', component: SearchTrendComponent },

      { path: '', component: SearchComponent, pathMatch: 'full' },
    ]),
  
  ],
  providers: [DataService, ScriptLoaderService],
  bootstrap: [AppComponent]
})
export class AppModule { }
