import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';
import { Categories } from 'src/app/Shared/models/applicationTypes/categories.model';
import { DemoData } from 'src/app/Shared/models/applicationTypes/demoData.model';
import { ReportData } from 'src/app/Shared/models/applicationTypes/reportData.model';
import { ScalePoint } from 'src/app/Shared/models/applicationTypes/scalePoint.model';
import { StudyData } from 'src/app/Shared/models/applicationTypes/studyData.model';
import { UpdateModel } from 'src/app/Shared/models/applicationTypes/update.model';
import Swal from 'sweetalert2';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit-report',
  templateUrl: './edit-report.component.html',
  styleUrls: ['./edit-report.component.css'],

})
export class EditReportComponent implements OnInit {
  UpdateModal: any;
  checked: any = false;
  constructor(public service: MasterService, private route: ActivatedRoute, private router: Router) { }

  parameters: any = this.route.params['_value']['recId'];

  reportData: ReportData = new ReportData;
  studyData: StudyData[] = [];
  scalePoint: ScalePoint[] = [];
  demodata: DemoData[] = [];
  categories: Categories[] = [];
  demoType: any;
  demoTypeValue: any = "";
  // Pagination 
  currentPage: number = 1;
  itemsPerPage: number = 10;

  showSelectAllStudyWavesCheckbox: boolean = true;
  showSelectAllScalePointsCheckbox: boolean = true;

  getItemsToDisplay(): any[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.categories.slice(startIndex, startIndex + this.itemsPerPage);
  }

  get totalPages(): number {
    return Math.ceil(this.categories.length / this.itemsPerPage);
  }

  studywaveModal: boolean = false;
  CategoriesModal: boolean = false;
  ScalePointsModal: boolean = false;
  DemographicsModal: boolean = false;
  iscatFilled: boolean = false;
  isdemoFilled: boolean = false;
  updateModel: UpdateModel = new UpdateModel();

  studyWaveSelected: any = [];

  ScalePointArray: any = [];

  demoArraySelected: any = [];
  demoArrayNeedToShow: any = [1];


  CategoryArraySelected: any = [];
  reportTypeName: any;
  ReportName: any;
  ReportTypeId: any;
  DatabaseName: any;
  ReportShowName: any;

  categoryString: any;
  StudyWavesString: any;
  DemoString: any;
  ScaleString: any;

  IsExist: any = [];
  DistinctDemoType: any = [];

  async ngOnInit(): Promise<void> {
    try {
      this.DatabaseName = await this.getDatabaseName(this.parameters);

      this.ReportTypeId = await this.getReportTypeByRecID(this.parameters);

      this.reportTypeName = await this.getReportTypeName(this.DatabaseName, this.ReportTypeId);

      this.ReportShowName = this.reportTypeName.split(" ")[0];

      this.getReportData();
    } catch (error) {
      console.error('Error:', error);
    }
  }

  getReportData() {
    this.service.getReportData(this.parameters).subscribe(
      res => {
        this.reportData = res[0] as ReportData;

        // Use forkJoin to wait for multiple observables to complete
        forkJoin([
          this.service.getStudyTypes(this.reportData.dBaseName),
          this.service.getScalePoints(this.reportData.dBaseName),
          this.service.getDemographics(this.reportData.dBaseName)
        ]).subscribe(
          ([studyTypes, scalePoints, demographics]) => {
            this.studyData = studyTypes as StudyData[];
            this.FillArrayWithExistingStudyWaves(this.studyData);

            this.scalePoint = scalePoints as ScalePoint[];
            this.FillArrayExitingScale(this.scalePoint);

            this.demodata = demographics as DemoData[];
            this.FillArrayExitingDemo(this.demodata);
          },
          error => {
            console.error('Error fetching data:', error);
          }
        );
      }
    );
  }



  toggleStudywavesModal() {
    this.studywaveModal = !this.studywaveModal;
  }

  toggleCategoriesModal() {
    this.CategoriesModal = !this.CategoriesModal;
    if (!this.categories.length) {
      this.getTarget();
    }
  }

  toggleScalePointModal() {
    this.ScalePointsModal = !this.ScalePointsModal;
  }

  toggleDemographicssModal() {
    this.DemographicsModal = !this.DemographicsModal;
    this.GetDemoTypeName();
    setTimeout(() => {
      const defaultIndex = 0;
      if (this.demoType.length > 0) {
        const selectedIndex = Math.min(defaultIndex, this.demoType.length - 1);
        this.OnClickDemoName(this.demoType[selectedIndex].demoType);
      }
    }, 300);
  }


  onChangeCheckBox(data: any, studywave: any) {
    if (data.target.checked) {
      this.studyWaveSelected.push(studywave);
    }
    else {
      //check it present in list if so remove it 
      if (this.studyWaveSelected.indexOf(studywave) != -1) {
        var index = this.studyWaveSelected.indexOf(studywave);
        this.studyWaveSelected.splice(index, 1);
        console.log(this.studyWaveSelected)
      }
    }
    this.StudyWavesString = this.studyWaveSelected.map(obj => obj.recID).join(',');
  }

