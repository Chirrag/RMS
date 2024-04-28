import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';
import { Report } from 'src/app/Shared/models/report.model';
import { Userinfo } from 'src/app/Shared/models/userinfo.model';
import { ReportsComponent } from './Elements/reports/reports.component';
import Swal from 'sweetalert2';
import { ReportSource } from 'src/app/Shared/models/reportSource.model';
import { ReportType } from 'src/app/Shared/models/reportType.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  public constructor(private route: ActivatedRoute, public service: MasterService, private fb: FormBuilder, private router: Router) {
    if (route.snapshot.data['route'] != undefined) this.service.routeName = route.snapshot.data['route'];
  }

  ngOnInit(): void {
    this.getUserInfo();
    this.getAllReports();
    this.getReportSource();
    this.getAllZipFiles();
    this.createReport();
  }

  @ViewChild(ReportsComponent) reportComponent: ReportsComponent;
  allReports: Report[] = [];
  displayReports: Report[] = [];
  userInfo: Userinfo;
  reportSources: ReportSource[] = [];
  reportTypes: ReportType[] = [];
  filterInput: string = "";
  allReportsModalShow: boolean = false;
  excelFilesModalShow: boolean = false;
  userModifyModalShow: string = "";
  createReportModal: boolean = false;
  editModal:boolean =false;
  zipFilesCount: number = 0;
  createReportForm: FormGroup;
  RankingParameterArray:any[]=[];
  RankingParameter:any;

  //Create Report
  createReport() {
    this.createReportForm = this.fb.group({
      reportName: ['', [Validators.required]],
      application: ['', [Validators.required]],
      rptType: ['', [Validators.required]],
      ranking: ['-1']
    })
   
  }

  isRptType3() {
    return this.createReportForm.get('rptType').value == 'Ranking';
    
  }

  onSubmitCreateReport() {
    this.service.postCreateReport(this.createReportForm.value).subscribe(
      res => {
           console.log(this.createReportForm.value)
        var ranking=this.createReportForm.value.ranking;
        this.createReportForm.reset();
        this.reportTypes = [];
        Swal.fire({
          title: 'Successful!',
          text: 'Report Created Successfully.',
          icon: 'success',
        });
        this.router.navigateByUrl(`/editReport/Report/${res}`,{state:{myReportType:this.reportTypes,ranking}});
        
      },
      err => {
        console.log(err);
      }
    )
  }

  //Filter Method
  async onFilter() {
    this.displayReports = await this.allReports;
    this.displayReports = this.displayReports.filter((report) =>
      report.reportName.toLowerCase().includes(this.filterInput.toLowerCase()) ||
      report.recID.toString().toLowerCase().includes(this.filterInput.toLowerCase())
    );
  }

  //Get All Reports
  getAllReports() {
    this.service.getAllReports().subscribe(
      res => {
        // console.log(res);
        this.allReports = res as Report[];
        this.displayReports = res as Report[];
        this.reportComponent.dataSource.data = res as Report[];
      },
      err => {
        console.log(err);
      }
    )
  }

  //Get User Info
  getUserInfo() {
    this.service.getUserInfo().subscribe(
      res => {
        // console.log(res);
        this.userInfo = res as Userinfo;
        this.userModifyModalShow = "";
      },
      err => {
        console.log(err);
      }
    )
  }

  //Get count of all zip files
  getAllZipFiles() {
    this.service.getAllZipFiles().subscribe(
      res => {
        this.zipFilesCount = res.length;
      },
      err => {
        console.log(err);
      }
    )
  }

  //Toggle Modal Show
  toggleAllReportsModal() {
    this.allReportsModalShow = !this.allReportsModalShow;
  }
  toggleExcelFilesModal() {
    this.excelFilesModalShow = !this.excelFilesModalShow;
  }
  toggleCreateReportModal() {
    this.createReportModal = !this.createReportModal;
  }
  showEditProfileModal() {
    this.userModifyModalShow = "Edit Profile";
  }
  showChangePasswordModal() {
    this.userModifyModalShow = "Change Password";
  }
  showChangePictureModal() {
    this.userModifyModalShow = "Change Picture";
  }
  closeUserModifyModal() {
    this.userModifyModalShow = "";
  }


  //On Delete
  onDelete(recID: number) {
    Swal.fire({
      title: 'Confirm Delete',
      text: 'Are you sure you want to delete this item?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Confirm',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        this.service.postDeleteReport(recID).subscribe(
          res => {
            // console.log(res);
            this.getAllReports();
          },
          err => {
            console.log(err);
          }
        )
      } else {
        // console.log('Deletion canceled');
      }
    });
  }


  //Create New Report---
  //Get Report Source
  getReportSource() {
    this.service.getReportSource().subscribe(
      res => {
        // console.log(res);
        this.reportSources = res as ReportSource[];
      },
      err => {
        console.log(err);
      }
    )
  }

  //Get Report Types
  getReportTypes(event: any) {
    this.service.getReportType(event.target.value).subscribe(
      res => {
        // console.log(res);
        this.reportTypes = res as ReportType[];
        console.log(this.reportTypes)
        this.getRankingParameter(event)
      
      },
      err => {
        console.log(err);
      }
    )
  }

  
  onClickEdit(id:any){
    this.router.navigateByUrl(`/editReport/Report/${id}`,{state:{myReportType:this.reportTypes}});
  }
  onClickView(id:any){
    this.router.navigateByUrl(`/viewReport/Report/${id}`,{state:{myReportType:this.reportTypes}});
  }


   getRankingParameter(event: any){
    this.service.RankingReport(event.target.value).subscribe(
      (res:any)=>{
       this.RankingParameterArray=res;
       console.log(this.RankingParameterArray)
      }
    )
   }

}
