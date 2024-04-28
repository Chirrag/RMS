import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';
import * as XLSX from 'xlsx';



@Component({
  selector: 'app-view-report',
  templateUrl: './view-report.component.html',
  styleUrls: ['./view-report.component.css']
})
export class ViewReportComponent implements OnInit {

  constructor(public service: MasterService, private route: ActivatedRoute, private router: Router) {
  }



  parameters: any = this.route.params['_value']['recId'];
  DatabaseName: any;
  showTable: boolean = false;
  QScoreData: any[] = [];
  ReportData: any[] = [];
  ReportName: any;
  ReportTypeId: any;
  pagedQScoreData: any[] = [];
  currentValue: any = "";
  comaprsionValue: any = "";
  rankingParameter: number;
  currentPage: number = 1;
  itemsPerPage: number = 20;
  ApplicationName: any;
  Name: any;
  includeCategoriesValue:any = 0;
  showLoader = false;
  errorMessage: string | null = null;


  ngOnInit(): void {
    this.GetDatabaseName();
    this.GetReportData()
    this.GetRpeortTypeID();
  }

  myArray: string[] = [
    "Individual profile",
    "Comparison profile"
  ];

  DownloadArray: string[] = [
    "Excel",
    "XML"
  ];


  fileName = 'ExcelSheet.xlsx';

  selectedValue: any = "Individual profile";
  myCurrentValue: any ="";
  formatId: any;

  FormateValue: any = "Excel";

  onPageChange(page: number): void {
    const startIndex = (page - 1) * this.itemsPerPage;
    this.pagedQScoreData = this.QScoreData.slice(startIndex, startIndex + this.itemsPerPage);
    this.currentPage = page;
  }

  GetDatabaseName() {
    this.service.getDatabaseName(this.parameters).subscribe(
      (res: string) => {
        this.DatabaseName = res;
      }
    )
  }
  GetReportName() {
    this.service.getReportTypeName(this.DatabaseName, this.ReportTypeId).subscribe(
      (res: any) => {
        this.ReportName = res;
      });
  }



  // GetQScoreReportData() {
  //   this.showLoader=true;
  //   this.showTable=false;
  //   this.formatId = this.myArray.indexOf(this.selectedValue);
  //   this.service.GetQScoreReportData(this.DatabaseName, this.parameters, this.formatId,this.includeCategoriesValue ? 1 : 0).subscribe((res: any) => {
  //     this.QScoreData = res;
  //     this.pagedQScoreData = this.QScoreData;
  //     this.ApplicationName = this.ReportData[0].application;
  //     this.GetReportName();
  //     this.myCurrentValue=this.selectedValue;
  //     this.onPageChange(1);
  //     setTimeout(()=>{
  //       this.showTable = true;
  //       this.showLoader=false;
  //     },500);
      
  //   })
  // }

  GetQScoreReportData() {
    this.errorMessage = null; // Reset error message on each button click
    try {
      this.showLoader = true;
      this.showTable = false;
      this.formatId = this.myArray.indexOf(this.selectedValue);
      this.service.GetQScoreReportData(this.DatabaseName, this.parameters, this.formatId, this.includeCategoriesValue ? 1 : 0).subscribe(
        (res: any) => {
          this.QScoreData = res;
          this.pagedQScoreData = this.QScoreData;
          this.ApplicationName = this.ReportData[0].application;
          this.GetReportName();
          this.myCurrentValue = this.selectedValue;
          this.onPageChange(1);
          setTimeout(() => {
            this.showTable = true;
            this.showLoader = false;
          }, 500);
        },
        (error: any) => {
          // Handle error
          console.error('An error occurred:', error);
          // Set error message
          this.errorMessage = 'An error occurred while fetching data. Please try again.';
          // Hide loader
          this.showLoader = false;
        }
      );
    } catch (error) {
      // Handle synchronous errors
      console.error('An unexpected error occurred:', error);
      // Set error message
      this.errorMessage = 'An unexpected error occurred. Please try again.';
      // Hide loader
      this.showLoader = false;
    }
  }
  

