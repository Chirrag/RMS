import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';

@Component({
  selector: 'app-adult',
  templateUrl: './adult.component.html',
  styleUrls: ['./adult.component.css']
})
export class AdultComponent {
  public constructor(private route: ActivatedRoute, public service: MasterService) {
    if (route.snapshot.data['route'] != undefined) this.service.routeName = route.snapshot.data['route'];
   }
}
