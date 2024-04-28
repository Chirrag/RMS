import { Component, OnInit } from '@angular/core';
import { MasterService } from '../Shared/master.service';
import { AuthService } from '../Shared/Auth/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  title(title: any) {
    throw new Error('Method not implemented.');
  }

  constructor(public service: MasterService, public authService: AuthService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.login();
  }

  isMobileMenuOpen = false;
  showModal = false;
  errorMsg: string = "";
  searchInput: string = "";
  loginLoader: boolean = false;
  // searchLoader: boolean = false;

  //login method
  loginForm: FormGroup;

  login() {
    this.loginForm = this.fb.group({
      Username: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required]],
      // Password: ['', [Validators.required, Validators.minLength(8), passwordStrength]],
    })
  }

  onSubmit() {
    this.loginLoader = true;
    this.service.postLoginUser(this.loginForm.value).subscribe(
      res => {
        // console.log(res);
        this.errorMsg = "";
        this.loginLoader = false;
        sessionStorage.setItem('_token', JSON.stringify(res));
        this.loginForm.reset();
        this.showModal = false;
        this.router.navigateByUrl('/user');
      },
      err => {
        console.log(err);
        this.errorMsg = err.error.message || "Something went wrong!";
        this.loginLoader = false;
      }
    )
  }

  //toggle menu in mobile
  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  //toggle login modal
  toggleModal() {
    this.showModal = !this.showModal;
  }

  //search bar function
  async onSearch() {
    await this.router.navigateByUrl(`/search?parameters=${this.searchInput}`);
    this.service.onSearch();
  }

  //logout
  onLogout() {
    Swal.fire({
      title: 'Confirm Logout',
      text: 'Are you sure you want to log out?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Logout',
      cancelButtonText: 'Cancel'
    }).then((result) => {
      if (result.isConfirmed) {
        sessionStorage.clear();
        this.authService.isLoggedIn = false;
        this.router.navigateByUrl('/');
      } else {
        // console.log('Logout canceled');
      }
    });
  }
}
