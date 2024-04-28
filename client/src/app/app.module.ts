import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './Components/user/user.component';
import { HomeComponent } from './Components/viewer/home/home.component';
import { MasterService } from './Shared/master.service';
import { AdultComponent } from './Components/viewer/adult/adult.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { PerformerComponent } from './Components/viewer/performer/performer.component';
import { HispanicComponent } from './Components/viewer/hispanic/hispanic.component';
import { DeadComponent } from './Components/viewer/dead/dead.component';
import { CharachterComponent } from './Components/viewer/charachter/charachter.component';
import { KidsComponent } from './Components/viewer/kids/kids.component';
import { CustomComponent } from './Components/viewer/custom/custom.component';
import { InternationalComponent } from './Components/viewer/international/international.component';
import { SearchComponent } from './Components/viewer/search/search.component';
import { JwtModule } from "@auth0/angular-jwt";
import { ReportDetailsComponent } from './Components/viewer/search/report-details/report-details.component';
import { ReportsComponent } from './Components/user/Elements/reports/reports.component';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ExcelFilesComponent } from './Components/user/Elements/excel-files/excel-files.component';
import { UserModifyComponent } from './Components/user/Elements/user-modify/user-modify.component';
import { EditReportComponent } from './Components/user/Elements/reports/edit-report/edit-report.component';
import { ViewReportComponent } from './Components/user/Elements/reports/view-report/view-report.component';
import { PrintReportComponent } from './Components/user/Elements/PrintReport/print-report.component';
import { LayoutComponent } from './layout/layout.component';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSelectModule } from '@angular/material/select'; // Import MatSelectModule
import { MatOptionModule } from '@angular/material/core';
import { PaginationComponent } from './Components/user/Elements/reports/view-report/pagination/pagination.component'; // Import MatOptionModule

export function tokenGetter() {
  return sessionStorage.getItem("_token");
}

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    HomeComponent,
    AdultComponent,
    PerformerComponent,
    HispanicComponent,
    DeadComponent,
    CharachterComponent,
    KidsComponent,
    CustomComponent,
    InternationalComponent,
    SearchComponent,
    ReportDetailsComponent,
    ReportsComponent,
    ExcelFilesComponent,
    UserModifyComponent,
    EditReportComponent,
    ViewReportComponent,
    PrintReportComponent,
    LayoutComponent,
    PaginationComponent
    

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["example.com"],
        disallowedRoutes: ["http://example.com/examplebadroute/"],
      },
    }),
    MatPaginatorModule,
    MatTableModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatSelectModule, // Import MatSelectModule
    MatOptionModule
  ],
  providers: [MasterService],
  bootstrap: [AppComponent]
})
export class AppModule { }
