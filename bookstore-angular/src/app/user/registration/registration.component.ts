import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  constructor(public userService: UserService) { }

  ngOnInit(): void { }

  onSubmit(){
    this.userService.register().subscribe(
      (res:any) => {
        // if(success){
        //   this.userService.formModel.reset();
        // } else {
        //   show error
        // }
      },
      err => {
        console.log(err);
      }
    );
  }

}
