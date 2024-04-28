import { Component, OnInit } from '@angular/core';
import { MasterService } from 'src/app/Shared/master.service';
import { Report } from 'src/app/Shared/models/report.model';

@Component({
  selector: 'app-print-report',
  templateUrl: './print-report.component.html',
  styleUrls: ['./print-report.component.css']
})
export class PrintReportComponent implements OnInit {

  constructor(private service:MasterService){}
  
   allReport:Report[]=[];

   ngOnInit(): void {
    this.getAllReports();
   }
 
  getAllReports() {
    this.service.getAllReports().subscribe(
      res => {
        this.allReport=res;  
      },
      err => {
        console.log(err);
      }
    )
  }

  print(){
    window.print();
    return false;
  }

  
}
