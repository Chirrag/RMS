import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportType } from 'src/app/Shared/models/reportType.model';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @Output() childEvent = new EventEmitter<number>();

 public  constructor(private route: ActivatedRoute,private router: Router){

  }
  
  dataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = ['reportName', 'datetime', 'recID', 'actions'];

  reportTypes: ReportType[] = [];


  ngOnInit() {
    this.dataSource.paginator = this.paginator;
  }

  onDelete(recID: number) {
    this.childEvent.emit(recID);
  }


  onClickEdit(id:any){
    this.router.navigateByUrl(`/editReport/Report/${id}`,{state:{myReportType:this.reportTypes}});
  }

  onClickView(id:any){
    this.router.navigateByUrl(`/viewReport/Report/${id}`,{state:{myReportType:this.reportTypes}});
  }

}
