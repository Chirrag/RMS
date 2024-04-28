import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MasterService } from 'src/app/Shared/master.service';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-excel-files',
  templateUrl: './excel-files.component.html',
  styleUrls: ['./excel-files.component.css']
})
export class ExcelFilesComponent {
  constructor(private service: MasterService) { }
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  dataSource = new MatTableDataSource<any>();
  displayedColumns: string[] = ['list'];


  ngOnInit() {
    this.getAllZipFiles();
  }

  getAllZipFiles() {
    this.service.getAllZipFiles().subscribe(
      res => {
        // console.log(res);
        this.dataSource.data = res;
        this.dataSource.paginator = this.paginator;

      },
      err => {
        console.log(err);
      }
    )
  }
  zipDownload(data:any){
   this.service.ZipDownloadFile(data).subscribe((blob: Blob) => {
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    document.body.appendChild(a);
    a.href = url;
    a.download = data; // Set the filename
    a.click();
    window.URL.revokeObjectURL(url);
  });
  
  }
}
