import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MasterService } from 'src/app/Shared/master.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  public constructor(private route: ActivatedRoute, public service: MasterService) {
    if (route.snapshot.data['route'] != undefined) this.service.routeName = route.snapshot.data['route'];
  }
}