  GetRpeortTypeID() {
    this.service.getReportTypeByRecID(this.parameters).subscribe(
      res => {
        this.ReportTypeId = res;
      });
  }

  GetReportData() {
    this.service.getReportData(this.parameters).subscribe(
      (res: any) => {
        this.ReportData = res;
        console.log(this.ReportData)
        this.Name = this.ReportData[0]?.reportName;
        const miscellaneousData = this.ReportData[0]?.miscellaneous;
        const keyValuePairs = miscellaneousData.split(',');
        // Loop through key-value pairs to find RankingParameter
        for (const pair of keyValuePairs) {
          const [key, value] = pair.split('=');
          if (key === 'RankingParameter') {
            this.rankingParameter = value;
            break;
          }
        }
      }
    )
  }

  setValue(data: any) {
    this.currentValue = data;
  }
  setValueForComparison(data: any) {
    this.comaprsionValue = data;
  }

  exportToExcel() {
    this.pagedQScoreData = this.QScoreData;
    setTimeout(() => {
      let data = document.getElementById("table-data") as HTMLTableElement;
      let ws = XLSX.utils.table_to_sheet(data);
      ws['!cols'] = [];
      const headerRow = XLSX.utils.decode_range(ws['!ref']).s.r;
      const headerCells = XLSX.utils.decode_range(ws['!ref']).e.c;
      for (let i = 0; i <= headerCells; i++) {
        ws['!cols'][i] = { wpx: 120 };
      }
      let wb = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
      XLSX.writeFile(wb, this.Name + ".xlsx");
      this.onPageChange(this.currentPage);
    }, 200);
  }

  exportToXML() {
    this.pagedQScoreData = this.QScoreData;
    setTimeout(() => {
      let data = document.getElementById("xml-data") as HTMLTableElement;
      let xmlContent = '<?xml version="1.0" encoding="UTF-8"?>';
      xmlContent += '<tableData>';
  
      if (data.rows.length > 0) {
        for (let i = 1; i < data.rows.length; i++) {
          xmlContent += '<row>';
  
          if (data.rows[i].cells) {
            for (let j = 0; j < data.rows[i].cells.length; j++) {
              let cellValue = this.escapeXml(data.rows[i].cells[j].textContent || '');
              let columnName = data.rows[0].cells[j].innerText.toLowerCase().replace(/ /g, '');
              
              // Log columnName and cellValue for debugging
              console.log(`Column Name: ${columnName}, Cell Value: ${cellValue}`);
  
              xmlContent += `<${columnName}>${cellValue}</${columnName}>`;
            }
          } else {
            console.error("Cells not found in row:", i);
          }
  
          xmlContent += '</row>';
        }
      } else {
        console.error("No rows found in the table");
      }
  
      xmlContent += '</tableData>';
  
      // Log the generated XML content for debugging
      console.log("Generated XML Content:", xmlContent);
  
      const blob = new Blob([xmlContent], { type: 'application/xml' });
      const link = document.createElement('a');
      link.href = window.URL.createObjectURL(blob);
      link.download = this.Name + '.xml';
      link.click();
      this.onPageChange(this.currentPage);
    }, 200);
  }
  
  
   exportFile(){
    if (this.FormateValue === "Excel") {
      this.exportToExcel();
    } else if (this.FormateValue === "XML") {
      this.exportToXML();
    }
   }

    
    escapeXml(text: string): string {
      return text.replace(/[<>&'"]/g, function (char) {
        switch (char) {
          case '<':
            return '&lt;';
          case '>':
            return '&gt;';
          case '&':
            return '&amp;';
          case "'":
            return '&apos;';
          case '"':
            return '&quot;';
          default:
            return char;
        }
      });
    }

    
}