  // Select All functionality 
  toggleSelectAllStudyWaves(event: any) {
    const selectAllChecked = event.target.checked;
    if (selectAllChecked) {
      this.studyWaveSelected = this.studyData.slice();
    } else {
      this.studyWaveSelected = [];
    }
    this.StudyWavesString = this.studyWaveSelected.map(obj => obj.recID).join(',');
  }

  isStudyWaveSelected(studyWave: any): boolean {
    return this.studyWaveSelected.some(selectedStudyWave => selectedStudyWave.recID === studyWave.recID);
  }
  trackByStudyWaveFn(index: number, item: any) {
    return item.recID; // Use a unique identifier for each item
  }

  OnChangeScaleCheckBox(data: any, scalepoint: any) {
    if (data.target.checked) {
      this.ScalePointArray.push(scalepoint);
    }
    else {
      if (this.ScalePointArray.indexOf(scalepoint) != -1) {
        var data = this.ScalePointArray.indexOf(scalepoint);
        this.ScalePointArray.splice(data, 1);
      }
    }
    this.ScaleString = this.ScalePointArray.join(',');

  }
  // scale point select all functionality 
  toggleSelectAllScalePoints(event: any) {
    const selectAllChecked = event.target.checked;
    if (selectAllChecked) {
      this.ScalePointArray = this.scalePoint.map(scalePoint => scalePoint.scalePoint);
    } else {
      this.ScalePointArray = [];
    }
    this.ScaleString = this.ScalePointArray.join(',');
  }

  isScalePointSelected(scalePoint: any): boolean {
    return this.ScalePointArray.includes(scalePoint);
  }

  OnChangeDemoBox(data: any, demoName: any) {
    if (data.target.checked) {
      if (this.demoArraySelected.indexOf(demoName) == -1) {
        this.demoArraySelected.push(demoName);
      }
    }
    else {
      if (this.demoArraySelected.indexOf(demoName) != -1) {
        var data = this.demoArraySelected.indexOf(demoName);
        this.demoArraySelected.splice(data, 1);
      }
    }
    this.DemoString = this.demoArraySelected.map(obj => obj.demoCode).join(",");
  }


  OnChangeCategoriesBox(data: any, name: any) {
    if (data.target.checked) {
      if (this.CategoryArraySelected.indexOf(name) == -1) {
        this.CategoryArraySelected.push(name);
      }
    }
    else {
      if (this.CategoryArraySelected.indexOf(name) != -1) {
        var data = this.CategoryArraySelected.indexOf(name);
        this.CategoryArraySelected.splice(data, 1);
      }
    }
    this.categoryString = this.CategoryArraySelected.map(obj => obj.code).join(',');
  }


  removeAndUncheck(data: any, item: any) {
    //remove from array
    if (this.studyWaveSelected.indexOf(item) != -1) {
      var index = this.studyWaveSelected.indexOf(item);
      this.studyWaveSelected.splice(index, 1);
    }
    //need to uncheck
    document.getElementById(item.recID).click();
  }

  removeAndUncheckScale(data: any, item: any) {
    //remove from array
    if (this.ScalePointArray?.indexOf(item) != -1) {
      var data = this.ScalePointArray.indexOf(item);
      this.ScalePointArray.splice(data, 1);
    }
    document.getElementById(item).click();
  }

  removeAndUncheckDemo(data: any, item: any) {
    // remove from array
    if (this.demoArraySelected?.indexOf(item) != -1) {
      var index = this.demoArraySelected.indexOf(item);
      this.demoArraySelected.splice(index, 1);
      this.DemoString = this.demoArraySelected.map(obj => obj.demoCode).join(",");
    }
  }

  removeAndUncheckCat(data: any, item: any) {
    if (this.CategoryArraySelected?.indexOf(item) != -1) {
      var data = this.CategoryArraySelected.indexOf(item);
      this.CategoryArraySelected.splice(data, 1);
      this.categoryString = this.CategoryArraySelected.map(obj => obj.code).join(',');
    }

    document.getElementById(item.name).click();

  }

