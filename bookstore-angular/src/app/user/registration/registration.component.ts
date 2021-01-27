import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(public userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userService.formModel.reset();
   }

  onSubmit(){
    this.userService.register().subscribe(
      (res:any) => {
        this.toastr.success("New user created!","Registration successful");
      },
      err => {
        console.log(err);
      }
    );
  }

}
