import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';
import { SearchView } from 'src/app/Shared/models/SearchView.model';

@Component({
  selector: 'app-report-details',
  templateUrl: './report-details.component.html',
  styleUrls: ['./report-details.component.css']
})
export class ReportDetailsComponent implements OnInit{
  constructor(public service: MasterService, private route: ActivatedRoute, private router:Router) { }

  SearchView:SearchView[]= [];
  ngOnInit(): void {
    this.getSearchView();
  }

  searchString: string = this.route.snapshot.queryParams['name'];
  studyType: string = this.route.snapshot.queryParams['studyType'];
  
  
  getSearchView(){
  this.service.getSearchViewDetails(this.searchString,this.studyType).subscribe(
   res=>{
    this.SearchView=res;
    console.log(this.SearchView);
    
   }
  )

  }
  }
  