  getTarget() {
    this.service.getTargetData(this.DatabaseName, this.ReportTypeId, this.StudyWavesString, this.parameters).subscribe(
      (res) => {
        this.categories = res as Categories[];
        this.FillArrayExitingCate(this.categories);
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }


  getReportTypeByRecID(parameters): Promise<number> {
    return new Promise((resolve, reject) => {
      this.service.getReportTypeByRecID(parameters).subscribe({
        next: (res: number) => {
          resolve(res);
          this.ReportTypeId = res;
        },
        error: (error) => {
          reject(error);
        }
      });
    });
  }

  getDatabaseName(parameters): Promise<string> {
    return new Promise((resolve, reject) => {
      this.service.getDatabaseName(parameters).subscribe({
        next: (res: string) => {
          resolve(res);
          this.DatabaseName = res;
        },
        error: (error) => {
          reject(error);
        }
      });
    });
  }

  getReportTypeName(databaseName: string, reportTypeId: number): Promise<string> {
    return new Promise((resolve, reject) => {
      this.service.getReportTypeName(this.DatabaseName, this.ReportTypeId).subscribe({
        next: (res: string) => {
          resolve(res);
        },
        error: (error) => {
          reject(error);
        }
      });
    });
  }

  FillArrayWithExistingStudyWaves(data: any) {
    const selectedStudyWaves = [];
    for (let i = 0; i < data.length; i++) {
      if (this.reportData.studydates.includes(data[i].recID.toString())) {
        selectedStudyWaves.push(data[i]);
      }
    }
    const clickStudyWaves = () => {
      if (selectedStudyWaves.length) {
        const studyWave = selectedStudyWaves.shift();
        document.getElementById(studyWave.recID).click();
        setTimeout(() => {
          clickStudyWaves();
        }, 10);
      } else {
        this.getTarget();
      }
    };
    setTimeout(() => {
      clickStudyWaves();
    }, 10);
  }



  FillArrayExitingScale(data: any) {
    for (let i = 0; i < data.length; i++) {
      if (this.reportData.scalePoints.includes(data[i].recID.toString())) {
        setTimeout(() => {
          document.getElementById(data[i].scalePoint).click();
        }, 20);
      }
    }
  }

  FillArrayExitingDemo(data: any) {
    var demoCodes = this.reportData.demoCodes.split(',').map(x => parseInt(x));
    for (let i = 0; i < data.length; i++) {
      if (this.isdemoFilled || demoCodes.indexOf(data[i].demoCode) != -1) {
        if (this.demoArraySelected.indexOf(data[i]) == -1) {
          this.demoArraySelected.push(data[i]);
          this.DemoString = this.demoArraySelected.map(obj => obj.demoCode).join(",");
        }
      }
    }


  }

  FillArrayExitingCate(data: any) {
    var targets = this.reportData.targets.split(',');
    for (let i = 0; i < data.length; i++) {
      if (this.iscatFilled || targets.indexOf(data[i].code.toString()) != -1) {
        setTimeout(() => {
          if (this.CategoryArraySelected.indexOf(data[i]) == -1) {
            this.CategoryArraySelected.push(data[i]);
            this.categoryString = this.CategoryArraySelected.map(obj => obj.code).join(',');
          }
          document.getElementById(data[i].name).click();

        }, 20)
      }
    }
  }

  OnUpdate() {
    this.updateModel.reportName = this.reportData.reportName;
    this.updateModel.rptType = this.reportData.rptType;
    this.updateModel.studydates = this.StudyWavesString;
    this.updateModel.scalePoints = this.ScaleString;
    this.updateModel.demoCodes = this.DemoString;
    this.updateModel.targets = this.categoryString;
    this.updateModel.application = this.reportData.dBaseName;
    this.updateModel.miscellaneous = this.reportData.miscellaneous;
    this.service.UpdateReport(this.parameters, this.updateModel).subscribe(
      res => {
        console.log(res);

        setTimeout(() => {
          window.location.reload();
        }, 1000);

      },
      err => {
        console.log(err);
      }
    );
  }

  GetDemoTypeName() {
    this.service.DemoTypeName(this.DatabaseName).subscribe(
      res => {
        this.demoType = res;
      }
    )
  }

  OnClickDemoName(DemoName: any) {
    this.service.DemoWithName(this.DatabaseName, DemoName).subscribe(
      res => {
        this.demoArrayNeedToShow = res;
        this.ClickEveryThatInSelected(this.demoArrayNeedToShow);
      }
    )

  }

  ClickEveryThatInSelected(dataShow: any) {
    var CommonDemoCodesArray = [];
    for (let i = 0; i < dataShow.length; i++) {
      for (let j = 0; j < this.demoArraySelected.length; j++) {
        if (dataShow[i].demoCode == this.demoArraySelected[j].demoCode) {
          CommonDemoCodesArray.push(this.demoArraySelected[j].demoCode + '-' + i);
        }
      }
    }
    setTimeout(() => {
      for (let i = 0; i < CommonDemoCodesArray.length; i++) {
        document.getElementById(CommonDemoCodesArray[i]).click();
      }
    }, 10);

  }

  nextCategory(curr: any) {
    this.currentPage = curr + 1;
    this.iscatFilled = true;
    this.FillArrayExitingCate(this.CategoryArraySelected);
  }

  prevCategory(curr: any) {
    this.currentPage = curr - 1;
    this.iscatFilled = true;
    this.FillArrayExitingCate(this.CategoryArraySelected);
  }

  FillDistinct(myArray: any) {
    for (let i = 0; i < myArray.length; i++) {
      if (this.DistinctDemoType.indexOf(myArray[i].demoType) == -1) {
        this.DistinctDemoType.push(myArray[i].demoType);
      }
    }
  }

  onSaveView() {
    this.OnUpdate();
    this.router.navigate([`viewReport/Report/${this.parameters}`]);
  }

  onSave() {
    this.OnUpdate();
    Swal.fire({
      title: 'Successful!',
      text: 'Report Updated Successfully.',
      icon: 'info',
    });
  }

  Reset() {
    this.studyWaveSelected = [];
    this.ScalePointArray = [];
    this.demoArraySelected = [];
    this.CategoryArraySelected = [];
    this.DistinctDemoType = [];
  }


}