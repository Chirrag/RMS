import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';
import { Search } from 'src/app/Shared/models/search.model';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  constructor(public service: MasterService, private route: ActivatedRoute, private router: Router) {
    //if (route.snapshot.data['route'] != undefined) this.service.routeName = route.snapshot.data['route'];
  }
  
  parameters: string;
  erroMesaage:string="";

  ngOnInit(): void {
    this.parameters = this.route.snapshot.queryParams['parameters'];
    this.service.onSearch();
    // setTimeout(()=>{
    //   this.erroMesaage= this.service.errorMessage;
    //   console.log(this.erroMesaage)
    // },1000)
    
  }
  
  
  
  
   

}
