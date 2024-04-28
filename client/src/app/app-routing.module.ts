import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Components/viewer/home/home.component';
import { AdultComponent } from './Components/viewer/adult/adult.component';
import { CustomComponent } from './Components/viewer/custom/custom.component';
import { SearchComponent } from './Components/viewer/search/search.component';
import { UserComponent } from './Components/user/user.component';
import { AuthGuard } from './Shared/Auth/auth.guard';
import { ReportDetailsComponent } from './Components/viewer/search/report-details/report-details.component';
import { EditReportComponent } from './Components/user/Elements/reports/edit-report/edit-report.component';
import { ViewReportComponent } from './Components/user/Elements/reports/view-report/view-report.component';
import { PrintReportComponent } from './Components/user/Elements/PrintReport/print-report.component';
import { AppComponent } from './app.component';
import { LayoutComponent } from './layout/layout.component';


const routes: Routes = [

  { path: '', component: LayoutComponent, children: [

    { path: '', component: HomeComponent, data: { route: 'home'}, canActivate:[AuthGuard]},
    { path: 'adultBrand', component: AdultComponent, data: { route: 'adultBrand'}},
    { path: 'customResearch', component: CustomComponent, data: { route: 'customResearch'}},
    { path: 'search', component: SearchComponent, data: { route: 'search'}},
    { path: 'reportDetails', component: ReportDetailsComponent, data: { route: 'reportDetails'}},

    { path: 'user', component: UserComponent, data: { route: 'user'}, canActivate:[AuthGuard]},
    { path: 'editReport/Report/:recId', component: EditReportComponent, data: { route: 'editReport'}, canActivate:[AuthGuard]},
    { path: 'viewReport/Report/:recId', component: ViewReportComponent, data: { route: 'viewReport'}, canActivate:[AuthGuard]},
   
    // Add other routes here
  ] },
  
  { path: 'printReport', component: PrintReportComponent },
  // Add other routes here
  ];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
