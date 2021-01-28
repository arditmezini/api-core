import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/core/services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
  constructor(
    public userService: UserService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userService.formModel.reset();
  }

  onSubmit() {
    this.userService.register().subscribe(
      (res: any) => {
        this.toastr.success('New user created!', 'Registration successful');
        this.router.navigate(['auth/login']);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
