import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { MasterService } from 'src/app/Shared/master.service';
import { Userinfo } from 'src/app/Shared/models/userinfo.model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-modify',
  templateUrl: './user-modify.component.html',
  styleUrls: ['./user-modify.component.css']
})
export class UserModifyComponent implements OnInit{
  constructor(private fb: FormBuilder, public service: MasterService) { }

  ngOnInit(): void {
    this.editProfile();
    this.changePicture();
    this.changePassword();
  }

  @Input() userModifyModalShow: string;
  @Input() userInfo: Userinfo;
  @Output() childEvent = new EventEmitter<any>();
  errorMsg: string = "";
  errorMsgPasswordChange: string = "";
  imageData: string | null = null;


  //Form Methods
  editProfileForm: FormGroup;
  changePictureForm: FormGroup;
  changePasswordForm: FormGroup;

  editProfile() {
    this.editProfileForm = this.fb.group({
      fullName: [this.userInfo.fullName, [Validators.required]],
      officePhone: [this.userInfo.officePhone, [Validators.required]],
      cellPhone: [this.userInfo.cellPhone, [Validators.required]],
    })
  }

  changePicture() {
    this.changePictureForm = this.fb.group({
      imageFile: ['', [Validators.required]],
    })
  }

  changePassword() {
    this.changePasswordForm = this.fb.group({
      oldPassword: ["", [Validators.required]],
      newPassword: ["", [Validators.required]],
      newPasswordAgain: ["", [Validators.required, passwordsMatchValidator]],
    })
  }


  //Submit Functions
  onSubmitEditProfile() {
    this.service.postUpdateUserInfo(this.editProfileForm.value).subscribe(
      res => {
        this.childEvent.emit();
        this.errorMsg = "";
        Swal.fire({
          title: 'Successful!',
          text: 'Profile Edited Successfully.',
          icon: 'success',
        })
      },
      err => {
        console.log(err);
        this.errorMsg = err.error.message || "Something went wrong!";
      }
    )
  }

  onSubmitChangePassword() {
    this.service.postChangePassword(this.changePasswordForm.value).subscribe(
      res => {
        // console.log(res);
        this.childEvent.emit();
        this.errorMsgPasswordChange = "";
        this.changePasswordForm.reset();
        Swal.fire({
          title: 'Successful!',
          text: 'Password Changed Successfully.',
          icon: 'success',
        })
      },
      err => {
        console.log(err);
        this.errorMsgPasswordChange = err.error.message || "Something went wrong!";
      }
    )
  }

  onSubmitChangePicture() {
    if(this.imageData)
    {
      this.service.postChangeProfilePicture(this.imageData).subscribe(
        res => {
          // console.log(res);
          this.childEvent.emit();
          this.errorMsg = "";
          this.changePictureForm.reset();
          Swal.fire({
            title: 'Successful!',
            text: 'Picture Changed Successfully.',
            icon: 'success',
          })
        },
        err => {
          console.log(err);
          this.errorMsg = err.error.message || "Something went wrong!";
        }
      )
    }
  }

  //Convert image to binary
  handleImageInput(event: any) {
    const file = event.target.files[0];

    if (file) {
      this.convertImageToBinary(file);
    }
  }

  convertImageToBinary(file: File) {
    const reader = new FileReader();

    reader.onload = (e: any) => {
      // 'e.target.result' contains the Base64 encoded image data
      this.imageData = e.target.result;
      this.imageData = this.imageData.substring('data:image/png;base64,'.length);
    };

    reader.readAsDataURL(file);
  }


}



function passwordsMatchValidator(control: AbstractControl) {
  const newPassword = control.value;
  const newPasswordAgainControl = control.root.get('newPassword');
  
  if (newPasswordAgainControl) {
    const confirmPassword = newPasswordAgainControl.value;

    if (newPassword === confirmPassword) {
      return null; // Passwords match, validation passes
    }
  }

  return { passwordMismatch: true }; // Passwords don't match, validation fails
}
